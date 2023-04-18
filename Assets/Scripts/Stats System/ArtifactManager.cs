using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ArtifactManager : MonoBehaviour {
    public Artifact[] basicArtifacts;
    public Artifact[] fighterArtifacts;
    public Artifact[] mageArtifacts;

    public GameObject player;
    private Artifact selectedArtifact;
    public void Awake() {
    }

   /* public Artifact getArtifact(int i) {
       /* selectedArtifact = null;
        Debug.Log(currentClass);
        switch (currentClass) {
            case characterClasses.fighter:
                if (fighterArtifacts.Length < i) { break; }
                selectedArtifact = fighterArtifacts[i];
                break;
            case characterClasses.mage:
                if (mageArtifacts.Length < i) { break; }
                selectedArtifact = mageArtifacts[i];
                break;
            case characterClasses.ranger:
                break;
            default:
                break;
        }
        return selectedArtifact;
   }*/

    public void AddArtifact(Artifact artifact) {
        player.GetComponentInChildren<StatsManager>().addArtifact(artifact);
    }

}
