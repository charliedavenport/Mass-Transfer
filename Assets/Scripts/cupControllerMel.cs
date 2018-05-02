using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cupControllerMel : MonoBehaviour {

    GameObject tea;
    GameObject tempButton;
    double teaPos;

    //Equation variables
   // double teaPosY;

    float temp; // T (in Kelvins) //
    double r; //R ???
    double p; //mmHg ???
    double time; // t
    double concentrarion; // C_A(liq)
    double initialHeight; // L0
    double newHeight; // L1
    double diffusionCoef;// D_AB //
    double moleFrac; // Y_AO
    double relativeHumidity; //Y_AL


    private void Awake()
    {
        tea = GameObject.Find("tea");
        tempButton = GameObject.Find("tempButton");

    }

    // Use this for initialization
    void Start()
    {
        teaPos = tea.transform.position.y;
        diffusionCoef = 0.0016; //unit: mm2/s
        temp = 273; //unit: Kelvins 


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(tea.transform.position.y);
        Mathf.Clamp(temp, 273, 310); //unit: Kelvins (~32 -100 F)
    }

    void onCollsionEnter(Collision col)
    {
        if (col.gameObject.name == "tempButton")
        {
            teaPos = tea.transform.position.y - 1; ;
        }
    }
}
