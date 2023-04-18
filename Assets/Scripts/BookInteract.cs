using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BookInteract : MonoBehaviour
{
    [SerializeField] DropEffect dropEffectTrigger;
    public void Steal(Enemy enemy) {
        transform.parent = enemy.GetStolenItemFollowTransform();
        transform.localPosition = Vector3.zero;
        enemy.OnDeath += DropBook;
    }

    private void DropBook(Enemy obj) {
        transform.parent = null;
        dropEffectTrigger.TryDropItem();
    }
}
