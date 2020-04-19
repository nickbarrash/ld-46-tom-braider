using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDamage : MonoBehaviour {
    public float DamageInterval = 0.2f;
    public float Damage = 1f;
    public string DamageType;

    private float DamageTime;

    private void Start() {
        ResetDamageTimer();
    }

    public void ResetDamageTimer() {
        DamageTime = 0f;
    }

    private void OnTriggerStay(Collider other) {
        if (GameConstants.IsCharacterLayer(other.gameObject.layer)) {
            DamageTime -= Time.deltaTime;
            if (DamageTime <= 0) {
                DamageTime = DamageInterval - DamageTime;
                Debug.Log(DamageType + " Damage for " + other.gameObject.name);
            }
        }
    }

    private void OnCollisionStay(Collision collision) {
        //Debug.Log(collision.collider.gameObject.tag + " " + collision.collider.gameObject.name);
        if (GameConstants.IsCharacterLayer(collision.collider.gameObject.layer)) {
            Debug.Log(DamageType + " Damage for " + collision.collider.gameObject.name);
        }
    }
}
