using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Quaternion start_rot;
    private Vector3 startPos;
    private float total_angle;
    private float totalSliderValue;

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
        totalSliderValue = 0f;

        start_rot = transform.rotation;
        startPos = transform.position;

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
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + translation), 0.5f);

    }

    public float getSliderOutput() { return sliderOutput; }

}
