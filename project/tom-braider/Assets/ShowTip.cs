using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTip : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayTip() {
        gameObject.SetActive(true);
    }

    public void HideTip() {
        gameObject.SetActive(false);
    }
}
