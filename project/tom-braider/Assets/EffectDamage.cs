﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDamage : MonoBehaviour {
    public float DamageInterval = 0.2f;
    public float Damage = 0.2f;
    public string DamageType;

    Dictionary<string, float> CharacterTickTimes = new Dictionary<string, float>();

    PersistentSettings settings;

    private void Start() {
        settings = FindObjectOfType<PersistentSettings>();
        ResetDamageTimer();
    }

    public void ResetDamageTimer() {
        CharacterTickTimes.Clear();
    }

    private void OnTriggerStay(Collider other) {
        DamageCharacter(other.gameObject);
    }

    private void OnCollisionStay(Collision collision) {
        DamageCharacter(collision.collider.gameObject);
    }

    private void DamageCharacter(GameObject character) {
        if (GameConstants.IsCharacterLayer(character.layer)) {
            float TickTime = 0f;
            string Name = character.name;
            if (CharacterTickTimes.ContainsKey(Name)) {
                TickTime = CharacterTickTimes[Name];
            }

            TickTime -= Time.deltaTime;

            if (TickTime <= 0) {
                TickTime = DamageInterval - TickTime;

                float damage = settings.HardMode ? 99 : Damage;

                character.GetComponent<CharacterHealth>().Damage(damage);
            }
            CharacterTickTimes[Name] = TickTime;
        }
    }
}
