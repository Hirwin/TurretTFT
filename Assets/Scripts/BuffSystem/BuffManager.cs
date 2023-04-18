using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuffManager : MonoBehaviour {
    [SerializeField] private List<BaseBuffSO> buffList;
    [SerializeField] private StatsManager statsManager;
    [SerializeField] private Buffable statsOwner;

    private void Update() {
        foreach (BaseBuffSO buff in buffList) {
            //buff.OnTick(statsOwner);
        }
    }

    public void AddBuff(BaseBuffSO[] _buff) {
        foreach (BaseBuffSO status in _buff) {
            if (!buffList.Contains(status)) {
                buffList.Add(status);
                status.SetBuffHolder(statsOwner);
                status.ApplyStats(statsManager);
                status.OnDurationEnd += Buff_OnBuffDurationEnd;
            } else {
                status.StackBuff();
            }
        }
    }

    private void Buff_OnBuffDurationEnd(object sender, BaseBuffSO.OnDurationEndEventArgs e) {
        RemoveBuff(e.buff);
    }

    public void RemoveBuff(BaseBuffSO _buff) {
        var check = buffList.Contains(_buff);
        if (check) {
            _buff.RemoveStats(statsManager);
            buffList.Remove(_buff);
        }
    }

}
