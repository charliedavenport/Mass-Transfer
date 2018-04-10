using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveController : MonoBehaviour {

	[SerializeField]
	private Material defaultMat;
	[SerializeField]
	private Material selectedMat;

	private Quaternion start_rot;

	private void Start()
	{
		start_rot = this.transform.rotation;
	}

	private void OnTriggerEnter(Collider other) {
		//Debug.Log("OnTriggerEnter");
        if (other.gameObject.tag == "Player") {
			//change valve material to selected
			GetComponent<Renderer>().material = selectedMat;
			// handController 'grabs' this valve
			other.GetComponent<HandController>().grabValve(this);
		}
    }

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {

			//change valve material to default
			GetComponent<Renderer>().material = defaultMat;

			other.GetComponent<HandController>().releaseValve();
		}
	}

	public void rotateValve(float angle) {
		transform.Rotate(new Vector3(0, angle, 0), Space.World);

	}

}
