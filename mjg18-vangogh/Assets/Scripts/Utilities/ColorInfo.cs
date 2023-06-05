using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorInfo
{
    public Color32 Hue { get; set; } = Color.white;
    public int Damage { get; set; } = 3;
    public float SplatRadius { get; set; } = 0.5f;
    public int SplatCount { get; set; } = 5;
    public float PlayerSpeed { get; set; } = 0f;
}