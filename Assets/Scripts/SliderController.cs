using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{

    [SerializeField]
    private GameObject cup;
    //[SerializeField]
    //private ParticleSystem flow_ps;

    [SerializeField]
    private Material defaultMat;
    [SerializeField]
    private Material selectedMat;
    [SerializeField]
    private Transform endTransform;
    private Vector3 endPos;
    private Quaternion start_rot;
    private Vector3 startPos;
    private float total_angle;
    //private float totalSliderValue;
    private float percentageSliderMoved;
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private float sliderOutput; //temperature, humidity, or time depending in or out depending on which valve this is //flow_rate

    public void select()
    {
        GetComponent<Renderer>().material = selectedMat;
    }

    public void deselect()
    {
        GetComponent<Renderer>().material = defaultMat;
    }

    private void Start()
    {
        total_angle = 0f;
        sliderOutput = 0;

        start_rot = transform.rotation;
        startPos = transform.position;
        endPos = endTransform.position;

        //bathTub = GameObject.Find("BathTub");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter");
        if (other.gameObject.tag == "Player")
        {
            //change valve material to selected
            GetComponent<Renderer>().material = selectedMat;
            // handController 'grabs' this valve
            other.GetComponent<HandController>().selectSlider(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (!other.GetComponent<HandController>().grabbingSlider)
            { // not grabbing
                deselect();
                other.GetComponent<HandController>().releaseSlider();
            }
            //change valve material to default
            //GetComponent<Renderer>().material = defaultMat;

            //other.GetComponent<HandController>().deselectValve();
        }
    }

    public void moveSlider(float translation)//float angle)
    {
        Debug.Log(this.transform.position.z);
        //this.;
            //TempCubeStart.position;
        Vector3 temporary = Vector3.Lerp(this.transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + translation), 0.5f);
        this.transform.position = new Vector3(temporary.x, temporary.y, Mathf.Clamp(temporary.z, endPos.z, startPos.z));//endPos.position.z));
        percentageSliderMoved =  1 - ( (this.transform.position.z - endPos.z) / (startPos.z - endPos.z) );

        slider.value = ( (slider.maxValue - slider.minValue) * percentageSliderMoved ) + slider.minValue;
        sliderOutput = slider.value;
        
        //this.transform.localPosition = 
        //Debug.Log(Mathf.Clamp(transform.position.z, -75, 75));
    }

    public float getSliderOutput() { return sliderOutput; }

}
