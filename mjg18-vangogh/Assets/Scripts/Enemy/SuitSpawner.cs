using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitSpawner : EnemySpawner
{
    [Header("Suit")]
    [SerializeField] private SuitController enemyPrefab = null;

    protected override void InstantiateEnemyPrefab()
    {
        SuitController enemy = Instantiate(this.enemyPrefab);
        Vector3 randPos = Random.insideUnitCircle * this.spawnRadius;
        randPos.z = 0;
        enemy.transform.position = this.transform.position + randPos;
        enemy.SetupDestination(GameManager.Instance.Player);
        GameManager.Instance.UpEnemyCount(enemy);
    }
}
