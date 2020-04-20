using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpaceTip : MonoBehaviour
{
    bool TipFromTrigger = false;
    bool TipShown = false;

    private float ShowTipDuration = 10f;
    private float ShowTipTimer;

    ShowTip SpaceTip;
    public void Awake() {
        SpaceTip = GameObject.Find("FollowSpace").GetComponent<ShowTip>();
    }

    private void Update() {
        if (TipShown) {
            if (ShowTipTimer > 0) {
                ShowTipTimer -= Time.deltaTime;
            } else {
                TipShown = false;
                SpaceTip.HideTip();
            }
        }
    }

    public void TriggerTip() {
        if (!TipShown) {
            ShowTipTimer = ShowTipDuration;
            SpaceTip.DisplayTip();
            TipShown = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!TipFromTrigger) {
            TipFromTrigger = true;
            TriggerTip();
        }
    }
}
