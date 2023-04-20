using System;
using System.Collections.Generic;
using UnityEngine;
public abstract class BaseBuffSO
{
    [SerializeField] protected StatModifier[] statList;
    [SerializeField] protected float maxDuration = -1;
    [SerializeField] public bool stackable;
    protected BuffManager.Tick tickFunction;
    protected BuffTypeSO statusType;
    
    protected float duration;
    protected Buffable statsOwner;

    public BaseBuffSO(StatModifier[] statList, float maxDuration, BuffManager.Tick tickFunction, bool stackable, BuffTypeSO statusType) {
        this.statList = statList;
        this.maxDuration = maxDuration;
        this.tickFunction = tickFunction;
        this.stackable = stackable;
        this.duration = maxDuration;
        this.statusType = statusType;
    }


    //We are going to want to create scriptable objects for the STatus Type, the same for stats themselves, the status type will probably be blank, and will be ingested by the factory initially, then the comparisons will search for that information, I think


    public event EventHandler<OnDurationEndEventArgs> OnDurationEnd;
    public class OnDurationEndEventArgs : EventArgs {
        public BaseBuffSO buff;
    }
    public virtual void StackBuff() {
       durationReset();
    }

    public StatModifier[] GetStatList() {
        return statList;
    }

    public float GetMaxDuration() {
        return maxDuration;
    }

    public BuffTypeSO GetStatusType() {
        return statusType;
    }
    
    protected void durationReset() {
        duration = maxDuration;
    }
    public void OnTick() {
        Debug.Log("We Got There");
        if (duration == -1) return;
            tickFunction(statsOwner);
        duration--;
        Debug.Log(duration);
        if (duration <= 0) {
            OnDurationEnd?.Invoke(this, new OnDurationEndEventArgs {
                buff = this
            });
            return;
        } else {
            tickFunction(statsOwner);
        }
    }

    public void SetBuffHolder(Buffable statsOwner) {
        this.statsOwner = statsOwner;
    }
}
