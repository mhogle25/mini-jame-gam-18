using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int chanceOneOutOf = 3;
    [SerializeField] private int numberToSpawn = 5;
    [SerializeField] private float spawnRadius = 0.7f;

    private EnemyController enemyPrefab = null;
    private float timer = 0f;

    // Update is called once per frame
    private void Update()
    {
        this.timer += Time.deltaTime;
        if (this.timer % 60 > 1)
        {
            if (Random.Range(0, this.chanceOneOutOf) == 0)
            {
                for (int i = 0; i < this.numberToSpawn; i++)
                {
                    InstantiateEnemyPrefab();
                }
            } 
            this.timer = 0;
        }
    }

    private void InstantiateEnemyPrefab()
    {
        EnemyController enemy = Instantiate(this.enemyPrefab);
        Vector3 randPos = Random.insideUnitCircle * this.spawnRadius;
        randPos.z = 0;
        enemy.transform.position = this.transform.position + randPos;
    }
}
