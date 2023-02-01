using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    [SerializeField] DetectionZone detectionZone;
    Health health;
    [SerializeField] Collider2D deathCollider;

    [SerializeField] List<Transform> wayPoints;
    [SerializeField] float flightSpeed = 3f;
    [SerializeField] float wayPointInRange = 0.1f;

    Transform nextWayPoint;
    int wayPointIndex = 0;

    public bool CanMove { get { return animator.GetBool(AnimationStrings.canMove); } }



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


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        nextWayPoint = wayPoints[wayPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = detectionZone.detectedColliders.Count > 0;   
    }

    private void FixedUpdate()
    {
        if (health.IsAlive)
        {
            if (CanMove)
            {
                Fly();
            }
            else
                rb.velocity = Vector3.zero;

        }
    }

    void Fly()
    {
        Vector2 directionToWayPoint = (nextWayPoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWayPoint.position, transform.position);

        rb.velocity = directionToWayPoint * flightSpeed;

        UpdateDirection();

        if(distance <= wayPointInRange)
        {
            wayPointIndex++;

            if (wayPointIndex >= wayPoints.Count)
                wayPointIndex = 0;

            nextWayPoint = wayPoints[wayPointIndex];
        }
    }

    void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;

        if(transform.localScale.x > 0)
        {
            if (rb.velocity.x < 0)
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
        }
        else
        {
            if(rb.velocity.x > 0)
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
        }
    }

    public void OnDeath()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.gravityScale = 1;
        deathCollider.enabled = true;
    }
}
