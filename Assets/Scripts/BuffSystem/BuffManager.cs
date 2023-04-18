using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class BuffManager : MonoBehaviour {
    private List<BaseBuffSO> buffList;
    [SerializeField] private StatsManager statsManager;
    [SerializeField] private Buffable statsOwner;

    private void Awake() {
        buffList = new List<BaseBuffSO>();
    }
    private void Update() {
        /*if (buffList.Count > 0) {
            foreach (BaseBuffSO buff in buffList) {
                //buff.OnTick(statsOwner);
            }
        }*/
    }

    public void AddBuff(BaseBuffSO status) {
        // if (!buffList.Contains(status)) {
                Debug.Log(status + " " + buffList);
                buffList.Add(status);
                status.SetBuffHolder(statsOwner);
                ApplyStats(status.GetStatList(), status.GetMaxDuration());
                status.OnDurationEnd += Buff_OnBuffDurationEnd;
           // } else {
           //     status.StackBuff();
          //  }
    }

    private void Buff_OnBuffDurationEnd(object sender, BaseBuffSO.OnDurationEndEventArgs e) {
        RemoveBuff(e.buff);
    }

    public void RemoveBuff(BaseBuffSO _buff) {
        var check = buffList.Contains(_buff);
        if (check) {
            RemoveStats(_buff.GetStatList());
            buffList.Remove(_buff);
        }
    }

    public void ApplyStats(StatModifier[] statList, float maxDuration) {
        float duration = maxDuration;
        if (statList.Length != 0) {
            foreach (StatModifier stat in statList) {
                statsManager.AddStatMod(stat);
            }
        }
    }
    public void RemoveStats(StatModifier[] statList) {
        if (statList.Length != 0) {
            foreach (StatModifier stat in statList) {
                statsManager.RemoveStatMod(stat);
            }
        }
    }

}
