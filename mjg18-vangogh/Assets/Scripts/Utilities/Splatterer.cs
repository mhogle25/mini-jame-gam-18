using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Splatterer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer splatterPrefab = null;
    [SerializeField] private int splatCount = 5;
    [SerializeField] private float radius = 0.5f;

    public void CreateSplatter(Vector2 relativePos)
    {
        for (int i = 0; i < this.splatCount; i++)
        {
            Vector2 randPos = Random.insideUnitCircle * this.radius;
            SpriteRenderer splatter = Instantiate(this.splatterPrefab);
            splatter.transform.position = relativePos + randPos;
            Debug.Log($"splatter pos: {splatter.transform.position}");
        }
    }
}
