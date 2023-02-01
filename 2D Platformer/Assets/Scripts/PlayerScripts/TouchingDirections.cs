using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;
    Animator anim;

    [SerializeField] ContactFilter2D castFilter;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    [SerializeField] float groundDistance = 0.05f;
    [SerializeField] float wallDistance = 0.2f;
    [SerializeField] float celeingDistance = 0.05f;

    bool _isGrounded = true;
    public bool IsGrounded
    {
        get { return _isGrounded; }
        private set
        {
            _isGrounded = value;
            anim.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    bool _isOnWall = true;
    Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    public bool IsOnWall
    {
        get { return _isOnWall; }
        private set
        {
            _isOnWall = value;
            anim.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    bool _isOnCeiling = true;
    public bool IsOnCeiling
    {
        get { return _isOnCeiling; }
        private set
        {
            _isOnCeiling = value;
            anim.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        IsGrounded = capsuleCollider.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = capsuleCollider.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = capsuleCollider.Cast(Vector2.up, castFilter, ceilingHits, celeingDistance) > 0;
    }
}
