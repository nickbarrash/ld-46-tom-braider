using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpikeCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Spike");
    }
}
