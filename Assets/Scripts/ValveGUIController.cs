using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValveGUIController : MonoBehaviour {

	[SerializeField]
	private BathTubController bathTub;
    [SerializeField]
    private Text flowInText;
    [SerializeField]
    private Text flowOutText;
	[SerializeField]
	private Text dVText;
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
        flowInText.text = "Q_in: " + valve_in.getFlowRate().ToString("F");
        flowOutText.text = "Q_out: " + valve_out.getFlowRate().ToString("F");
		dVText.text = "= " + bathTub.get_dV().ToString("F");

    }


}
