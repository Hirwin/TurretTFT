using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class PoisonDebuffFactory : BuffFactory<PoisonBuff> {

    public override PoisonBuff CreateBuff() {
        PoisonBuff _Buff = new PoisonBuff(statList, maxDuration, tickFunction, stackable);
        return _Buff;
    }

}