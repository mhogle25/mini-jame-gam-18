using System.Collections.Generic;
using UnityEngine;

public class PaintSplatterer : MonoBehaviour
{
    [SerializeField] private Splatter[] splatterPrefabs = { };
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
            Splatter splatter = InstantiateRandomSplatter();
            splatter.SetColor(GameManager.Instance.GetColorInfo(paintColor).Hue);
            splatter.transform.position = relativePos + randPos;

            if (!this.splatters.ContainsKey(paintColor))
                this.splatters[paintColor] = new();

            this.splatters[paintColor].Add(splatter);
        }
    }

    private Splatter InstantiateRandomSplatter()
    {
        int randomIndex = Random.Range(0, this.splatterPrefabs.Length);
        return Instantiate(this.splatterPrefabs[randomIndex]);
    }
}