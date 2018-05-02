using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupGUIController : MonoBehaviour
{

   [SerializeField]
    private Text tempText;
    [SerializeField]
    private Text humidityText;
    [SerializeField]
    private Text pressureText;
    [SerializeField]
    private SliderController tempSlider;//was valve_in
    [SerializeField]
    private SliderController humiditySlider;
    [SerializeField]
    private SliderController pressureSlider;

    private float temp;
    private float humidity;
    private float pressure;

    public void setFlowRate(float tempInput, float humidityInput, float pressureInput)
    {
        temp = tempInput;
        humidity = humidityInput;
        pressure = pressureInput;
    }

    private void Awake()
    {
        temp = 0f;
        humidity = 0f;
        pressure = 0f;
    }

    private void Update()
    {
        tempText.text = "Temperature: " + tempSlider.getSliderOutput().ToString("F");
        humidityText.text = "Humidity: " + humiditySlider.getSliderOutput().ToString("F");
        pressureText.text = "Pressure: " + pressureSlider.getSliderOutput().ToString("F");

    }


}