using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Properties")]
    [SerializeField] protected int chanceOneOutOf = 3;
    [SerializeField] protected int numberToSpawn = 5;
    [SerializeField] protected float spawnRadius = 0.7f;

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

    protected abstract void InstantiateEnemyPrefab();
}
