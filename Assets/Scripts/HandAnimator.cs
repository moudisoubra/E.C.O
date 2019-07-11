using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandAnimator : MonoBehaviour
{
    public SteamVR_Action_Single grab = null;
    public SteamVR_Behaviour_Pose pose = null;

    public Animator animator = null;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        pose = GetComponentInParent<SteamVR_Behaviour_Pose>();

        grab[pose.inputSource].onChange += Grab;
    }

    private void OnDestroy()
    {
        grab[pose.inputSource].onChange -= Grab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grab(SteamVR_Action_Single action, SteamVR_Input_Sources inputSource, float axis, float delta)
    {
        animator.SetFloat("PickUp", axis);
    }
}
