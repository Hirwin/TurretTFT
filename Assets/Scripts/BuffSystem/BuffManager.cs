using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class BuffManager : MonoBehaviour {
    [SerializeField] private List<BaseBuffSO> buffList;
    [SerializeField] private StatsManager statsManager;
    [SerializeField] private Buffable statsOwner;
    public delegate void Tick(Buffable statsOwner);
    protected Tick tickFunction;
    private void Awake() {
        buffList = new List<BaseBuffSO>();
        StartCoroutine(OnTick());
    }
    private void Update() {
    }
    IEnumerator OnTick() {
        Debug.Log("Starting in the manager");
        for (; ; ) {
            if (buffList.Count > 0) {
                Debug.Log(buffList.Count);
                for (var i = 0; i < buffList.Count; i++){
                    buffList[i].OnTick();
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public void AddBuff(BaseBuffSO status) {
        BaseBuffSO stackableStatus = checkForStatusType(status.GetStatusType());
        if (stackableStatus == null) {
                Debug.Log(status + " " + buffList);
                buffList.Add(status);
                status.SetBuffHolder(statsOwner);
                ApplyStats(status.GetStatList());
                status.OnDurationEnd += Buff_OnBuffDurationEnd;
           } else if (stackableStatus.stackable){
                stackableStatus.StackBuff();
           }

    }

    private void Buff_OnBuffDurationEnd(object sender, BaseBuffSO.OnDurationEndEventArgs e) {
        RemoveBuff(e.buff);
    }

    public BaseBuffSO checkForStatusType(BuffTypeSO statusType) {
        if (buffList.Count == 0) { return null; }
        foreach (BaseBuffSO buff in buffList) {
            if (buff.GetStatusType() == statusType) {
                return buff;
            }
        }
        return null;
    }

    public void RemoveBuff(BaseBuffSO _buff) {
        var check = buffList.Contains(_buff);
        if (check) {
            RemoveStats(_buff.GetStatList());
            buffList.Remove(_buff);
        }
    }

    public void ApplyStats(StatModifier[] statList) {
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
