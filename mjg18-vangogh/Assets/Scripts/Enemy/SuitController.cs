using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIDestinationSetter))]
public class SuitController : EnemyController
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private AIDestinationSetter aiDestinationSetter = null;

    [SerializeField] private Animator animator;

    private void Awake() 
    {
        this.aiDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    //private void StateMoveFixed() => rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Vector3 direction = GameManager.Instance.Player.transform.position - this.transform.position;
        direction.z = 0;
        //Debug.Log(Vector3.Normalize(direction));
        this.animator.SetFloat("Horizontal", direction.x);
        this.animator.SetFloat("Vertical", direction.y);

    }

    public override void SetupDestination(PlayerController player)
    {
        this.aiDestinationSetter.target = player.transform;
    }
}
