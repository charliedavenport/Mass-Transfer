using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayer : MonoBehaviour {

    [SerializeField]
    private Transform Head;
    [SerializeField]
    private Transform LeftHand;
    [SerializeField]
    private Transform RightHand;

    // from steamvr camera-rig
    [SerializeField]
    private Transform LeftController; 
    [SerializeField]
    private Transform RightController;

    private void Update()
    {
        LeftHand.position = LeftController.position;
        LeftHand.rotation = LeftController.rotation;

        RightHand.position = RightController.position;
        RightHand.rotation = RightController.rotation;
    }

}
