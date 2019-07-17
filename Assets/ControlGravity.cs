using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGravity : MonoBehaviour
{
    public Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < originalPosition.y)
        {
            Debug.Log("FIXING GRAVITY");
            transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);
        }
    }
}
