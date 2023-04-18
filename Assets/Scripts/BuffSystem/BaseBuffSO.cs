using System;
using UnityEngine;

[System.Serializable]
public class BaseBuffSO
{
    [SerializeField] private StatModifier[] statList;
    [SerializeField] private float maxDuration = -1;
    [SerializeField] private TickBase tickFunction;
    [SerializeField] public bool stackable;

    public BaseBuffSO(StatModifier[] statList, float maxDuration, TickBase tickFunction, bool stackable) {
        this.statList = statList;
        this.maxDuration = maxDuration;
        this.tickFunction = tickFunction;
        this.stackable = stackable;
    }

    private float duration;
    private Buffable statsOwner;

    public event EventHandler<OnDurationEndEventArgs> OnDurationEnd;
    public class OnDurationEndEventArgs : EventArgs {
        public BaseBuffSO buff;
    }
    public void ApplyStats(StatsManager statsManager) {
        duration = maxDuration;
        if (statList.Length != 0) {
            foreach (StatModifier stat in statList) {
                statsManager.AddStatMod(stat); 
            }
        }
    }
    public void RemoveStats(StatsManager statsManager) {
        if (statList.Length != 0) {
            foreach (StatModifier stat in statList) {
                statsManager.RemoveStatMod(stat);
            }
        }
    }
    public void StackBuff() {
       // durationReset();
    }

    private void durationReset() {
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
