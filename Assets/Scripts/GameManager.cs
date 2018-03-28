using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class GameManager : MonoBehaviour {

    public GameObject vrCameraRig;
    public GameObject nonVRCameraRig;

    public SteamVR_TrackedObject hmd;
    public SteamVR_TrackedObject leftController;
    public SteamVR_TrackedObject rightController;


    public void enableVR() {
        StartCoroutine(doEnableVR());
    }


    IEnumerator doEnableVR() {

        while (UnityEngine.XR.XRSettings.loadedDeviceName != "OpenVR") {
            UnityEngine.XR.XRSettings.LoadDeviceByName("OpenVR");
            yield return null;
        }
        UnityEngine.XR.XRSettings.enabled = true;
        vrCameraRig.SetActive(true);
        nonVRCameraRig.SetActive(false);
    }
}
