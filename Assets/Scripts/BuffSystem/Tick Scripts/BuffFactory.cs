using UnityEngine.TextCore.Text;
using UnityEngine;
using System;

public abstract class BuffFactory: MonoBehaviour{
    [SerializeField] private StatsManager statsManager;
    [SerializeField] private StatModifier[] statList;
    [SerializeField] private float maxDuration = -1;
    [SerializeField] private TickBase tickFunction;
    [SerializeField] public bool stackable;
    public virtual BaseBuffSO CreateBuff() {
        BaseBuffSO _Buff = new BaseBuffSO(statList, maxDuration, tickFunction, stackable);
        return _Buff;
    }
}