using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectWallDop : MonoBehaviour
{
    private bool IsWallFlipped = false;

    public float WallFlipDuration;

    private float WallFlipTime;

    private void Start() {
        WallFlipTime = WallFlipDuration;
        SetRotation(0);
    }

    public void Update() {
        if (IsWallFlipped && WallFlipTime > 0f) {
            WallFlipTime -= Time.deltaTime;
            SetRotation(Mathf.InverseLerp(WallFlipDuration, 0, WallFlipTime));
        }
    }

    public void SetRotation(float t) {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90 * t));
    }

    public void TriggerWallFlip() {
        IsWallFlipped = true;
    }
}
