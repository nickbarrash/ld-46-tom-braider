using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    UIManager UI;

    bool IsVictory = false;

    ShowTip WASDTip;
    float ShowTipTimer = 10f;

    bool TipShown = false;
    bool TipHidden = false;

    public void Awake() {
        WASDTip = GameObject.Find("MoveTom").GetComponent<ShowTip>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowTip[] tips = FindObjectsOfType<ShowTip>();
        foreach (ShowTip tip in tips) {
            if (tip.gameObject.name != "MoveTom") {
                tip.gameObject.SetActive(false);
            } else {
                tip.gameObject.SetActive(true);
                TipShown = true;
            }
        }

        IsVictory = false;
        UI = FindObjectOfType<UIManager>();
        UI.SwitchToPanel(0);
    }

    private void Update() {
        if (!TipHidden && TipShown) {
            if (ShowTipTimer > 0) {
                ShowTipTimer -= Time.deltaTime;
            } else {
                TipHidden = true;
                WASDTip.HideTip();
            }
        }
    }

    public void GameOver() {
        if (!IsVictory) {
            UI.SwitchToPanel(1);
        }
    }

    public void Victory() {
        IsVictory = true;
        UI.SwitchToPanel(2);
    }

    public void RestartNoModeChange() {
        RestartGame(FindObjectOfType<PersistentSettings>().HardMode);
    }

    public void RestartGame(bool IsHardMode) {
        IsVictory = false;
        FindObjectOfType<PersistentSettings>().HardMode = IsHardMode;
        SceneManager.LoadScene("GameScene");
    }
}
