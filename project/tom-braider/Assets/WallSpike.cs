using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpike : MonoBehaviour
{
    public bool Inverse = false;

    public float ExtrudeDuration = 0.05f;
    public float RetractDuration = 0.75f;
    public float RetractedPauseDuration = 0.1f;
    public float ExtrudedPauseDuration = 0.1f;
    public float CycleOffset = 0f;

    private float ExtrudeTimer = 0f;
    private float RetractTimer = 0f;
    private float RetractedPauseTimer = 0f;
    private float ExtrudedPauseTimer = 0f;
    private float CycleTimer = 0f;

    private float SPIKE_X_MOVE = -1f; // hard coded!!
    private float InitialX;

    private bool IsStart = true;

    void Start() {
        CycleTimer = CycleOffset;
        InitialX = transform.localPosition.x;

        if (Inverse) {
            foreach (Transform child in transform) {
                child.localPosition = new Vector3(child.localPosition.x * -1f, child.localPosition.y, child.localPosition.z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CycleTimer > 0f || IsStart) {
            IsStart = false;
            CycleTimer -= Time.deltaTime;
            if (CycleTimer <= 0f) {
                RetractTimer = RetractDuration - CycleTimer;
                CycleTimer = 0f;
            }
        } else if (RetractTimer > 0f) {
            RetractTimer -= Time.deltaTime;
            Retract();
            if (RetractTimer <= 0f) {
                RetractedPauseTimer = RetractedPauseDuration - RetractTimer;
                RetractTimer = 0f;
            }
        } else if (RetractedPauseTimer > 0f) {
            RetractedPauseTimer -= Time.deltaTime;
            if (RetractedPauseTimer <= 0f) {
                ExtrudeTimer = ExtrudeDuration - RetractedPauseTimer;
                RetractedPauseTimer = 0f;
            }
        } else if (ExtrudeTimer > 0f) {
            ExtrudeTimer -= Time.deltaTime;
            Extrude();
            if (ExtrudeTimer <= 0f) {
                ExtrudedPauseTimer = ExtrudedPauseDuration - ExtrudeTimer;
                ExtrudeTimer = 0f;
            }
        } else if (ExtrudedPauseTimer > 0f) {
            ExtrudedPauseTimer -= Time.deltaTime;
            if (ExtrudedPauseTimer <= 0f) {
                RetractTimer = RetractDuration - ExtrudedPauseTimer;
                ExtrudedPauseTimer = 0f;
            }
        }
    }

    public void Extrude() {
        float t = Mathf.InverseLerp(ExtrudeDuration, 0, ExtrudeTimer);
        SetSpikeFromT(t);
    }

    public void SetSpikeFromT(float t) {
        transform.localScale = new Vector3(t, transform.localScale.y, transform.localScale.z);

        float tmpT = Inverse ? t : 1 - t;
        transform.localPosition = new Vector3(InitialX + tmpT * SPIKE_X_MOVE, transform.localPosition.y, transform.localPosition.z);
    }

    public void Retract() {
        float t = Mathf.InverseLerp(0, RetractDuration, RetractTimer);
        SetSpikeFromT(t);
    }
}
