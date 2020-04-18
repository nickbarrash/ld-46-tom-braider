using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public GameObject Target;
    private NavMeshAgent NavAgent;

    // Start is called before the first frame update
    void Start()
    {
        NavAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Target.transform.position - transform.position).magnitude > GameConstants.PLAYER_FOLLOW_DISTANCE) {
            NavAgent.SetDestination(Target.transform.position);
        } else {
            NavAgent.SetDestination(transform.position);
        }
    }
}
