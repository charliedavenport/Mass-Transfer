using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.Networking;

public class VRPlayer : NetworkBehaviour {

    [SerializeField]
    private Transform SteamVR_Rig;
    [SerializeField]
    private Transform Head;
    [SerializeField]
    private HandController LeftHand;
    [SerializeField]
    private HandController RightHand;

    // from steamvr camera-rig
    [SerializeField]
    private SteamVR_TrackedObject hmd;
    [SerializeField]
    private SteamVR_TrackedObject LeftController; 
    [SerializeField]
    private SteamVR_TrackedObject RightController;


    private void Start() {
        
    }

    private void FixedUpdate() {

        if (UnityEngine.XR.XRSettings.enabled) { // was VRSettings.enabled
            if (SteamVR_Rig == null) { //only want this to run first time
                GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
                hmd = gm.hmd;
                LeftController = gm.leftController;
                RightController = gm.rightController;
            }

            
        }
    }

    private void Update()
    {
        LeftHand.gameObject.transform.position = LeftController.transform.position;
        LeftHand.gameObject.transform.rotation = LeftController.transform.rotation;

        RightHand.gameObject.transform.position = RightController.transform.position;
        RightHand.gameObject.transform.rotation = RightController.transform.rotation;
    }

}
