using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental;
using UnityEngine.AI;
using Unity.VisualScripting;

public class StatsManager : MonoBehaviour {

    //[SerializeField] StatPanel statPanel;
    public enum StatTypes { Damage, Health, MoveSpeed, FireRate, AttackRange } 

    public GameObject character;
    //[SerializeField] private List<CharacterStat> statList = new List<CharacterStat>();
    [SerializeField] private CharacterStat[] statTypeList;
    public IDictionary<StatTypes, CharacterStat> statMap = new Dictionary<StatTypes, CharacterStat>();
    public IDictionary<int, Artifact> artifactList = new Dictionary<int, Artifact>();

    private void Awake() {
        statMapInitiate();
        character.GetComponent<IHasStats>().ListenToStats();
    }

    private void Start() {
        pushStats();
    }

    private void statMapInitiate() {
        foreach (var stat in statTypeList) {
            statMap.Add(stat.StatType, stat);
        }
    }

    public void addArtifact(Artifact artifact) {//These artifact things should probably be listeners to  an artifact manager that calls when a new artifact is added or something
        artifactList.Add(artifact.uniqueID, artifact);
        artifact.Equip(this);
        updateArtifacts();
    }

    public void removeArtifact(Artifact artifact) {
        artifactList.Remove(artifact.uniqueID);
        artifact.Unequip(this);
        updateArtifacts();
    }

    public CharacterStat getStat(StatTypes statType) {
        return statMap[statType];
    }
    public float getStatValue(StatTypes statType) {
        Debug.Log(statMap[statType].Value);
        return statMap[statType].Value;
    }

    public void AddStatMod(StatModifier statMod) {
        var stat = statMod.StatType;
        getStat(stat).AddModifier(statMod);
    }

    public void RemoveStatMod(StatModifier statMod) {
        var stat = statMod.StatType;
        getStat(stat).RemoveModifier(statMod);
    }

    public void updateArtifacts() {
       // statPanel.UpdateStatValues();
        pushStats();
        foreach (KeyValuePair<int, Artifact> kvp in artifactList) {
            Debug.Log("Key = " + kvp.Key + " Value = " + kvp.Value);
        }
    }

    public void pushStats() {
        foreach (KeyValuePair<StatTypes, CharacterStat> kvp in statMap) {
            kvp.Value.PushStat();
            Debug.Log("Pushing " + kvp.Value.StatType);
        }
    }

}