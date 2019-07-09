using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Teleporter : MonoBehaviour
{

    public GameObject pointer;
    public SteamVR_Action_Boolean teleportAction;
    public SteamVR_Behaviour_Pose pose = null;
    public bool hasPosition = false;
    public bool isTeleporting = false;
    public float fadeTime = 0.5f;
    public LayerMask layerMask;
    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    void Update()
    {
        hasPosition = UpdatePointer();
        pointer.SetActive(hasPosition);

        if (teleportAction.GetStateUp(pose.inputSource))
        {
            TryTeleport();
        }
    }

    public void TryTeleport()
    {
        if (!hasPosition || isTeleporting)
            return;

        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        Vector3 translateVector = pointer.transform.position - groundPosition;

        StartCoroutine(MoveRig(cameraRig, translateVector));
    }

    IEnumerator MoveRig(Transform cameraRig, Vector3 transformPosition)
    {
        isTeleporting = true;

        SteamVR_Fade.Start(Color.black, fadeTime, true);

        yield return new WaitForSeconds(fadeTime);

        cameraRig.position += transformPosition;

        SteamVR_Fade.Start(Color.clear, fadeTime, true);

        isTeleporting = false;
    }

    public bool UpdatePointer()
    {
        //Ray from controller;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, layerMask) && hit.transform.gameObject.CompareTag("Floor"))
        {
            pointer.transform.position = hit.point;
            return true;
        }

        return false;
    }
}
