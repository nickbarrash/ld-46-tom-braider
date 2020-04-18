using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTriggerToggle : MonoBehaviour
{
    private int InTriggerCount = 0;

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsTriggered() {
        return InTriggerCount > 0;
    }

    private void OnTriggerEnter(Collider collider) {
        Debug.Log("Start Trigger " + collider.gameObject.name);
        InTriggerCount += 1;
    }

    private void OnTriggerExit(Collider collider) {
        Debug.Log("Stop Trigger " + collider.gameObject.name);
        InTriggerCount -= 1;
    }
}
