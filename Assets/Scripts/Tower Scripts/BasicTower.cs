using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicTower : Tower
{
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private GameObject detectionRadius;

    private float fireCountdown;

    private void Update() {
        TryShoot();
    }

    public override void ListenToStats() {
        statsManager.getStat(StatsManager.StatTypes.AttackRange).OnStatUpdate += AttackRange_OnStatChange;
    }

    private void AttackRange_OnStatChange(object sender, CharacterStat.OnStatUpdateEventArgs e) {
        SetAttackRange(e.characterStat.Value);
    }

    private void SetAttackRange(float value) {
        detectionRadius.transform.localScale = new Vector3(value, 1, value);
    }

    public override void TryShoot() {
        var fireRate = statsManager.getStat(StatsManager.StatTypes.FireRate).Value;

        if (fireCountdown <= 0f && CheckForTarget() != null) {
            Debug.Log("Shooting");
            Shoot();
            fireCountdown = 1f / fireRate;
        } else {
            fireCountdown -= Time.deltaTime;
        }
    }

    public override void Shoot() {
        var target = CheckForTarget();
        var currentDamage = statsManager.getStat(StatsManager.StatTypes.Damage).Value;
        GameObject bulletFired = Instantiate(BulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
        ProjectileBullet bullet = bulletFired.GetComponent<ProjectileBullet>();
        if (bullet != null) {
            bullet.SetDamage(currentDamage);
            bullet.Seek(target.transform);
        }
    }
}
