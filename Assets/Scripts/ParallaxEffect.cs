using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    Vector2 startingPos;
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPos;

    float startingZ;
    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) - clippingPlane;

    void Start()
    {
        startingPos = transform.position;
        startingZ = transform.position.z;
    }
    void Update()
    {
        Vector2 newPosition = startingPos - camMoveSinceStart / parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ); 
    }
}
