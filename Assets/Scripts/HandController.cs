using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

	[SerializeField]
	private VRPlayer player;
	public Vector3 controllerVelocity;
	public Vector3 controllerAngularVelocity;
	private ValveController currentValve;

    private int controllerIndex;

    public bool grabbing;

    public void setControllerIndex(int ind) {
        controllerIndex = ind;
    }

	private void Awake() {
		currentValve = null;
	}

	private void Start() {
        grabbing = false;
		// get rot on first frame
		//stored_rot = this.transform.rotation;
	}

	private void Update() {

        if (currentValve != null) {
			bool a_btn_down = SteamVR_Controller.Input(controllerIndex).GetPressDown(Valve.VR.EVRButtonId.k_EButton_A);
			if (a_btn_down && !grabbing)
			{
                //currentValve.rotateValve(0f);

                StartCoroutine(doGrabValve());
			}
		}


    }

    public void selectValve(ValveController valve) {
		currentValve = valve;

    }

	public void releaseValve() {
		currentValve = null;
	}

    IEnumerator doGrabValve() {
        grabbing = true;
        while (true) {
            bool a_btn_up = SteamVR_Controller.Input(controllerIndex).GetPressUp(Valve.VR.EVRButtonId.k_EButton_A);
            if (a_btn_up) {
                Debug.Log("a_btn_up");
                currentValve.deselect();
                break;
            }

            float offset = Vector3.Dot((transform.position - currentValve.transform.position), Vector3.right);
            currentValve.rotateValve(offset * Mathf.Rad2Deg);

            yield return new WaitForFixedUpdate();
        }
        grabbing = false;
    }


}
