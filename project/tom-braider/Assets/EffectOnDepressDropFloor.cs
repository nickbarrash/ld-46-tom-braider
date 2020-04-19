using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnDepressDropFloor : EffectOnDepress {

    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public override void TriggerEffect() {
        rb.constraints = RigidbodyConstraints.None;
        transform.localScale = transform.localScale * 0.99f;
    }
}
