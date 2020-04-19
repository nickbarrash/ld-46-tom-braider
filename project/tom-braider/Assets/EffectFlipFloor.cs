using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EffectFlipFloor : EffectOnStayDepress {
    private Dictionary<string, GameObject> LaunchablesMap;

    public Vector3 LaunchForce;

    public float LaunchAnimationDuration = 0.1f;
    public float PauseAnimationDuration = 0.5f;
    public float RetractAnimationDuration = 1f;

    private float LaunchAnimationTimer = 0f;
    private float PauseAnimationTimer = 0f;
    private float RetractAnimationTimer = 0f;

    private bool Animating = false;

    // Start is called before the first frame update
    void Start()
    {
        LaunchablesMap = new Dictionary<string, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LaunchAnimationTimer > 0f) {
            LaunchAnimationTimer -= Time.deltaTime;
            LaunchAnimate();
            if (LaunchAnimationTimer <= 0f) {
                PauseAnimationTimer = PauseAnimationDuration - LaunchAnimationTimer;
                LaunchAnimationTimer = 0f;
            }
        } else if (PauseAnimationTimer > 0f) {
            PauseAnimationTimer -= Time.deltaTime;
            if (PauseAnimationTimer <= 0f) {
                RetractAnimationTimer = RetractAnimationDuration - PauseAnimationTimer;
                PauseAnimationTimer = 0f;
            }
        } else if (RetractAnimationTimer > 0f) {
            RetractAnimationTimer -= Time.deltaTime;
            RetractAnimate();
            if (RetractAnimationTimer <= 0f) {
                RetractAnimationTimer = 0f;
                transform.rotation = Quaternion.identity;
                Animating = false;
            }
        }
    }

    public void LaunchAnimate() {
        SetRotation(Mathf.InverseLerp(LaunchAnimationDuration, 0, LaunchAnimationTimer));
    }

    public void RetractAnimate() {
        SetRotation(Mathf.InverseLerp(0, RetractAnimationDuration, RetractAnimationTimer));
    }

    public void SetRotation(float t) {
        transform.rotation = Quaternion.Euler(new Vector3(90 * t, 0, 0));
    }

    public override void StartEffect() {
        StartLaunch();
    }

    public void StartLaunch() {
        if (!Animating) {
            Animating = true;
            LaunchAnimationTimer = LaunchAnimationDuration;
            foreach (GameObject character in LaunchablesMap.Values) {
                NavMeshAgent agent = character.gameObject.GetComponent<NavMeshAgent>();
                if (agent != null) {
                    agent.enabled = false;
                }

                Follow follower = character.gameObject.GetComponent<Follow>();
                if (follower != null) {
                    follower.Fly();
                }
                character.transform.position += Vector3.up * 2;
                character.GetComponent<Rigidbody>().velocity = Vector3.zero;
                character.GetComponent<Rigidbody>().AddForce(LaunchForce, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (GameConstants.IsCharacterLayer(other.gameObject.layer)) {
            LaunchablesMap.Add(other.gameObject.name, other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (GameConstants.IsCharacterLayer(other.gameObject.layer)) {
            LaunchablesMap.Remove(other.gameObject.name);
        }
    }
}
