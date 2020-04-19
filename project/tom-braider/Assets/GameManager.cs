using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    UIManager UI;

    // Start is called before the first frame update
    void Start()
    {
        UI = FindObjectOfType<UIManager>();
        UI.SwitchToPanel(0);
    }

    public void GameOver() {
        UI.SwitchToPanel(1);
    }

    public void RestartGame() {
        SceneManager.LoadScene("GameScene");
    }
}
