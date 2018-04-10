using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BathTubController : NetworkBehaviour {


    [SerializeField]
    ValveController valeController;
    [SerializeField]
    private ValveGUIController valveGUI;
    [SerializeField]
    private float flowRate;
    [SerializeField]
    private float valveAngle;

    [SyncVar]
    float flowRate_sync;

    private GameManager gm;

    private void Awake() {
        flowRate = 0f;
        valveAngle = 0f;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void FixedUpdate() {
        if (isLocalPlayer) {
            valveGUI.setFlowRate(flowRate, 0f);
            CmdSyncBathTub(flowRate);
        }
        else { // not local player
            flowRate = flowRate_sync;
            // not sure about this line... need to check if this is necessary
            valveGUI.setFlowRate(flowRate, 0f);
        }
    }

    [Command]
    void CmdSyncBathTub(float fRate) {
        flowRate = fRate;
        //set syncvars
        flowRate_sync = fRate;
    }

}
