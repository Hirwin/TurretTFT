using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : Buffable, IDamagable, IHasStats
{
    [SerializeField]private NavMeshAgent enemyAgent;
    [SerializeField]private Transform objective;
    [SerializeField]private Health health;
    [SerializeField]private Transform spawnPoint;
    [SerializeField]private Transform holdPoint;
    private GameObject itemHeld;

    public int BaseHealth { get; set; }

    public event Action<Enemy> OnDeath;

    public void ListenToStats() {
        statsManager.getStat(StatsManager.StatTypes.MoveSpeed).OnStatUpdate += EnemyMove_OnStatChange;
        health.Initialize();
    }

    private void EnemyMove_OnStatChange(object sender, CharacterStat.OnStatUpdateEventArgs e) {
        enemyAgent.speed = e.characterStat.Value;
        Debug.Log("Movespeed changing value to:" + e.characterStat.Value);
    }

    private void DropHolding() {
        if (itemHeld != null) {
            itemHeld = null;
        }
    }

    private IEnumerator DeathRoutine() {
        DropHolding();
        OnDeath?.Invoke(this);
        Destroy(gameObject);
        yield return new WaitForSeconds(1f);
    }

    public void TakeDamage(float damage) {
        health.TakeDamage(damage);
    }
    public void BeginDeath() {
        StartCoroutine(DeathRoutine());
    }

    public void setObjective(Transform objective) {
        this.objective = objective;
        enemyAgent.destination = objective.position;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void StealSuccess(GameObject stealTarget) {
        if (spawnPoint != null) {
            setObjective(spawnPoint);
        }
        stealTarget.GetComponent<BookInteract>().Steal(this);
        //Create a book stolen event trigger to listen to here.
    }

    public void SetSpawnPoint(Transform spawnPoint) {
        this.spawnPoint = spawnPoint;
    }
    public Transform GetStolenItemFollowTransform() {
        return holdPoint;
    }

}
