using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public GameObject Target;
    private NavMeshAgent NavAgent;

    [HideInInspector]
    public bool IsFollowing, IsFlying, IsGrabbingTreasure;

    public float FlyingNavmeshVeloTreshold = 0.1f;

    // Just enough so we dont' turn nav back on too quickly
    private float FlyingDuration = 0.5f;
    private float FlyingTimer = 0f;

    public GameObject Treasure;

    // Start is called before the first frame update
    void Start()
    {
        IsFollowing = true;
        NavAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGrabbingTreasure && Input.GetKeyDown(KeyCode.Space)) {
            IsFollowing = !IsFollowing;
        }

        if (NavAgent.isActiveAndEnabled) {
            if (IsGrabbingTreasure) {
                NavAgent.SetDestination(Treasure.transform.position);
            } else if (IsFollowing) {
                NavAgent.SetDestination(Target.transform.position);
            } else {
                NavAgent.SetDestination(transform.position);
            }
        }

        if (IsFlying == true) {
            if (FlyingTimer <= 0 && GetComponent<Rigidbody>().velocity.magnitude < FlyingNavmeshVeloTreshold) {
                IsGrabbingTreasure = true;
                NavAgent.enabled = true;
                Debug.Log("NavAgent Back on!");
                IsFlying = false;
            }
            FlyingTimer -= Time.deltaTime;
        }
    }

    public void Fly() {
        IsFlying = true;
        FlyingTimer = FlyingDuration;
    }

    public void GrabTreasure() {
        IsGrabbingTreasure = false;
        IsFollowing = true;
    }
}
