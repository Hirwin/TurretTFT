using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoisonBuff : BaseBuffSO
{
    public PoisonBuff(StatModifier[] statList, float maxDuration, BuffFactory<BaseBuffSO>.Tick tickFunction, bool stackable) : base(statList, maxDuration, tickFunction, stackable) {
        this.statList = statList;
        this.maxDuration = maxDuration;
        this.tickFunction = tickFunction;
        this.stackable = stackable;
    }
}
