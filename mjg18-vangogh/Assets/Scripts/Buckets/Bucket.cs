using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PaintColor color;

    private BucketSpawner spawner = null;

    public void Setup(BucketSpawner spawner)
    {
        this.spawner = spawner;
        this.spriteRenderer.sortingOrder = Mathf.RoundToInt(this.transform.position.y * 100f) * -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.SetPlayerColor(this.color);
            this.spawner.Reset();
            GameManager.Instance.Splatterer.CreateSplatter(this.color, this.transform.position, 20, 2f);
            Destroy(this.gameObject);
        }
    }
}
