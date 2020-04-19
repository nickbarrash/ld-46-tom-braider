using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EffectFallawayTile : EffectOnStayDepress {

    TriggerTile FallTile;
    EffectOnDepressDropFloor DropFloor;

    int DepressTotalCount = 0;

    private void Start() {
        FallTile = GetComponent<TriggerTile>();
        DropFloor = GetComponent<EffectOnDepressDropFloor>();
    }

    public override void AddDepress(Collider other) {
        DepressTotalCount++;
        if (DepressTotalCount == 1) {
            FallTile.SetTileColor(Color.yellow);
        }
        if (DepressTotalCount >= 2) {
            FallTile.TriggeredColor = Color.red;
            FallTile.SetTileColor(Color.red);
            DropFloor.TriggerEffect();
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            //Follow Follower = other.gameObject.GetComponent<Follow>();
            //if (Follower != null) {
            //    Follower.IsFollowing = false;
            //}

            NavMeshAgent Agent = other.gameObject.GetComponent<NavMeshAgent>();
            if (Agent != null) {
                Agent.enabled = false;
            }

            SimpleControlMovement Movement = other.gameObject.GetComponent<SimpleControlMovement>();
            if (Movement != null) {
                Movement.IsFrozen = true;
            }

            FreezeCharacter Freezer = other.gameObject.GetComponent<FreezeCharacter>();
            if (Freezer != null) {
                Freezer.Freeze(transform.position);
            } else {
                Debug.LogWarning("Fallaway step freezing non character");
            }
        }
    }
}
