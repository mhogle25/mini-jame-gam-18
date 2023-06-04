using UnityEngine;
using System;

public class PlayerController : EntityController
{
    [Header("General Settings")]
    [SerializeField] private float moveSpeed = 4f;
    [Header("Splats")]
    [SerializeField] private float splatDistance = 1f;
    [Header("Hurtboxes")]
    [SerializeField] private Hurtbox hurtboxUpPrefab = null;
    [SerializeField] private Hurtbox hurtboxDownPrefab = null;
    [SerializeField] private Hurtbox hurtboxLeftPrefab = null;
    [SerializeField] private Hurtbox hurtboxRightPrefab = null;
    [Header("Obj References")]
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Animator animator;

    private Action state = null;
    private Action fixedState = null;

    private PaintColor selectedColor = PaintColor.Magenta;
    
//[SerializeField] is automatic when public, can add when private to make something serialize in the Unity inspector
    private Vector2 movement;
    //private is implied

    public PaintColor SelectedColor => this.selectedColor;

    protected override void Update()
    {
        base.Update();
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
        Vector2 positionOffset = this.transform.position + AttackOffset();

        GameManager.Instance.Splatterer.CreateSplatter(this.selectedColor, positionOffset);
        GameManager.Instance.Splatterer.CreateStroke(this.selectedColor, positionOffset, GetDirectionFacing());
        InstantiateHurtbox(GetDirectionFacing(), positionOffset);
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

    private void StateMoveFixed() => rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);

    private bool AreWeMoving() {
        return Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1;
    }

    private Vector3 AttackOffset()
    {
        float lastMoveX = this.animator.GetFloat("lastMoveX");
        float lastMoveY = this.animator.GetFloat("lastMoveY");

        return new Vector3(lastMoveX * this.splatDistance, lastMoveY * this.splatDistance, 0);
    }

    private Hurtbox InstantiateHurtbox(Direction direction, Vector2 position)
    {
        Hurtbox hurtbox = Instantiate(GetHurtbox(direction));
        hurtbox.transform.position = position;
        hurtbox.Setup(this.selectedColor);
        return hurtbox;
    }

    private Hurtbox GetHurtbox(Direction direction)
    {
        return direction switch
        {
            Direction.Up => this.hurtboxUpPrefab,
            Direction.Down => this.hurtboxDownPrefab,
            Direction.Left => this.hurtboxLeftPrefab,
            Direction.Right => this.hurtboxRightPrefab,
            _ => throw new Exception("What the fr*ck")
        };
    }

    private Direction GetDirectionFacing()
    {
        float lastMoveX = this.animator.GetFloat("lastMoveX");
        float lastMoveY = this.animator.GetFloat("lastMoveY");

        if (lastMoveX > 0.01)
            return Direction.Right;
        if (lastMoveX < -0.01)
            return Direction.Left;
        if (lastMoveY > 0.01)
            return Direction.Up;
        if (lastMoveY < -0.01)
            return Direction.Down;

        return Direction.Down;
    }
}
