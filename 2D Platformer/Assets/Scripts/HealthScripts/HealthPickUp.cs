using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] float healthRestore = 20f;

    [SerializeField] Vector3 spinRotationSpeed = new Vector3 (0, 180,0);

    AudioSource healSound;

    private void Awake()
    {
        healSound = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();

        if (health && health.CurrentHealth < health.MaxHealth)
        {
            bool wasHealed = health.Heal(healthRestore);

            if (wasHealed)
            {
               AudioSource.PlayClipAtPoint(healSound.clip, transform.position, healSound.volume);
               Destroy(gameObject);
            }

        }
    }
}
