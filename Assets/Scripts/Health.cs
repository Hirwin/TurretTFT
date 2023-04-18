using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;

    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;
    public class OnHealthChangedEventArgs : EventArgs {
        public float healthNormalized;
    }
    private void Awake() {
        maxHealth = 100f;
        currentHealth = maxHealth;
    }

    public void Initialize() {
        enemy.GetComponent<StatsManager>().getStat(StatsManager.StatTypes.Health).OnStatUpdate += Health_OnStatChange;
    }

    private void Health_OnStatChange(object stat, CharacterStat.OnStatUpdateEventArgs e) {
        NewHealth(e.characterStat.Value);
    }

    public void NewHealth(float newMaxHealth) {
        var tempPastMax = maxHealth;
        maxHealth = newMaxHealth;
        currentHealth = (currentHealth / tempPastMax) * maxHealth;
        Debug.Log("Current HEalth: " + currentHealth + "Previous Max" + tempPastMax );
        OnHealthChanged?.Invoke(this, new Health.OnHealthChangedEventArgs {
            healthNormalized = currentHealth / maxHealth
        });
    }
    
    public void TakeDamage(float damage) {
        currentHealth -= damage;
        OnHealthChanged?.Invoke(this, new Health.OnHealthChangedEventArgs {
            healthNormalized = currentHealth/maxHealth
        });
        if (currentHealth <= 0) {
            enemy.BeginDeath();
        }
    }


}
