﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEffectToggle : MonoBehaviour
{
    public TrapTriggerToggle TrapTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay(Collider collider) {
        if (TrapTrigger.IsTriggered()) {
            Debug.Log("Trigger Effect " + collider.gameObject.name);
        }
    }
}
