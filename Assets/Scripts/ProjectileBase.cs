using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    protected Transform target;
    protected float damage;
    protected BaseBuffSO status;
    [SerializeField] protected float speed = 70f;
    
    public void Seek(Transform _target) {
        target = _target;
    }

    public void SetDamage(float _damage) {
        this.damage = _damage;
    }
    public float GetDamage() {
        return damage;
    }

    public void SetStatus(BaseBuffSO _status) {
            status = _status;
    }

    public void ApplyStatus(BuffManager _target) {
        if (status!= null && _target != null) {
            Debug.Log(_target);
            _target.GetComponent<BuffManager>().AddBuff(status);
        }
    }
}
