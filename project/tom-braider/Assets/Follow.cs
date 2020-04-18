using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public GameObject Target;
    private NavMeshAgent NavAgent;

    private bool IsFollowing;

    // Start is called before the first frame update
    void Start()
    {
        IsFollowing = true;
        NavAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            IsFollowing = !IsFollowing;
        }

        if (IsFollowing) {
            NavAgent.SetDestination(Target.transform.position);
        } else {
            //Debug.Log(transform.position);
            NavAgent.SetDestination(transform.position);
        }
    }
}
