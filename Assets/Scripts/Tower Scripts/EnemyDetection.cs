using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{

    [SerializeField]private Tower tower;

    private void Start() {
        tower ??= GetComponent<Tower>();
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Enter");
        Debug.Log(tower);
        if (other.CompareTag("Enemy")) {
            tower.AddtargetToInRangeList(other.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Enemy")) {
            Debug.Log("Exit");
            tower.RemoveTargetFromInRangeList(other.GetComponent<Enemy>());
        }
    }
}
