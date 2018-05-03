using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CupController : NetworkBehaviour
{

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
    private Slider tempUISlider;
    [SerializeField]
    private Slider humidityUISlider;
    [SerializeField]
    private float valveAngle;

    private const float width = 3f;
    private const float length = 4f;

    float tempUIValue;
    float humidityUIValue;

    //Equation variables
    float temp; // T (in Kelvins) //
    double r; //R 
    double p; //mmHg 
    double time; // t
    double concentrarion; // C_A(liq)
    double initialHeight; // L0
    double newHeight; // L1
    double diffusionCoef;// D_AB //
    double moleFrac; // Y_AO
    double relativeHumidity; //Y_AL

    //[SyncVar]
    //float flowRate_sync;

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
    }

    private void FixedUpdate()
    {

        tempValue = tempSlider.getSliderOutput();
        humidityValue = humiditySlider.getSliderOutput();
        pressureValue = pressureSlider.getSliderOutput();

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
        float CA_liq = 55f; // mol / L
        float R = 62.3637f; // L mmHg / mol K
        float P = 760f; // mmHg
        float D_AB = 0.0016f; // cm2 / day
        float L_0 = 1f; // m
        float r_h = 0.25f; // %

        float T = tempValue;

        float P_star = Mathf.Pow(10, 29.8605f - (3152.2f / T) - 7.3037f * Mathf.Log10(T) + (2.4247f * Mathf.Pow(10, -9) * T) + (1.809f * Mathf.Pow(10, -6) * T * T));
        float Y_a0 = P_star / P;
        float Y_aL = r_h * Y_a0;

        float t = Time.time;

        float L1 = (2 / CA_liq) * ((-P * D_AB * Mathf.Log((1 - Y_a0) / (1 - Y_aL)) * t) / (R * T)) + (L_0 * L_0);
        L1 = Mathf.Sqrt(L1);

        return L1;
    }


    /*
        public void setFlowInRate(float f)
        {
            flowInRate = f;
        }
        public void setFlowOutrate(float f)
        {
            flowOutRate = f;
        }

        [Command]
        void CmdSyncBathTub(float fRate)
        {
            flowInRate = fRate;
            //set syncvars
            flowRate_sync = fRate;
        }*/

}