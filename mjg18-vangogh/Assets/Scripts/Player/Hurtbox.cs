using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    private PaintColor color = PaintColor.Magenta;

    public void Setup(PaintColor color)
    {
        this.color = color;
    }
}
