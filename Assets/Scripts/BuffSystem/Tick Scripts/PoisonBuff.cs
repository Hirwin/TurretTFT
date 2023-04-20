using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoisonBuff : BaseBuffSO
{
    public PoisonBuff(StatModifier[] statList, float maxDuration, BuffManager.Tick tickFunction, bool stackable, BuffTypeSO statusType) : base(statList, maxDuration, tickFunction, stackable, statusType) {
        this.statList = statList;
        this.maxDuration = maxDuration;
        this.tickFunction = tickFunction;
        this.stackable = stackable;
        this.statusType = statusType;
    }
}
