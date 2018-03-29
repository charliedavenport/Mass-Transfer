using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.Networking;

public class VRPlayer : NetworkBehaviour {

    
    [SerializeField]
    private Transform Head;
    [SerializeField]
    private HandController LeftHand;
    [SerializeField]
    private HandController RightHand;

    // from steamvr camera-rig
    [SerializeField]
    private Transform SteamVR_Rig;
    [SerializeField]
    private SteamVR_TrackedObject hmd;
    [SerializeField]
    private SteamVR_TrackedObject LeftController; 
    [SerializeField]
    private SteamVR_TrackedObject RightController;

    [SyncVar]
    Vector3 headPos;
    [SyncVar]
    Quaternion headRot;

    /**
     * Utility Function 
     * copys transform.position and transform.rotation from a to b
     */
    private static void copyTransform(Transform a, Transform b) {
        b.position = a.position;
        b.rotation = a.rotation;
    }

    private void Start() {
        
    }

    private void FixedUpdate() {

        if (isLocalPlayer) {
            if (UnityEngine.XR.XRSettings.enabled) { // was VRSettings.enabled
                if (SteamVR_Rig == null) { //only want this to run first time
                    GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
                    hmd = gm.hmd;
                    LeftController = gm.leftController;
                    RightController = gm.rightController;

                    copyTransform(LeftController.transform, LeftHand.transform);
                    copyTransform(RightController.transform, RightHand.transform);
                    copyTransform(hmd.transform, Head);

                    //TODO: add Transform feet and find feet position here

                    //TODO: handle controller inputs
                }


            }//VRSettings.enabled
            CmdSyncPlayer(Head.transform.position, Head.transform.rotation);
        } //isLocalPlayer
        else {
            // runs on all other clients and server
            // move to syncvars
            Head.position = headPos;
            Head.rotation = headRot;
        }
    }// FixedUpdate

    [Command]
    void CmdSyncPlayer(Vector3 pos, Quaternion rot) {
        Head.transform.position = pos;
        Head.transform.rotation = rot;
        //set syncvars
        headPos = pos;
        headRot = rot;
    }

    private void Update()
    {
        LeftHand.gameObject.transform.position = LeftController.transform.position;
        LeftHand.gameObject.transform.rotation = LeftController.transform.rotation;

        RightHand.gameObject.transform.position = RightController.transform.position;
        RightHand.gameObject.transform.rotation = RightController.transform.rotation;
    }

}
