using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PoisonDebuffFactory : BuffFactory<PoisonBuff> {
    protected BuffManager.Tick tickFunction;
    public void OnTick(Buffable statsOwner) {
        IDamagable target = statsOwner.GetComponent<IDamagable>();
        if (target != null) {
            target.TakeDamage(statsManager.getStat(StatsManager.StatTypes.PoisonTick).Value);
        }
    }
    public override PoisonBuff CreateBuff() {
        tickFunction = OnTick;
        PoisonBuff _Buff = new PoisonBuff(statList, maxDuration, tickFunction, stackable, statusType);
        return _Buff;
    }

}

public class PoisonBuff : BaseBuffSO {
    public PoisonBuff(StatModifier[] statList, float maxDuration, BuffManager.Tick tickFunction, bool stackable, BuffTypeSO statusType) : base(statList, maxDuration, tickFunction, stackable, statusType) {
        this.statList = statList;
        this.maxDuration = maxDuration;
        this.tickFunction = tickFunction;
        this.stackable = stackable;
        this.statusType = statusType;
    }
}
