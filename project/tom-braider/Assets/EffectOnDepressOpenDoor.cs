using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnDepressOpenDoor : EffectOnDepress
{
    public float AnimationDuration = 1.0f;
    private bool IsTriggered = false;
    private float AnimationTime = 0f;

    private GameObject Door;
    private float ScaleX;
    private float PositionX;

    // ASSUME WE ONLY SCALE BY X!!!!
    private void Start() {
        Door = transform.Find("Door").gameObject;
        
        if (Door.transform.localScale.z > 1) {
            Debug.Log("Warning, you should only scale doors on X axis");
        }

        ScaleX = Door.transform.localScale.x;
        PositionX = Door.transform.localPosition.x;
    }

    public override void Update() {
        base.Update();
        if (IsTriggered && AnimationTime > 0) {
            AnimationTime -= Time.deltaTime;
            float AnimationLerp = Mathf.InverseLerp(0f, AnimationDuration, AnimationTime);
            Door.transform.localScale = new Vector3(AnimationLerp * ScaleX, Door.transform.localScale.y, Door.transform.localScale.z);
            Door.transform.localPosition = new Vector3((1 - AnimationLerp) * ScaleX / 2.0f, Door.transform.localPosition.y, Door.transform.localPosition.z);
        }
    }

    public override void TriggerEffect() {
        IsTriggered = true;
        AnimationTime = AnimationDuration;
    }
}
