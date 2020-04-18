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
            DelayTime -= Time.deltaTime;
            if (DelayTime <= 0) {
                TriggerEffect();
                IsTriggerFinished = true;
                IsAnimatingBeforeTrigger = false;
            } else {
                AnimatingBeforeTrigger();
            }
        }
    }

    public void Trigger() {
        IsAnimatingBeforeTrigger = true;
        DelayTime = EffectDelay;
    }

    public virtual void AnimatingBeforeTrigger() {
    }

    public virtual void TriggerEffect() {

    }
}
