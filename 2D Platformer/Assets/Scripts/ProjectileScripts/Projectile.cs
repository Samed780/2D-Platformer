using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed = new Vector2(3f, 0);
    [SerializeField] float damage = 5f;
    [SerializeField] Vector2 knockBack = new Vector2(0, 0);

    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 deliveredKnockBack = transform.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);

        Health health = collision.GetComponent<Health>();

        if (health != null)
            health.TakeDamage(damage, deliveredKnockBack);

        Destroy(gameObject);
    }
}
