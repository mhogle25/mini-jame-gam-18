using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PaintSplatterer : MonoBehaviour
{
    [SerializeField] private Splatter splatterPrefab = null;
    [SerializeField] private int splatCount = 5;
    [SerializeField] private float radius = 0.5f;

    private readonly Dictionary<PaintColor, List<Splatter>> splatters = new();

    public void CreateSplatter(PaintColor paintColor, Vector2 relativePos)
    {
        CreateSplatter(paintColor, relativePos, this.splatCount, this.radius);
    }

    public void CreateSplatter(PaintColor paintColor, Vector2 relativePos, int splatCount, float radius)
    {
        for (int i = 0; i < splatCount; i++)
        {
            Vector2 randPos = Random.insideUnitCircle * radius;
            Splatter splatter = Instantiate(this.splatterPrefab);
            splatter.SetColor(GameManager.Instance.GetColorInfo(paintColor).Hue);
            splatter.transform.position = relativePos + randPos;

            this.splatters[paintColor].Add(splatter);
        }
    }
}
