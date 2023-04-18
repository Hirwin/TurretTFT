using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArtifactType {
    Basic
}
public enum ArtifactClass {
    Fighter,
    Mage
}

[CreateAssetMenu(fileName = "New Artifact")]
public class Artifact : ScriptableObject {
    public int uniqueID;
    [Space]
    public string artifactName;
    public string description;
    public Sprite artwork;
    [Space]
    public int StrengthBonus;
    public int HealthBonus;
    public int IntelligenceBonus;
    public int ArmorBonus;
    [Space]
    public float StrengthPercent;
    public float HealthPercent;
    public float IntelligencePercent;
    public float ArmorPercent;
    [Space]
    public ArtifactType type;
    public ArtifactClass artClass;

    public void Print() {
        Debug.Log(artifactName + ": " + description);
    }

    public void Equip(StatsManager c) {

    }

    public void Unequip(StatsManager c) {

    }

}
