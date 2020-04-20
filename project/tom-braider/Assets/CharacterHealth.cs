using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public HealthBar HealthAffordance;
    private GameManager Manager;

    private float Health = 1f;

    private void Start() {
        Manager = FindObjectOfType<GameManager>();
    }

    public void Damage(float Damage) {
        Health -= Damage;
        HealthAffordance.SetHealth(Health);

        if (Health <= 0) {
            Manager.GameOver();
        }
    }
}
