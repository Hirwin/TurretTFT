using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : ProjectileBase
{
    private void OnTriggerEnter(Collider other) {
        if (other.transform == target){
            ApplyStatus(other.GetComponent<BuffManager>());
            other.GetComponent<IDamagable>().TakeDamage(GetDamage());
            Destroy(gameObject);
        }
    }

    private void AttackTarget() {
        if (target == null) {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame) {
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void Update() {
        AttackTarget();
    }
}
