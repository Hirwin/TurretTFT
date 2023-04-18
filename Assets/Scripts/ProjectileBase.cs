using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    protected Transform target;
    protected float damage;
    [SerializeField] protected float speed = 70f;
    [SerializeField] protected BaseBuffSO[] OnApply;
    
    public void Seek(Transform _target) {
        target = _target;
    }

    public void SetDamage(float _damage) {
        this.damage = _damage;
    }
    public float GetDamage() {
        return damage;
    }

    public void ApplyStatus(BuffManager _target) {
        if (OnApply != null && _target != null) {
            Debug.Log(_target);
            _target.GetComponent<BuffManager>().AddBuff(OnApply);
        }
    }
}
