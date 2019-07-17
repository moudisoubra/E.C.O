using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandSoubra : MonoBehaviour
{
    public SteamVR_Action_Boolean grab = null;
    public SteamVR_Behaviour_Pose pose = null;
    public FixedJoint joint = null;

    public InteractableSoubra currentInteractable = null;
    public List<InteractableSoubra> contactInteractable = new List<InteractableSoubra>();
    // Start is called before the first frame update
    void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grab.GetLastStateDown(pose.inputSource))
        {
            Debug.Log(pose.inputSource + " Trigger Down");
            PickUp();
        }

        if (grab.GetLastStateUp(pose.inputSource))
        {
            Debug.Log(pose.inputSource + " Trigger Up");
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Interactable"))
            return;

        contactInteractable.Add(other.gameObject.GetComponent<InteractableSoubra>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Interactable"))
            return;

        contactInteractable.Remove(other.gameObject.GetComponent<InteractableSoubra>());
    }

    public void PickUp()
    {
        currentInteractable = GetNearestIS();

        if (!currentInteractable)
            return;

        if (currentInteractable.activeHand)
        {
            currentInteractable.activeHand.Drop();
        }

        currentInteractable.transform.position = transform.position;
        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        joint.connectedBody = targetBody;

        currentInteractable.activeHand = this;
    }

    public void Drop()
    {
        if (!currentInteractable)
            return;

        joint.connectedBody = null;

        Throw();


        currentInteractable.activeHand = null;
        currentInteractable = null;
    }

    public void Throw()
    {
        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        Debug.Log("Velocity: " + pose.GetVelocity());
        Debug.Log("AngularVelocity: " + pose.GetAngularVelocity());
    }

    private InteractableSoubra GetNearestIS()
    {
        InteractableSoubra nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (InteractableSoubra interactable in contactInteractable)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }

        return nearest;
    }
}
