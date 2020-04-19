using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSphere : MonoBehaviour {

    public GameObject DropWall;

    private void OnTriggerEnter(Collider other) {
        if (GameConstants.IsCharacterLayer(other.gameObject.layer)) {
            gameObject.SetActive(false);

            Debug.Log("Grabbed Treasure!!");

            Follow follower = other.gameObject.GetComponent<Follow>();
            if (follower != null) {
                follower.GrabTreasure();
            }

            DropWall.GetComponent<EffectWallDop>().TriggerWallFlip();
        }
    }
}
