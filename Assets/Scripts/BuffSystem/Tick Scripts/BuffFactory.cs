using UnityEngine.TextCore.Text;
using UnityEngine;
using System;


public abstract class BuffFactory<T>: MonoBehaviour where T : BaseBuffSO {
    [SerializeField] protected StatsManager statsManager;
    [SerializeField] protected StatModifier[] statList;
    [SerializeField] protected float maxDuration = -1;
    [SerializeField] protected bool stackable;
    [SerializeField] protected BuffTypeSO statusType;
    public virtual T CreateBuff() {
        T _Buff = null;
        return _Buff;
    }

    public void setStatsManager(StatsManager statsManager) {
        this.statsManager = statsManager;
    }
}