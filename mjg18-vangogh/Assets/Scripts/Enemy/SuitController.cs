using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

[RequireComponent(typeof(AIDestinationSetter))]
public class SuitController : EntityController
{
    [Header("Suit")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer deathSpritePrefab;

    private AIDestinationSetter aiDestinationSetter = null;

    private Action state = null;

    private void Awake() 
    {
        this.aiDestinationSetter = GetComponent<AIDestinationSetter>();
        this.state = StateActive;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        this.state?.Invoke();
    }

    public void SetupDestination(PlayerController player)
    {
        this.aiDestinationSetter.target = player.transform;
    }

    public override void Damage(int damage)
    {
        this.health -= damage;
        if (this.health < 1)
        {
            SpriteRenderer deathSprite = Instantiate(this.deathSpritePrefab);
            deathSprite.transform.position = this.transform.position;
            GameManager.Instance.DownEnemyCount(this);
            Destroy(this.gameObject);
        }
    }

    public void AttackEvent()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.animator.SetTrigger("Attacking");
        }
    }

    private void StateActive()
    {
        Vector3 direction = GameManager.Instance.Player.transform.position - this.transform.position;
        direction.z = 0;
        this.animator.SetFloat("Horizontal", direction.x);
        this.animator.SetFloat("Vertical", direction.y);
    }
}
