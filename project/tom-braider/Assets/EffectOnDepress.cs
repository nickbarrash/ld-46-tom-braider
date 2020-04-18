using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectOnDepress : MonoBehaviour
{
    public float EffectDelay = 0;

    [HideInInspector]
    public bool IsAnimatingBeforeTrigger = false;
    [HideInInspector]
    public bool IsTriggerFinished = false;

    // no animation at 0
    private float DelayTime = 0;

    public virtual void Update() {
        if (IsAnimatingBeforeTrigger && !IsTriggerFinished) {
            Debug.Log("Here!");
            DelayTime -= Time.deltaTime;
            if (DelayTime <= 0) {
                Debug.Log("Calling Trigger");
                TriggerEffect();
                IsTriggerFinished = true;
                IsAnimatingBeforeTrigger = false;
            } else {
                AnimatingBeforeTrigger();
            }
        }
    }

    public void Trigger() {
        Debug.Log("Triggered On Depress Effect");
        IsAnimatingBeforeTrigger = true;
        DelayTime = EffectDelay;
    }

    public virtual void AnimatingBeforeTrigger() {
        Debug.Log("Animating Before Trigger");
    }

    public virtual void TriggerEffect() {
        Debug.Log("Super Trigger");
    }
}
