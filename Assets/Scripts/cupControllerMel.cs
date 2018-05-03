using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class cupControllerMel : MonoBehaviour//NetworkBehaviour
{
	
    GameObject tea;
    double teaPos;

	[SerializeField]
	SliderController tempSlider;//valveIn
	[SerializeField]
	SliderController humiditySlider;//valveOut
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
	private float valveAngle;

	private const float width = 3f;
	private const float length = 4f;

	/*CONSTANTS*/
	float CA_liq = 55f; // mol / L
	float R = 62.3637f; // L mmHg / mol K
	float P = 760f; // mmHg
	float D_AB = 0.0016f; // cm2 / day
	float L0 = 1f; // m
	float r_h = 0.25f; // %

	/*NONFIXED VARS*/
	float temp; // T (in Kelvins) //
	float time; // t where 1 minute = 1 day
	float L1; // L1
	float Y_A0; 
	float Y_AL;
	float P_star;

	//[SyncVar]
	//float flowRate_sync;

	private GameManager gm;


	private void Awake()
    {
		tea = GameObject.Find("tea");
		tempValue = 0f;
		humidityValue = 0f;
		pressureValue = 0f;
		valveAngle = 0f;
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();

	}

    // Use this for initialization
    void Start()
    {
		teaPos = tea.transform.position.y;
        temp = 273; //unit: Kelvins 


    }

	private void FixedUpdate()
	{

		tempValue = 273;//tempSlider.getSliderOutput(); //tempUIValue;
		humidityValue = 0.25f;//humiditySlider.getSliderOutput();

		/*float dV = flowInRate - flowOutRate; // difference in volume
        float dy = dV / (width * length); // difference in height;
        Debug.Log(dy);
        water.incrementWaterLevel(dy);*/

		//if (isLocalPlayer)
		//{
			/*
            flowInRate = valveIn.getFlowRate();
            flowOutRate = valveOut.getFlowRate();

            float dV = flowInRate - flowOutRate; // difference in volume
            float dy = dV / (width * length); // difference in height;
            Debug.Log(dy);
            water.incrementWaterLevel(dy);*/

			//valveGUI.setFlowRate(flowInRate, flowOutRate);
			//(flowInRate);
	//	}
		//else
		//{ // not local player
		  //flowInRate = flowRate_sync;
		  // not sure about this line... need to check if this is necessary
		  //valveGUI.setFlowRate(flowInRate, flowOutRate);
		//}
	}

	// Update is called once per frame
	void Update()
    {
		// Debug.Log(tea.transform.position.y);
		//Mathf.Clamp(temp, 273, 373); //unit: Kelvins (~32 -100 F)
		Debug.Log("Hello!" + computeL1());
	}

	public float computeL1()
	{
		temp = tempValue;

		P_star = Mathf.Pow(10, 29.8605f - (3152.2f / temp) - 7.3037f * Mathf.Log10(temp) + (2.4247f * Mathf.Pow(10, -9) * temp) + (1.809f * Mathf.Pow(10, -6) * temp * temp));
		Debug.Log("PSTAR! " + P_star);
		Y_A0 = P_star / P;
		Y_AL = r_h * Y_A0;

		time = Time.time * 24 * 60; // 1 min is 1 day
		Debug.Log("TIME TIME TIME! " + time);

		L1 = (2 / CA_liq) * ((-P * D_AB * Mathf.Log((1 - Y_A0) / (1 - Y_AL)) * time) / (R * T)) + (L0 * L0);
		Debug.Log("L1 BEFORE ! " + L1);
		L1 = Mathf.Sqrt(L1);
		Debug.Log("L1 AFTER ! " + L1);

		return L1;
	}
}
