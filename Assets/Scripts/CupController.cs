using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CupController : NetworkBehaviour
{

    [SerializeField]
    SliderController tempSlider;//cube
    [SerializeField]
    SliderController humiditySlider;//cube
    [SerializeField]
    SliderController pressureSlider;//new
    //[SerializeField]
    //WaterController water;
    [SerializeField]
    private CupGUIController cupGUI; //valveGUI
    [SerializeField]
    private float tempValue;
    [SerializeField]
    private float humidityValue;
    [SerializeField]
    private float pressureValue;
    [SerializeField]
    private Slider tempUISlider;
    [SerializeField]
    private Slider humidityUISlider;
    [SerializeField]
    private float valveAngle;

	GameObject tea;
	double teaPos;

	private const float width = 3f;
    private const float length = 4f;

	/*CONSTANTS*/
	float CA_liq = 55f; // mol / L
	float R = 62.3637f; // L mmHg / mol K
	float P = 760f; // mmHg
	float D_AB = 0.0016f; // cm2 / day
	float L0 = 1f; // m

	/*NONFIXED VARS*/
	float temp; // T (in Kelvins) //
	float time; // t where 1 minute = 1 day
	float L1; // L1
	float Y_A0;
	float Y_AL;
	float P_star;
	float r_h;

	float tempUIValue;
    float humidityUIValue;

    private GameManager gm;

    private void Awake()
    {
        tempValue = 0f;
        humidityValue = 0f;
        pressureValue = 0f;
        valveAngle = 0f;
        tempUIValue = tempUISlider.value;
        humidityUIValue = humidityUISlider.value;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		temp = tempUIValue; //unit: Kelvins 
		r_h = humidityUIValue;
		tea = GameObject.Find("tea");
		//teaPos = tea.transform.position.y;

	}

	private void FixedUpdate()
    {

        tempValue = tempSlider.getSliderOutput();
        humidityValue = humiditySlider.getSliderOutput();
        pressureValue = pressureSlider.getSliderOutput();
		L1 = computeL1();
		//Debug.Log("L1 L1 L1 L1 L1 L1!!! " + L1);

		/*float dV = flowInRate - flowOutRate; // difference in volume
        float dy = dV / (width * length); // difference in height;
        Debug.Log(dy);
        water.incrementWaterLevel(dy);*/

		if (isLocalPlayer)
        {
            /*
            flowInRate = valveIn.getFlowRate();
            flowOutRate = valveOut.getFlowRate();

            float dV = flowInRate - flowOutRate; // difference in volume
            float dy = dV / (width * length); // difference in height;
            Debug.Log(dy);
            water.incrementWaterLevel(dy);*/

            //valveGUI.setFlowRate(flowInRate, flowOutRate);
            //(flowInRate);
        }
        else
        { // not local player
            //flowInRate = flowRate_sync;
            // not sure about this line... need to check if this is necessary
            //valveGUI.setFlowRate(flowInRate, flowOutRate);
        }
    }

    public float computeL1()
    {
		temp = tempValue;

		P_star = Mathf.Pow(10, 29.8605f - (3152.2f / temp) - 7.3037f * Mathf.Log10(temp) + (2.4247f * Mathf.Pow(10, -9) * temp) + (1.809f * Mathf.Pow(10, -6) * temp * temp));
		Y_A0 = P_star / P;
		Y_AL = r_h * Y_A0;

		time = Time.time * 24 * 60; // 1 min is 1 day

		L1 = (2 / CA_liq) * ((-P * D_AB * Mathf.Log((1 - Y_A0) / (1 - Y_AL)) * time) / (R * time)) + (L0 * L0);
		L1 = Mathf.Sqrt(L1);

		return L1;
	}
}