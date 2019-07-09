using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light light;
    public GameObject rig;
    public float maxDistance;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        maxDistance = 10;
        rig = GameObject.FindGameObjectWithTag("Rig");
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(rig.transform.position, this.gameObject.transform.position);

        if (distance > maxDistance)
        {
            light.enabled = false;
        }
        else
        {
            light.enabled = true;
        }
    }
}
