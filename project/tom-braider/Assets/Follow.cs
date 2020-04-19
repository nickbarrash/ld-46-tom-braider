using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public GameObject FollowTarget;
    public GameObject EndGameTarget;
    private NavMeshAgent NavAgent;

    [HideInInspector]
    public bool IsFollowing, IsFlying, IsGrabbingTreasure, IsEndGame;

    public float FollowOffThreshold = 2.0f;
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
        //FollowOriginal();
        FollowNew();
    }

    public void FollowNew() {
        if (!IsGrabbingTreasure && !IsEndGame && Input.GetKeyDown(KeyCode.Space)) {
            IsFollowing = !IsFollowing;
            if (!IsFollowing) {
                NavAgent.enabled = false;
            }else {
                NavAgent.enabled = true;
            }
        }

        if (IsEndGame) {
            if (NavAgent.isActiveAndEnabled) {
                NavAgent.SetDestination(EndGameTarget.transform.position);
            }
        } else if (IsGrabbingTreasure) {
            if (NavAgent.isActiveAndEnabled) {
                NavAgent.SetDestination(Treasure.transform.position);
            }
        } else {
            if (IsFollowing) {
                NavAgent.SetDestination(FollowTarget.transform.position);
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

    public void FollowOriginal() {
        if (!IsGrabbingTreasure && !IsEndGame && Input.GetKeyDown(KeyCode.Space)) {
            IsFollowing = !IsFollowing;
        }

        if (NavAgent.isActiveAndEnabled) {
            if (IsEndGame) {
                NavAgent.SetDestination(EndGameTarget.transform.position);
            } else if (IsGrabbingTreasure) {
                NavAgent.SetDestination(Treasure.transform.position);
            } else {
                if (NavAgent.isActiveAndEnabled) {
                    if (IsFollowing) {
                        NavAgent.SetDestination(FollowTarget.transform.position);
                    } else {
                        NavAgent.SetDestination(transform.position);
                    }
                }
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

    public bool IsCloseToPlayer() {
        return (transform.position - FollowTarget.transform.position).magnitude < FollowOffThreshold;
    }

    public void Fly() {
        IsFlying = true;
        FlyingTimer = FlyingDuration;
    }

    public void GrabTreasure() {
        IsGrabbingTreasure = false;
        IsFollowing = true;
    }

    public void EndGame() {
        NavAgent.enabled = true;
        IsEndGame = true;
    }
}
