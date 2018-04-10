using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

	[SerializeField]
	private VRPlayer player;
	public Vector3 controllerVelocity;
	public Vector3 controllerAngularVelocity;
	private ValveController currentValve;

	private Quaternion stored_rot;	

	private void Awake() {
		currentValve = null;
	}

	private void Start() {
		// get rot on first frame
		stored_rot = this.transform.rotation;
	}

	private void FixedUpdate() {
		
		if (currentValve != null) {
			int indexRight = (int)player.RightController.index;
			bool a_btn = SteamVR_Controller.Input(indexRight).GetPress(Valve.VR.EVRButtonId.k_EButton_A);
			if (a_btn)
			{
				// rotate valve using controller
				Quaternion current_rot = this.transform.rotation;
				float current_rot_y = current_rot.eulerAngles.y;
				float stored_rot_y = stored_rot.eulerAngles.y;
				float diff = current_rot_y - stored_rot_y;

				currentValve.rotateValve(diff);

				stored_rot = current_rot;
			}
		}

    }

    public void grabValve(ValveController valve) {
		currentValve = valve;
	}

	public void releaseValve() {
		currentValve = null;
	}


}
