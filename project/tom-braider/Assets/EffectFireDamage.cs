using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFireDamage : MonoBehaviour
{
    public float FireDamageInterval = 0.2f;
    public float FireDamage = 1f;

    private float FireDamageTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetDamageTimer() {
        FireDamageTime = 0f;
    }

    private void OnTriggerStay(Collider other) {
        FireDamageTime -= Time.deltaTime;
        if (FireDamageTime <= 0 ) {
            FireDamageTime = FireDamageInterval;
            Debug.Log("Fire Damage for " + other.gameObject.name);
        }
    }
}
