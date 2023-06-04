using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScraperController : EntityController
{
    [Header("Scraper")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private SpriteRenderer deathSpritePrefab;

    private LeftRight direction = LeftRight.Left;
    private Vector2 movement;

    private bool dead = false;

    public void SetupDirection(LeftRight direction)
    {
        this.direction = direction;
    }

    public override void Damage(int damage)
    {
        this.health -= damage;
        if (this.health < 1)
        {
            SpriteRenderer deathSprite = Instantiate(this.deathSpritePrefab);
            deathSprite.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (!this.dead)
            this.movement = DirectionValue();
    }

    private void FixedUpdate()
    {
        if (!this.dead)
            this.rb.MovePosition(this.rb.position + moveSpeed * Time.fixedDeltaTime * movement);
    }

    private Vector2 DirectionValue()
    {
        return this.direction switch
        {
            LeftRight.Left => new Vector2(-1, 0),
            LeftRight.Right => new Vector2(1, 0),
            _ => throw new System.Exception("Fr*ck")
        };
    }
}
