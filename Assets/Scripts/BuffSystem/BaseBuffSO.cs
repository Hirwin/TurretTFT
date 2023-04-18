using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuffSO
{
    [SerializeField] protected StatModifier[] statList;
    [SerializeField] protected float maxDuration = -1;
    [SerializeField] protected TickBase tickFunction;
    [SerializeField] public bool stackable;
    //We are going to want to create scriptable objects for the STatus Type, the same for stats themselves, the status type will probably be blank, and will be ingested by the factory initially, then the comparisons will search for that information, I think

    public BaseBuffSO(StatModifier[] statList, float maxDuration, TickBase tickFunction, bool stackable) {
        this.statList = statList;
        this.maxDuration = maxDuration;
        this.tickFunction = tickFunction;
        this.stackable = stackable;
    }

    protected float duration;
    protected Buffable statsOwner;

    public event EventHandler<OnDurationEndEventArgs> OnDurationEnd;
    public class OnDurationEndEventArgs : EventArgs {
        public BaseBuffSO buff;
    }
    public virtual void StackBuff() {
       // durationReset();
    }

    public StatModifier[] GetStatList() {
        return statList;
    }

    public float GetMaxDuration() {
        return maxDuration;
    }
    protected void durationReset() {
        duration = maxDuration;
    }
    public void OnTick() {
        if (duration == -1) return;
            tickFunction.OnTick(statsOwner);
        duration--;
        if (duration <= 0) {
            OnDurationEnd?.Invoke(this, new OnDurationEndEventArgs {
                buff = this
            });
            return;
        } else {
            tickFunction.OnTick(statsOwner);
        }
    }

    public void SetBuffHolder(Buffable statsOwner) {
        this.statsOwner = statsOwner;
    }
}
