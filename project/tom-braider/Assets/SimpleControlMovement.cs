using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleControlMovement : MonoBehaviour
{
    [HideInInspector]
    public bool IsFrozen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float MoveDist = 1f * Time.deltaTime * GameConstants.PLAYER_SPEED_MULTIPLE;

        if (!IsFrozen) {
            float x = 0, y = 0, z = 0;
            if (Input.GetKey(KeyCode.W)) {
                z += MoveDist;
            }

            if (Input.GetKey(KeyCode.S)) {
                z -= MoveDist;
            }

            if (Input.GetKey(KeyCode.A)) {
                x -= MoveDist;
            }

            if (Input.GetKey(KeyCode.D)) {
                x += MoveDist;
            }
            transform.Translate(new Vector3(x, y, z).normalized * MoveDist);
        }
    }
}
