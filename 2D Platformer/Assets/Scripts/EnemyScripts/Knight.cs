using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Health))]
public class Knight : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    TouchingDirections touchingDirections;
    Health health;

    [SerializeReference] DetectionZone attackZone;
    [SerializeField] DetectionZone cliffDetectionZone;


    [SerializeField] float walkAcceleration = 3f;
    [SerializeField] float maxSpeed = 3f;
    [SerializeField] float walkStopRate = 0.6f;

    public enum WalkableDirection { right, left}

    WalkableDirection _walkDirection;
    Vector2 WalkDirectionVector = Vector2.right;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        private set 
        {
            if(_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if(value == WalkableDirection.right)
                {
                    WalkDirectionVector = Vector2.right;
                }
                else if(value == WalkableDirection.left)
                {
                    WalkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    bool _hasTarget = false;
    public bool HasTarget 
    { 
        get { return _hasTarget; }
        private set 
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        } 
    }

    public bool CanMove { get { return animator.GetBool(AnimationStrings.canMove); } }

    public float AttackCooldown { 
        get { return animator.GetFloat(AnimationStrings.attackCooldown); }
        private set { animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value,0)); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;

        if(AttackCooldown > 0)
            AttackCooldown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
            FlipDirection();

        if (!health.LockVelocity)
        {
            if (CanMove && touchingDirections.IsGrounded)
            {
                float xVelocity = Mathf.Clamp(rb.velocity.x + (walkAcceleration * WalkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed);
                rb.velocity = new Vector2(xVelocity, rb.velocity.y);
            }
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
    }

    void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.right)
            WalkDirection = WalkableDirection.left;
        else if (WalkDirection == WalkableDirection.left)
            WalkDirection = WalkableDirection.right;
        else
            Debug.LogError("Current walkable direction is not set to either right or left");
    }

    public void OnHit(float damage, Vector2 knockBack)
    {
        rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
    }

    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
            FlipDirection();
    }
}
