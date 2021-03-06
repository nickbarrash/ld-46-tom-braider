﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectOnStayDepress : MonoBehaviour
{
    public int DepressThreshold = 1;
    protected int DepressCount = 0;

    public virtual void AddDepress(Collider other) {
        Debug.Log("Add " + DepressCount);
        if (++DepressCount == DepressThreshold) {
            StartEffect();
        }
    }

    public virtual void RemoveDepress(Collider other) {
        Debug.Log("Sub " + DepressCount);
        if (DepressCount-- == DepressThreshold) {
            StopEffect();
        }
    }

    public virtual void StartEffect() {

    }

    public virtual void StopEffect() {

    }
}
