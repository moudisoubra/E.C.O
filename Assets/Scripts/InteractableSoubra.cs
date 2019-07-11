using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InteractableSoubra : MonoBehaviour
{
    public GameObject returnTo = null;
    public HandSoubra activeHand = null;
    public Rigidbody rb = null;
    public bool returnable;
    public bool returning = false;
    public float waitTime = 0;

    public Coroutine countDown = null;

    public Vector3 originalPosition = Vector3.zero;
    public Quaternion originalRotation = Quaternion.identity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        if (returning)
        {
            ReturnObjectToPlace();
        }
    }

    public void ReturnObjectToPlace()
    {
        if (activeHand)
        {
            if (activeHand.GetComponent<HandSoubra>().currentInteractable == this.gameObject)
            {
                activeHand.GetComponent<HandSoubra>().currentInteractable = null;
            }
            activeHand = null;
        }
        returnable = false;
        StopCoroutine(countDown);
        returnTo.GetComponent<Collider>().enabled = false;
        rb.useGravity = false;
        rb.isKinematic = true;
        transform.position = Vector3.Lerp(transform.position, returnTo.transform.position, Time.deltaTime * 0.8f);
        transform.rotation = Quaternion.Lerp(transform.rotation, returnTo.transform.rotation, Time.deltaTime * 0.8f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (returnTo && collision.gameObject == returnTo)
        {
            returning = true;
        }
        if (activeHand || !returnable)
            return;

        if (countDown != null)
            StopCoroutine(countDown);

        countDown = StartCoroutine(CountDown());
    }

    private void OnTriggerEnter(Collider other)
    {

    }
    private IEnumerator CountDown()
    {
        yield return new WaitForSecondsRealtime(waitTime);

        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
