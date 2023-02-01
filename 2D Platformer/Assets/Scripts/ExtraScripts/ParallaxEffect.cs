using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform followTarget;
    //starting position of the parallax gameobject
    Vector2 startingPos;
    //start Z value of the parallax gameobject
    float startingZ;

    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPos;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;


    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = startingPos + camMoveSinceStart * parallaxFactor;
        
        transform.position = new Vector3(newPos.x, newPos.y, startingZ);
    }
}
