using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupGUIController : MonoBehaviour
{

   [SerializeField]
    private Text tempText;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text humidityText;
    [SerializeField]
    private SliderController tempSlider;//was valve_in
    [SerializeField]
    private SliderController timeSlider;
    [SerializeField]
    private SliderController humiditySlider;

    private float temp;
    private float time;
    private float humidity;

    public void setFlowRate(float tempInput, float timeInput, float humidityInput)
    {
        temp = tempInput;
        time = timeInput;
        humidity = humidityInput;
    }

    private void Awake()
    {
        temp = 0f;
        time = 0f;
        humidity = 0f;
    }

    private void Update()
    {
        tempText.text = "Temperature: " + tempSlider.getSliderOutput().ToString("F");
        timeText.text = "Time: " + timeSlider.getSliderOutput().ToString("F");
        humidityText.text = "Humidity: " + humiditySlider.getSliderOutput().ToString("F");

    }


}