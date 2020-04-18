using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnStayDepressFire : EffectOnStayDepress {
    private EffectFireDamage Effect;

    public bool AnyTrigger = true;
    private int ActiveTriggerCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Effect = transform.Find("EffectTrigger").GetComponent<EffectFireDamage>();
        gameObject.SetActive(false);
    }

    public override void StartEffect() {
        if (ActiveTriggerCount++ == 0) {
            Effect.ResetDamageTimer();
            gameObject.SetActive(true);
        }
    }

    public override void StopEffect() {
        if (--ActiveTriggerCount == 0) {
            gameObject.SetActive(false);
        }
    }
}
