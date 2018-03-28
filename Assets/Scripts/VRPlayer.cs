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
    private Transform LeftHand;
    [SerializeField]
    private Transform RightHand;

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
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            
        }
    }

    private void Update()
    {
        LeftHand.position = LeftController.transform.position;
        LeftHand.rotation = LeftController.transform.rotation;

        RightHand.position = RightController.transform.position;
        RightHand.rotation = RightController.transform.rotation;
    }

}
