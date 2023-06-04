using System.Collections.Generic;
using UnityEngine;

public class PaintSplatterer : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Splatter[] splatterPrefabs = { };
    [SerializeField] private Splatter strokeUpPrefab = null;
    [SerializeField] private Splatter strokeDownPrefab = null;
    [SerializeField] private Splatter strokeLeftPrefab = null;
    [SerializeField] private Splatter strokeRightPrefab = null;

    [Header("Splat Properties")]
    [SerializeField] private int splatCount = 5;
    [SerializeField] private float radius = 0.5f;

    private readonly Dictionary<PaintColor, List<Splatter>> splatters = new();

    public void CreateStroke(PaintColor paintColor, Vector2 relativePos, Direction direction)
    {
        Splatter stroke = InstantiateStroke(direction);
        stroke.SetColor(paintColor);
        stroke.transform.position = relativePos;

        if (!this.splatters.ContainsKey(paintColor))
            this.splatters[paintColor] = new();

        this.splatters[paintColor].Add(stroke);
    }

    public void CreateSplatter(PaintColor paintColor, Vector2 relativePos)
    {
        CreateSplatter(paintColor, relativePos, this.splatCount, this.radius);
    }

    public void RemoveSplatter(PaintColor color, Splatter reference)
    {
        if (this.splatters.ContainsKey(color))
            this.splatters[color].Remove(reference);
    }

    public void CreateSplatter(PaintColor paintColor, Vector2 relativePos, int splatCount, float radius)
    {
        for (int i = 0; i < splatCount; i++)
        {
            Vector2 randPos = Random.insideUnitCircle * radius;
            Splatter splatter = InstantiateRandomSplatter();
            splatter.SetColor(paintColor);
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

    public Splatter InstantiateStroke(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Instantiate(this.strokeUpPrefab),
            Direction.Down => Instantiate(this.strokeDownPrefab),
            Direction.Left => Instantiate(this.strokeLeftPrefab),
            Direction.Right => Instantiate(this.strokeRightPrefab),
            _ => throw new System.Exception("What the frick")
        };
    }
}