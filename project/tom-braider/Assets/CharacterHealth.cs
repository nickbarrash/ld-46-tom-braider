using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public HealthBar HealthAffordance;

    private float Health = 1f;

    public void Damage(float Damage) {
        Health -= Damage;

        if (Health <= 0) {
            HealthAffordance.SetHealth(Health);
        }
    }
}
