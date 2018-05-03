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
    SliderController timeSlider;//cube
    [SerializeField]
    SliderController HumiditySlider;//new
    //[SerializeField]
    //WaterController water;
    [SerializeField]
    private CupGUIController cupGUI; //valveGUI
    [SerializeField]
    private float tempValue;
    [SerializeField]
    private float timeValue;
    private float pressureValue;
    [SerializeField]
    private float humidityValue;
    [SerializeField]
    private Slider tempUISlider;
    [SerializeField]
    private Slider timeUISlider;
    [SerializeField]
    private Slider humidityUISlider;
    [SerializeField]
    private float valveAngle;

	//GameObject tea;
    GameObject cupTopGhost;
    GameObject teaL1;
    float cupTopPos;
    float teaL1Pos;

	private const float width = 3f;
    private const float length = 4f;

	/*CONSTANTS*/
	float CA_liq = 55.5f; // mol / L
	float R = 62.3637f; // L mmHg / mol K
	float P = 760f; // mmHg
	float D_AB = .0016f; // cm2 / day
	float L0 = 2f; // m

	/*NONFIXED VARS*/
	float temp; // T (in Kelvins) //
	float time; // t where 1 minute = 1 day
	float L1; // L1
	float Y_A0;
	float Y_AL;
	float P_star;
	float r_h;

	float tempUIValue;
    float timeUIValue;
    float humidityUIValue;

    private GameManager gm;

    private void Awake()
    {
        tempValue = 0f;
        timeValue = 0f;
        humidityValue = 0f;
        pressureValue = 0f;
        valveAngle = 0f;
        tempUIValue = tempUISlider.value;
        timeUIValue = timeUISlider.value;
        humidityUIValue = humidityUISlider.value;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		temp = tempUIValue; //unit: Kelvins
        time = timeUIValue;
		r_h = humidityUIValue;
		teaL1 = teaL1 = GameObject.Find("teaL1");
        cupTopGhost = GameObject.Find("cupTop");
        //cupTopPos = cupTopGhost.transform.position.y;
       // Debug.Log("cupTopPos is: " + cupTopPos);

    }

	private void FixedUpdate()
    {
        //cupTopGhost.
        //cupTopPos++;
        tempValue = tempSlider.getSliderOutput();
        timeValue = timeSlider.getSliderOutput();
        humidityValue = HumiditySlider.getSliderOutput();
        //pressureValue = pressureSlider.getSliderOutput();
		L1 = computeL1();
		

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
        time = timeValue;
        r_h = humidityValue;
      //  float tempTime;

		P_star = Mathf.Pow(10, 29.8605f - (3152.2f / temp) - 7.3037f * Mathf.Log10(temp) + (2.4247f * Mathf.Pow(10, -9) * temp) + (1.809f * Mathf.Pow(10, -6) * temp * temp));
		Y_A0 = P_star / P;
		Y_AL = r_h * Y_A0;

		/*time = Time.time * 60 * 24; // 1 min is 1 day
        Debug.Log("time is = " + time);
        */
       // tempTime = 

		L1 = (2 / CA_liq) * ((-P * D_AB * Mathf.Log((1 - Y_A0) / (1 - Y_AL)) * time) / (R * time)) + (L0 * L0);
        Debug.Log("FIRST L1!!! " + L1);
        L1 = Mathf.Sqrt(L1);

        Debug.Log("L1!!! " + L1);

        return L1;
	}

    public float getTemp() {
        return temp;
    }

    public float getTime()
    {
        return time;
    }
    public float getL1()
    {
        return L1;
    }
    public float getY_A0()
    {
        return Y_A0;
    }
    public float getY_AL()
    {
        return Y_AL;
    }

    public float getr_h()
    {
        return r_h;
    }


    /*
    public void moveTea(float translation)//float angle)
    {
       // Debug.Log(this.transform.position.z);
        //this.;
        //TempCubeStart.position;
        Vector3 temporary = Vector3.Lerp(this.transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + translation), 0.5f);

        this.transform.position = new Vector3(temporary.x, temporary.y, Mathf.Clamp(temporary.z, endPos.z, startPos.z));//endPos.position.z));

        percentageSliderMoved = 1 - ((this.transform.position.z - endPos.z) / (startPos.z - endPos.z));

        slider.value = ((slider.maxValue - slider.minValue) * percentageSliderMoved) + slider.minValue;
        sliderOutput = slider.value;

        //this.transform.localPosition = 
        //Debug.Log(Mathf.Clamp(transform.position.z, -75, 75));
    }*/
}