using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cupController : MonoBehaviour {

    GameObject tea;
    GameObject tempButton;
    float teaPos;

    //Equation variables
   // float teaPosY;

    float temp; // T (in Kelvins) 
    float r; //R ???
    float p; //mmHg ???
    float time; // t
    float concentrarion; // C_A(liq)
    float initialHeight; // L0
    float newHeight; // L1
    float diffusionCoef// D_AB
    float moleFrac; // Y_AO
    float relativeHumidity; //Y_AL


    private void Awake()
    {
        tea = GameObject.Find("tea");
        tempButton = GameObject.Find("tempButton");

    }

    // Use this for initialization
    void Start()
    {
        teaPos = tea.transform.position.y;


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(tea.transform.position.y);
    }

    void onCollsionEnter(Collision col)
    {
        if (col.gameObject.name == "tempButton")
        {
            teaPos = tea.transform.position.y - 1; ;
        }
    }
}
