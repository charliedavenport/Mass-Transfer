﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{/*

    [SerializeField]
    private GameObject cup;
    //[SerializeField]
    //private ParticleSystem flow_ps;

    [SerializeField]
    private Material defaultMat;
    [SerializeField]
    private Material selectedMat;
    private Quaternion start_rot;
    private float total_angle;

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

        start_rot = transform.rotation;

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

            if (!other.GetComponent<HandController>().grabbing)
            { // not grabbing
                deselect();
                other.GetComponent<HandController>().releaseSlider();
            }
            //change valve material to default
            //GetComponent<Renderer>().material = defaultMat;

            //other.GetComponent<HandController>().deselectValve();
        }
    }

    public void rotateSlider(float angle)
    {

        float max_angle = -180f;

        total_angle += angle;

        //Debug.Log(total_angle);

        if (total_angle < max_angle)
        {
            total_angle = max_angle;
            transform.rotation = Quaternion.AngleAxis(max_angle, Vector3.up) * start_rot;
            /*if (!flow_ps.isPlaying)
            {
                flow_ps.Play();
            }*/
      /*  }
        else if (total_angle > 0)
        {
            total_angle = 0;
            transform.rotation = start_rot;
            /*if (flow_ps.isPlaying)
            {
                flow_ps.Stop();
            }*/
 /*       }
        else
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up) * transform.rotation;
            /*if (!flow_ps.isPlaying)
            {
                flow_ps.Play();
            }*/
       // }

        //update flow rate
 /*       sliderOutput = -total_angle; // ranges from 0 to 90 right now

    }

    public float getSliderOutput() { return sliderOutput; }*/

}
