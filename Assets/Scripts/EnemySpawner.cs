using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform objective;

    //Test

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy() {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
        enemy.GetComponent<Enemy>().setObjective(objective);
        enemy.GetComponent<Enemy>().SetSpawnPoint(spawnPoint.transform);
    }
}
