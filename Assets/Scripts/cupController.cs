using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cupController : MonoBehaviour {

    GameObject tea;
    GameObject tempButton;
    Vector3 teaPos;
    float teaPosY;

    private void Awake()
    {
        tea = GameObject.Find("tea");
        tempButton = GameObject.Find("tempButton");

    }

    // Use this for initialization
    void Start()
    {
        teaPos = tea.transform.position;


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
            teaPosY = tea.transform.position.y - 1; ;
        }
    }
}
