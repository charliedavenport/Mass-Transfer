using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

	[SerializeField]
	private VRPlayer player;
	public Vector3 controllerVelocity;
	public Vector3 controllerAngularVelocity;
    [SerializeField]
	private ValveController currentValve;
    private SliderController currentSlider;

    private int controllerIndex;

    public bool grabbing;

    public void setControllerIndex(int ind) {
        controllerIndex = ind;
    }

	private void Awake() {
		currentValve = null;
        currentSlider = null;
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

    public void selectSlider(SliderController slider){
        currentSlider = slider;
    }

	public void releaseValve() {
        if (currentValve != null)
        {
            currentValve.deselect();
        }
		currentValve = null;
	}

    public void releaseSlider(){
        if (currentSlider != null)
        {
            currentSlider.deselect();
        }
        currentSlider = null;
    }

    IEnumerator doGrabValve() {

        float prev_offset = 0f;

        grabbing = true;
        while (true) {
            bool a_btn_up = SteamVR_Controller.Input(controllerIndex).GetPressUp(Valve.VR.EVRButtonId.k_EButton_A);
            if (a_btn_up) {
                //Debug.Log("a_btn_up");
                releaseValve();
                break;
            }

            float offset = Vector3.Dot((transform.position - currentValve.transform.position), Vector3.right);
            float temp = offset;
            offset -= prev_offset;
            prev_offset = temp;
            currentValve.rotateValve(-offset * Mathf.Rad2Deg * 10f);

            yield return new WaitForFixedUpdate();
        }
        grabbing = false;
    }

    IEnumerator doGrabSlider()
    {

        float prev_offset = 0f;

        grabbing = true;
        while (true)
        {
            bool a_btn_up = SteamVR_Controller.Input(controllerIndex).GetPressUp(Valve.VR.EVRButtonId.k_EButton_A);
            if (a_btn_up)
            {
                //Debug.Log("a_btn_up");
                releaseSlider();
                break;
            }

            float offset = Vector3.Dot((transform.position - currentValve.transform.position), Vector3.right);
            float temp = offset;
            offset -= prev_offset;
            prev_offset = temp;
            currentSlider.rotateSlider(-offset * Mathf.Rad2Deg * 10f);

            yield return new WaitForFixedUpdate();
        }
        grabbing = false;
    }

}
