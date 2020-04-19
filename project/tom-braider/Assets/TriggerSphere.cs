using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSphere : EffectOnDepress {
    public float ForcePower = 15f;

    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public override void TriggerEffect() {
        rb.AddForce(Vector3.back * ForcePower, ForceMode.Impulse);
    }
}
