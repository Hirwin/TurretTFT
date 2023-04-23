using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class FrostBuffFactory : BuffFactory<FrostBuff> {
    protected BuffManager.Tick tickFunction;
    private int frostStacks;
    [SerializeField]private StatModifier Frozen;
    public void OnTick(Buffable statsOwner) {
        IDamagable target = statsOwner.GetComponent<IDamagable>();
        if (target != null && frostStacks >= 10) {
            target.TakeDamage(statsManager.getStat(StatsManager.StatTypes.PoisonTick).Value);
        }
    }

    public virtual void StackBuff() {
        frostStacks++;
    }

    public override FrostBuff CreateBuff() {
        tickFunction = OnTick;
        FrostBuff _Buff = new FrostBuff(statList, maxDuration, tickFunction, stackable, statusType);
        return _Buff;
    }

}

public class FrostBuff : BaseBuffSO {
    public FrostBuff(StatModifier[] statList, float maxDuration, BuffManager.Tick tickFunction, bool stackable, BuffTypeSO statusType) : base(statList, maxDuration, tickFunction, stackable, statusType) {
        this.statList = statList;
        this.maxDuration = maxDuration;
        this.tickFunction = tickFunction;
        this.stackable = stackable;
        this.statusType = statusType;
    }
}
