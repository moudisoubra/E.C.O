using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnObject : MonoBehaviour
{
    public float waitTime = 2.0f;

    public InteractableSoubra interactableScript = null;
    public HandSoubra lastInteraction = null;
    public Coroutine countDown = null;

    public Vector3 originalPosition = Vector3.zero;
    public Quaternion originalRotation = Quaternion.identity;

    void Awake()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        interactableScript = GetComponent<InteractableSoubra>();
    }

    private void Update()
    {
        lastInteraction = interactableScript.activeHand;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!lastInteraction)
            return;

        if (countDown != null)
            StopCoroutine(countDown);

        countDown = StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(waitTime);

        GameObject currentObject = null;

        if (interactableScript.activeHand)
        {
            currentObject = lastInteraction.currentInteractable.gameObject;
        }

        if (currentObject)
        {
            if (gameObject.GetInstanceID() == currentObject.GetInstanceID())
                yield break;
        }

        transform.position = originalPosition;
        transform.rotation = originalRotation;
        Debug.Log("Returned");
    }
}
