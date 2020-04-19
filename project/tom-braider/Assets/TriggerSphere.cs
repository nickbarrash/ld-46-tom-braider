using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSphere : EffectOnDepress {
    public float ForcePower = 15f;
    public float RockDeleteTimer = 10f;

    bool isTriggered = false;

    private Rigidbody rb;



    public override void Update() {
        base.Update();

        if (isTriggered && RockDeleteTimer >=0) {
            RockDeleteTimer -= Time.deltaTime;
            if (RockDeleteTimer <= 0) {
                gameObject.SetActive(false);
            }
        }
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public override void TriggerEffect() {
        rb.AddForce(Vector3.back * ForcePower, ForceMode.Impulse);
        isTriggered = true;
    }
}
