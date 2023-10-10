using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] enemyTypeReference;

    private GameObject spawnedEnemy;

    private int enemyTypeIndex;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        enemyTypeIndex = 0;
        spawnedEnemy = Instantiate(enemyTypeReference[enemyTypeIndex]);

        spawnedEnemy.transform.position = new Vector3(0, 0, 0);

        enemyTypeIndex = 0;
        spawnedEnemy = Instantiate(enemyTypeReference[enemyTypeIndex]);

        spawnedEnemy.transform.position = new Vector3(14, 0, 0);
        
        enemyTypeIndex = 0;
        spawnedEnemy = Instantiate(enemyTypeReference[enemyTypeIndex]);

        spawnedEnemy.transform.position = new Vector3(4, 0, 0);

    }
}
