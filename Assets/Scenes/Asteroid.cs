using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Transform asteroidT;
    Vector3 randomRotation;

    void awake()
    {
        asteroidT = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        //random size
        Vector3 scale = Vector3.one;
        scale.x = randomRotation.Range(.8f, 1.2f);
        asteroidT.localScale = scale;
        //random rotation
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
