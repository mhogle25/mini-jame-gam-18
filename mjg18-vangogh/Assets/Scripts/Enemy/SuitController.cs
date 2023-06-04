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

        SetupDestination(GameManager.Instance.Player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupDestination(PlayerController player)
    {
        this.aiDestinationSetter.target = player.transform;
    }

}
