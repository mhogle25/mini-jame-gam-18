using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 100f;
    public Rigidbody2D rb;

    public Animator animator;

    public UnityEvent<Vector2> onAttack = null;
    
//[SerializeField] is automatic when public, can add when private to make something serialize in the Unity inspector
    private Vector2 movement;
//private is implied

    void Update()
    {

       movement.x = Input.GetAxisRaw("Horizontal");
       movement.y = Input.GetAxisRaw("Vertical");

       animator.SetFloat("Horizontal", movement.x);
       animator.SetFloat("Vertical", movement.y);
       animator.SetFloat("Speed", movement.sqrMagnitude);

       //attacking, runs animations and shit
       if (Input.GetKeyDown("j"))
        {
            Attack();
        }


    }

    void Attack()
    {
        //Play an attack animation
        animator.SetTrigger("Attack");
        //Detect enemies in range of attack
        //Apply damage
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

}
