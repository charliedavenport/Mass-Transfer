using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValveGUIController : MonoBehaviour {

    [SerializeField]
    private Text flowInText;
    [SerializeField]
    private Text flowOutText;
    [SerializeField]
    private ValveController valve_in;
    [SerializeField]
    private ValveController valve_out;

    private float flowInRate;
    private float flowOutRate;

    public void setFlowRate(float inRate, float outRate) {
        flowInRate = inRate;
        flowOutRate = outRate;
    }

    private void Awake() {
        flowInRate = 0f;
        flowOutRate = 0f;
    }

    private void Update() {
        flowInText.text = "Flow In Rate: " + valve_in.getFlowRate().ToString("F");
        flowOutText.text = "Flow Out Rate: " + valve_out.getFlowRate().ToString("F");

    }


}
