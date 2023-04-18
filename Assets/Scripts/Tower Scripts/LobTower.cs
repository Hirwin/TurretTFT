using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LobTower : Tower {
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private GameObject detectionRadius;

    private float fireCountdown;
    private bool targetAquired = false;
    private float gravityManager = 9.81f * 2f;


    float launchSpeed;
    void Awake() {
        OnMortarTurretRangeChange();
    }
    void OnMortarTurretRangeChange() {
        float x = statsManager.getStat(StatsManager.StatTypes.AttackRange).Value + 5.00001f; ;
        float y = shootPoint.transform.position.y;
        launchSpeed = Mathf.Sqrt(9.81f * (y + Mathf.Sqrt(x * x + y * y)));
    }

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
        if (fireCountdown <= 0f && CheckForTarget() != null && !targetAquired) {
            targetAquired = true;
            Shoot();
            fireCountdown = 1f / fireRate;
        } else {
            fireCountdown -= Time.deltaTime;
        }
    }

    public void Launch() {
        var target = CheckForTarget();
        Vector3 launchPoint = shootPoint.transform.position;
        Vector3 targetPoint = target.GetPosition();
        targetPoint.y = 0f;

        Vector2 dir;
        dir.x = targetPoint.x - launchPoint.x;
        dir.y = targetPoint.z - launchPoint.z;
        float x = dir.magnitude;
        float y = -launchPoint.y;
        dir /= x;

        float g = gravityManager;
        float s = launchSpeed;
        float s2 = s * s;

        float r = s2 * s2 - g * (g * x * x + 2f * y * s2);
        Debug.Assert(r >= 0f, "Launch velocity insufficient for range!" + r);
        float tanTheta = (s2 + Mathf.Sqrt(r)) / (g * x);
        float cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
        float sinTheta = cosTheta * tanTheta;

        FireShell(launchPoint, targetPoint, new Vector3(s * cosTheta * dir.x, s * sinTheta, s * cosTheta * dir.y));
    }

    public void FireShell(Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVelocity ) {
        var currentDamage = statsManager.getStat(StatsManager.StatTypes.Damage).Value;
        GameObject bulletFired = Instantiate(BulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
        ProjectileMortar bullet = bulletFired.GetComponent<ProjectileMortar>();;
        if (bullet != null) {
            Debug.Log(bullet+ ": Bullet Prefab");
            bullet.Initialize(launchPoint, targetPoint, launchVelocity);
            bullet.SetDamage(currentDamage);
            bullet.Seek(CheckForTarget().transform);
        }
    }

    public override void Shoot() {
        if (CheckForTarget() != null) {
            Launch();
            Debug.Log("Fired");
            targetAquired = false;
            Debug.Log(targetAquired);
        } else {
        }
    }
}
