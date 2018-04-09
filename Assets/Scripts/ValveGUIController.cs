using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValveGUIController : MonoBehaviour {

    [SerializeField]
    private Text flowRateText;

    private float flowRate;

    public void setFlowRate(float f) {
        flowRate = f;
    }

    private void Awake() {
        flowRate = 0f;
    }

    private void Update() {
        flowRateText.text = "Flow Rate: " + flowRate.ToString("F");
    }


}
