using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorInfo
{
    public Color32 Hue { get; set; } = Color.white;
    public int Damage { get; set; } = 3;
    public float HurtRadius { get; set; } = 1f;
    public bool Stuns { get; set; } = true;
}