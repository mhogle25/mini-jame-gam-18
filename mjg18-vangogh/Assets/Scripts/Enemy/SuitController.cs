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

    private void Awake() 
    {
        this.aiDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public override void SetupDestination(PlayerController player)
    {
        this.aiDestinationSetter.target = player.transform;
    }
}
