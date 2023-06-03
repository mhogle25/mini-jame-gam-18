using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Splatterer : MonoBehaviour
{
    [SerializeField] private Splatter splatterPrefab = null;
    [SerializeField] private int splatCount = 5;
    [SerializeField] private float radius = 0.5f;

    private readonly Dictionary<PaintColor, List<Splatter>> splatters = new();

    /// <summary>
    /// ooga booga
    /// </summary>
    /// <param name="paintColor">color</param>
    /// <param name="relativePos">position</param>
    public void CreateSplatter(PaintColor paintColor, Vector2 relativePos)
    {
        for (int i = 0; i < this.splatCount; i++)
        {
            Vector2 randPos = Random.insideUnitCircle * this.radius;
            Splatter splatter = Instantiate(this.splatterPrefab);
            splatter.SetColor(GameManager.Instance.GetColorInfo(paintColor).Hue);
            splatter.transform.position = relativePos + randPos;

            this.splatters[paintColor].Add(splatter);
        }
    }


}
