using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectEndGame : EffectOnStayDepress {

    Follow Follower;

    // Start is called before the first frame update
    void Start()
    {
        Follower = FindObjectOfType<Follow>();
    }

    public override void StartEffect() {
        Follower.EndGame();
    }
}
