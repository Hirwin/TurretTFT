using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : Buffable, IHasStats
{
    private Vector3 projectileShootFromPosition;
    [SerializeField]private List<Enemy> targetsInRange = new();
    [SerializeField]private Enemy currentTarget;
    [SerializeField]private TargetStyle currentTargetStyle = TargetStyle.First;

    [SerializeField]protected StatsManager statsManager;
    private TargetStyle _previousTargetStyle;

    private void Awake() {
        _previousTargetStyle = currentTargetStyle;
    }

    public virtual void ListenToStats() {
        Debug.Log("No Stats provided");   
    }

    public Enemy CheckForTarget() {
        
        if (_previousTargetStyle != currentTargetStyle) {
            HandleTargetStyleSwitch();
        }
        GetCurrentTarget();
        if (currentTarget != null) {
            return currentTarget;
        } else {
            return null;
        }
    }

    private void HandleTargetDeath(Enemy enemy) {
        RemoveTargetFromInRangeList(enemy);
    }

    public void AddtargetToInRangeList(Enemy target) {
        targetsInRange.Add(target);
        target.OnDeath += HandleTargetDeath;
        GetCurrentTarget();
    }

    public void RemoveTargetFromInRangeList(Enemy target) {
        targetsInRange.Remove(target);
        target.OnDeath -= HandleTargetDeath;
        GetCurrentTarget();
    }

    private void GetCurrentTarget() {
        if (targetsInRange.Count <= 0) {
            currentTarget = null;
            return;
        } else {
            currentTarget = currentTargetStyle switch {
                TargetStyle.First => targetsInRange.First(),
                TargetStyle.Last => targetsInRange.Last(),
                TargetStyle.Strong => targetsInRange.OrderBy(e => e.BaseHealth).First(),
                TargetStyle.Weak => targetsInRange.OrderBy(e => e.BaseHealth).Last(),
                _ => targetsInRange.First()
            };
        }

    }

    private void HandleTargetStyleSwitch() {
        _previousTargetStyle = currentTargetStyle;
        GetCurrentTarget();
    }

    public virtual void Shoot() {
        Debug.Log("Default Shoot, Pew Pew");
    }

    public virtual void TryShoot() {
        Debug.Log("Default TryShoot, Who Who?");
    }

    public enum TargetStyle {
        First, Last, Strong, Weak
    }


}