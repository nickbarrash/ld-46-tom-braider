using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnStayDepressFire : EffectOnStayDepress {
    private EffectDamage Effect;

    // Start is called before the first frame update
    void Start()
    {
        Effect = transform.Find("EffectTrigger").GetComponent<EffectDamage>();
        gameObject.SetActive(false);
    }

    public override void StartEffect() {
        Effect.ResetDamageTimer();
        gameObject.SetActive(true);
    }

    public override void StopEffect() {
        gameObject.SetActive(false);
    }
}
