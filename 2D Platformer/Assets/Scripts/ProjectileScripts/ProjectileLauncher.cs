using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform launchPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireProjectile()
    {
        if (projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
            Vector3 original = projectile.transform.localScale;
            projectile.transform.localScale = new Vector3(original.x * transform.localScale.x > 0 ? 1 : -1, original.y, original.z);
        }
    }
}
