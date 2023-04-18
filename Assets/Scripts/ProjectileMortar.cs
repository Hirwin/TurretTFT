using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class ProjectileMortar : ProjectileBase
{
    [SerializeField] GameObject detonatePrefab;
    private float age;

    Vector3 launchPoint, targetPoint, launchVelocity;

    public void Initialize(
        Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVelocity
    ) {
        this.launchPoint = launchPoint;
        this.targetPoint = targetPoint;
        this.launchVelocity = launchVelocity;
    }

    private void Update() {
        age += Time.deltaTime;
        Vector3 p = launchPoint + launchVelocity * age;
        p.y -= 0.5f * (9.81f * 2f) * age * age;

        if (p.y <= 0f) {
            TargetHit();
            return;
        }

        transform.localPosition = p;
        Vector3 d = launchVelocity;
        d.y -= 9.81f * 2f * age;
        transform.localRotation = Quaternion.LookRotation(d);
    }

    protected virtual void Detonate() {
        GameObject blastZone = Instantiate(detonatePrefab, transform.position, Quaternion.identity);
        Detonation bullet = blastZone.GetComponent<Detonation>(); ;
        bullet.Initialize(GetDamage());
        Debug.Log("Boom Baybeeee");
    }
    private void TargetHit() {
        Detonate();
        Destroy(gameObject);
    }


}
