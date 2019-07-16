using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform orbitAround;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        orbitAround = GameObject.FindGameObjectWithTag("OrbitPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(orbitAround.position, orbitAround.up, speed * Time.deltaTime);
    }
}
