using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketSpawner : MonoBehaviour
{
    [SerializeField] private Bucket[] bucketPrefabs;
    [SerializeField] private int chanceOneOutOf = 20;
    [SerializeField] private float spawnRadius = 1f;

    private Bucket bucket = null;

    private float timer = 0f;

    private void Update()
    {
        if (!GameManager.Instance.InCombat)
            return;

        if (this.bucket != null)
            return;

        this.timer += Time.deltaTime;
        if (this.timer % 60 > 1)
        {
            if (Random.Range(0, this.chanceOneOutOf) == 0)
            {
                int bucketIndex = Random.Range(0, this.bucketPrefabs.Length);
                this.bucket = Instantiate(this.bucketPrefabs[bucketIndex]);
                Vector3 randPos = Random.insideUnitCircle * this.spawnRadius;
                randPos.z = 0;
                this.bucket.transform.position = this.transform.position + randPos;
                this.bucket.Setup(this);
            }
            this.timer = 0;
        }
    }

    public void Reset()
    {
        this.bucket = null;
    }
}
