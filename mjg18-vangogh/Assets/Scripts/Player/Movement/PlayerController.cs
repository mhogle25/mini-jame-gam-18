using UnityEngine;
using System;

internal enum Direction
{
    Up,
    Left,
    Down,
    Right,
    Neutral
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float splatDistance = 1f;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Animator animator;

    private Action state = null;
    private Action fixedState = null;

    private PaintColor selectedColor = PaintColor.Red;
    
//[SerializeField] is automatic when public, can add when private to make something serialize in the Unity inspector
    private Vector2 movement;
    //private is implied

    public PaintColor SelectedColor => this.selectedColor;

    private void Update()
    {
        this.state?.Invoke();
    }

    private void FixedUpdate()
    {
        this.fixedState?.Invoke();
    }

    public void SetStateMove()
    {
        this.state = StateMove;
        this.fixedState = StateMoveFixed;
    }

    public void AttackEvent()
    {
        GameManager.Instance.Splatterer.CreateSplatter(this.selectedColor, this.transform.position + SplatOffset());
    }

    private void StateMove()
    {
        this.movement.x = Input.GetAxisRaw("Horizontal");
        this.movement.y = Input.GetAxisRaw("Vertical");

        this.animator.SetFloat("Horizontal", movement.x);
        this.animator.SetFloat("Vertical", movement.y);
        this.animator.SetFloat("Speed", movement.sqrMagnitude);

        if (AreWeMoving())
        {
            this.animator.SetFloat("lastMoveX", movement.x);
            this.animator.SetFloat("lastMoveY", movement.y);
        }

        //attacking, runs animations and shit
        if (Input.GetKeyDown(KeyCode.J))
            this.animator.SetTrigger("Attack");
    }

    private void StateMoveFixed() => rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    private bool AreWeMoving() {
        return Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1;
    }

    private Vector3 SplatOffset()
    {
        float lastMoveX = this.animator.GetFloat("lastMoveX");
        float lastMoveY = this.animator.GetFloat("lastMoveY");

        return new Vector3(lastMoveX * this.splatDistance, lastMoveY * this.splatDistance, 0);
    }
}
