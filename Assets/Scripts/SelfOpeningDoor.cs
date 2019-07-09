using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfOpeningDoor : MonoBehaviour
{
    public bool openDoor;
    public Animator doorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        doorAnimator.SetBool("Open", openDoor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rig"))
        {
            Debug.Log("Collided with Player");
            openDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rig"))
        {
            Debug.Log("Player left");
            openDoor = false;
        }
    }
}
