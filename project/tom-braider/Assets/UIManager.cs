using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<GameObject> Panels;

    public void SwitchToPanel(int PanelIndex) {
        foreach(GameObject Panel in Panels) {
            if (Panel != null) {
                Panel.SetActive(false);
                StartScript(Panel);
            }
        }
        Panels[PanelIndex].SetActive(true);
    }

    public void StartScript(GameObject Panel) {
        BackstoryController controller = Panel.GetComponent<BackstoryController>();
        if (controller != null) {
            controller.StartAnimation();
        }
    }
}
