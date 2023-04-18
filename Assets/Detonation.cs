using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Detonation : MonoBehaviour
{
    [SerializeField] VisualEffect explosion;
    [SerializeField] GameObject aoePool;
    [SerializeField] float radius;
    [SerializeField] float damage;
    [SerializeField] float lifeTime = 5;
    private bool exploded = false;
    private void Start() {
        Destroy(gameObject, lifeTime);
    }
    public void Initialize(float _damage) {
        //this.radius = _radius;
        this.damage = _damage;
        transform.localScale = new Vector3(radius/4, radius/4, radius/4);
    }

    private void Update() {
        if (!exploded) {
            Explode();
        }
    }

    private void Explode() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders) {
            var enemy = collider.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.TakeDamage(damage);  
            }
        }
        exploded = true;
    }
}
