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
            if (Input.GetKey(KeyCode.W)) {
                transform.Translate(0f, 0f, MoveDist);
            }

            if (Input.GetKey(KeyCode.S)) {
                transform.Translate(0f, 0f, -1f * MoveDist);
            }

            if (Input.GetKey(KeyCode.A)) {
                transform.Translate(-1f * MoveDist, 0f, 0f);
            }

            if (Input.GetKey(KeyCode.D)) {
                transform.Translate(MoveDist, 0f, 0f);
            }
        }
    }
}
