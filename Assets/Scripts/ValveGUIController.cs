using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValveGUIController : MonoBehaviour {

    [SerializeField]
    private Text flowInText;
    [SerializeField]
    private Text flowOutText;

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
        flowInText.text = "Flow In Rate: " + flowInRate.ToString("F");
        flowOutText.text = "Flow Out Rate: " + flowOutRate.ToString("F");

    }


}
