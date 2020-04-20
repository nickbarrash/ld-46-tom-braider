using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpikeCoordinator : MonoBehaviour
{
    public List<WallSpike> Spikes;

    public float AnimationOffset;
    public float RetractDuration;
    public float RetractPauseDuration;
    public float ExtrudeDuration;
    public float ExtractPauseDuration;

    public float CycleModulo(float Offset, float SetTime) {
        return (SetTime + Offset) % (RetractDuration + RetractPauseDuration + ExtrudeDuration + ExtractPauseDuration);
    }

    // Update is called once per frame
    void Update()
    {
        float MyTime = Time.time;
        for(int index = 0; index < Spikes.Count; index++) {
            float CycleTime = CycleModulo(index * AnimationOffset, MyTime);

            if (CycleTime > RetractDuration) {
                CycleTime -= RetractDuration;
                if (CycleTime > RetractPauseDuration) {
                    CycleTime -= RetractPauseDuration;
                    if (CycleTime > ExtrudeDuration) {
                        CycleTime -= ExtrudeDuration;
                        if (CycleTime > ExtractPauseDuration) {
                        } else {
                            // Retracted Pause
                            Spikes[index].SetSpikeFromT(1);
                        }
                    } else {
                        // Retract
                        Spikes[index].SetSpikeFromT(RetractT(CycleTime));
                    }
                } else {
                    // Extruded Pause
                    Spikes[index].SetSpikeFromT(0);
                }
            } else {
                // Extrude
                Spikes[index].SetSpikeFromT(ExtrudeT(CycleTime));
            }
        }
    }

    public float ExtrudeT(float ExtrudeTime) {
        return Mathf.InverseLerp(RetractDuration, 0, ExtrudeTime);
    }

    public float RetractT(float RetractTime) {
        return Mathf.InverseLerp(0, ExtrudeDuration, RetractTime);
    }
}
