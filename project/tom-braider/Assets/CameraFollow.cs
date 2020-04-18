using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject TargetObject;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(TargetObject.transform.position.x, transform.position.y, TargetObject.transform.position.z);
    }
}
