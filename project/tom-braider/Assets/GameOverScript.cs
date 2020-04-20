using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    GameManager manager;

    private void Start() {
        manager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        manager.Victory();
    }
}
