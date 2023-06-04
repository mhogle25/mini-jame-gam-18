using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [Header("Entity")]
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    protected virtual void Update()
    {
        this.spriteRenderer.sortingOrder = Mathf.RoundToInt(this.transform.position.y * 100f) * -1;
    }
}
