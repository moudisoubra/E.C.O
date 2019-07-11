using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    Rigidbody rb = null;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public GameObject GetCurrentObject()
    {
        if (!rb)
            return null;
        else
            return rb.gameObject;
    }
}
