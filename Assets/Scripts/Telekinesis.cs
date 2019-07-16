using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    public GameObject teleSpot;
    public GameObject currentObject;
    public bool pickItUp;
    public bool launchIt;
    public float time;

    void Start()
    {
        
    }


    void Update()
    {
        if (pickItUp)
        {
            currentObject.GetComponent<Rigidbody>().useGravity = false;
            currentObject.GetComponent<Rigidbody>().isKinematic = true;
            currentObject.transform.position = Vector3.Lerp(currentObject.transform.position, teleSpot.transform.position, Time.deltaTime * 0.8f);
        }

        if (launchIt)
        {
            currentObject.GetComponent<Rigidbody>().AddForce(teleSpot.transform.right, ForceMode.Impulse);
            currentObject.GetComponent<Rigidbody>().useGravity = true;
            currentObject.GetComponent<Rigidbody>().isKinematic = false;
            pickItUp = false;
            time += Time.deltaTime;

            if (time > 2)
            {
                time = 0;
                currentObject = null;
                launchIt = false;
            }

        }
    }
}
