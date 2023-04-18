using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    [SerializeField] GameObject BookPrefab;

    public void AttemptToStealItem(Enemy target) {
        var spawnedItem = Instantiate(BookPrefab);
        target.StealSuccess(spawnedItem);

    }
}
