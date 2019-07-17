using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{
    public float sensitivity = 0.1f;
    public float maxSpeed = 1.0f;

    public SteamVR_Action_Boolean movePress = null;
    public SteamVR_Action_Vector2 moveValue = null;

    public float speed = 0.0f;
    public CharacterController cController = null;
    public Transform cameraRig = null;
    public Transform head = null;


    public void Awake()
    {
        cController = GetComponent<CharacterController>();
    }

    void Start()
    {
        cameraRig = SteamVR_Render.Top().origin;
        head = SteamVR_Render.Top().head;
    }


    void Update()
    {
        HandleHead();
        HandleHeight();
        CalculateMovement();
    }

    public void HandleHead()
    {
        Vector3 oldPosition = cameraRig.position;
        Quaternion oldRotation = cameraRig.rotation;

        transform.eulerAngles = new Vector3(0.0f, head.rotation.eulerAngles.y, 0.0f);
        cameraRig.position = oldPosition;
        cameraRig.rotation = oldRotation;
    }

    public void CalculateMovement()
    {
        Vector3 orientationEuler = new Vector3(0, transform.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        if (movePress.GetLastStateUp(SteamVR_Input_Sources.Any))
            speed = 0;

        if (movePress.state)
        {
            speed += moveValue.axis.y * sensitivity;
            speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

            movement += orientation * (speed * Vector3.forward) * Time.deltaTime;
        }

        movement.y += Physics.gravity.y * Time.deltaTime; // Did this to apply gravity to a character controller.

        cController.Move(movement);
    }

    public void HandleHeight()
    {
        float headHeight = Mathf.Clamp(head.localPosition.y, 1, 2);
        cController.height = headHeight;

        Vector3 newCenter = Vector3.zero;
        newCenter.y = cController.height / 2;
        newCenter.y += cController.skinWidth;

        newCenter.x = head.localPosition.x;//MAKE SURE THE CAPSULE FOLLOWS CAMERA WITHING PLAYING SPACE
        newCenter.z = head.localPosition.z;

        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

        cController.center = newCenter;
    }
}
