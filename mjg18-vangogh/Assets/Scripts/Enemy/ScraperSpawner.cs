using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScraperSpawner : EnemySpawner
{
    [Header("Scraper")]
    [SerializeField] private ScraperController enemyPrefab = null;
    [SerializeField] private LeftRight movementDirection = LeftRight.Left;

    protected override void InstantiateEnemyPrefab()
    {
        ScraperController enemy = Instantiate(this.enemyPrefab);
        Vector3 randPos = Random.insideUnitCircle * this.spawnRadius;
        randPos.z = 0;
        enemy.transform.position = this.transform.position + randPos;
        enemy.SetupDirection(this.movementDirection);
    }
}
