using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0.1f, 10.0f)] float spawnWaitTime = 1f;
    [SerializeField] [Range(0, 50)] int poolSize = 5;

    private GameObject[] pool;

    private void Awake()
    {
        CreatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            ActivateEnemyFromPool();
            yield return new WaitForSeconds(spawnWaitTime);
        }

    }

    void ActivateEnemyFromPool()
    {
        foreach (GameObject enemy in pool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return;
            }
        }
    }

    void CreatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            // instantiate a new enemy instance and put it under a single parent
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

}
