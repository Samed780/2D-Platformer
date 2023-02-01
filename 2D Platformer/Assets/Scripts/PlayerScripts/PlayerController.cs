using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Health))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    TouchingDirections touchingDirections;
    Health health;

    Vector2 moveInput;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 7f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float airSpeed = 3f;

    public float CurrentMoveSpeed
    {
        get 
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                            return runSpeed;
                        else
                            return walkSpeed;
                    }
                    else
                    {
                        return airSpeed;
                    }
                }
                else
                    //idle speed is 0
                    return 0;
            }
            else
                //movement locked
                return 0;   
        }
    }

    bool _isMoving = false;
    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    bool _isRunning = false;
    public bool IsRunning
    {
        get { return _isRunning; }
        private set 
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    bool _isFacingRight = true;
    public bool IsFacingRight { 
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
                transform.localScale *= new Vector2(-1, 1);
            _isFacingRight = value;
        } 
    }

    public bool CanMove { get { return animator.GetBool(AnimationStrings.canMove); }}

    public bool IsAlive { get { return animator.GetBool(AnimationStrings.isAlive); }}

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        health = GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(!health.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
            IsMoving = false;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
            IsRunning = true;
        else if(context.canceled)
            IsRunning = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
            animator.SetTrigger(AnimationStrings.attack);
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
            animator.SetTrigger(AnimationStrings.rangedAttack);
    }

    public void OnHit(float damage, Vector2 knockBack)
    {
        rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
    }

    void SetFacingDirection(Vector2 move)
    {
        if (moveInput.x > 0 && !IsFacingRight)
            IsFacingRight = true;
        else if (moveInput.x < 0 && IsFacingRight)
            IsFacingRight = false;
    }
}
