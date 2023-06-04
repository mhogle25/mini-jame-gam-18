using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    [Header("Entity")]
    [SerializeField] protected SpriteRenderer spriteRenderer = null;

    protected int health = 3;

    protected virtual void Update()
    {
        this.spriteRenderer.sortingOrder = Mathf.RoundToInt(this.transform.position.y * 100f) * -1;
    }

    public abstract void Damage(int damage);
}
