using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public bool isEnable = true;
    public ObjectSpawner objectSpawner;

    public float spawnIntervalMin = 4;
    public float spawnIntervalMax = 10;

    public float spawnInterval = 0;
    public float spawnTimer = 0;

    private void Update()
    {
        if (!isEnable) {
            return;
        }

        spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        } else
        {
            spawnTimer = spawnInterval;
            if (objectSpawner)
            {
                objectSpawner.Spawn();
            }
        }
    }
}
