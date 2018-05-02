using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BathTubController : NetworkBehaviour {


    [SerializeField]
    ValveController valveIn;
    [SerializeField]
    ValveController valveOut;
    [SerializeField]
    WaterController water;
    [SerializeField]
    private ValveGUIController valveGUI;
    [SerializeField]
    private float flowInRate;
    [SerializeField]
    private float flowOutRate;
    [SerializeField]
    private float valveAngle;

    private const float width = 3f;
    private const float length = 4f;

    [SyncVar]
    float flowRate_sync;

    private GameManager gm;

    private void Awake() {
        flowInRate = 0f;
        flowOutRate = 0f;
        valveAngle = 0f;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void FixedUpdate() {

        flowInRate = valveIn.getFlowRate();
        flowOutRate = valveOut.getFlowRate();

        float dV = flowInRate - flowOutRate; // difference in volume
        float dy = dV / (width * length); // difference in height;
        Debug.Log(dy);
        water.incrementWaterLevel(dy);

        if (isLocalPlayer) {
            /*
            flowInRate = valveIn.getFlowRate();
            flowOutRate = valveOut.getFlowRate();

            float dV = flowInRate - flowOutRate; // difference in volume
            float dy = dV / (width * length); // difference in height;
            Debug.Log(dy);
            water.incrementWaterLevel(dy);*/

            //valveGUI.setFlowRate(flowInRate, flowOutRate);
            CmdSyncBathTub(flowInRate);
        }
        else { // not local player
            flowInRate = flowRate_sync;
            // not sure about this line... need to check if this is necessary
            //valveGUI.setFlowRate(flowInRate, flowOutRate);
        }
    }

    public void setFlowInRate(float f)
    {
        flowInRate = f;
    }
    public void setFlowOutrate(float f)
    {
        flowOutRate = f;
    }

    [Command]
    void CmdSyncBathTub(float fRate) {
        flowInRate = fRate;
        //set syncvars
        flowRate_sync = fRate;
    }

}
