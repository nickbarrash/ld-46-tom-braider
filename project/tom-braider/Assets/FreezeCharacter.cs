using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCharacter : MonoBehaviour
{
    public void Freeze(Vector3 TileCenter) {
        Debug.Log("freeze");
        transform.position = Vector3.Lerp(transform.position, TileCenter, 0.2f);
    }
}
