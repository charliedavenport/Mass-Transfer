using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BathTubController : NetworkBehaviour {

    [SerializeField]
    private Transform valveCollider;
    [SerializeField]
    private float flowRate;
    [SerializeField]
    private Canvas valveGUI;

    [SyncVar]
    float flowRate_sync;

    private GameManager gm;

    private void Awake() {
        flowRate = 0f;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void FixedUpdate() {
        if (isLocalPlayer) {
            CmdSyncBathTub(flowRate);
        }
        else { // not local player
            flowRate = flowRate_sync;
        }
    }

    [Command]
    void CmdSyncBathTub(float fRate) {
        flowRate = fRate;
        //set syncvars
        flowRate_sync = fRate;
    }

}
