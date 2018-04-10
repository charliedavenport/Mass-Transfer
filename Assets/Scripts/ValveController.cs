using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveController : MonoBehaviour {

	[SerializeField]
	private Material defaultMat;
	[SerializeField]
	private Material selectedMat;

	private float start_rot_y;
    private Quaternion min_rot;
    private Quaternion max_rot;

    float min_y, max_y;

    private float total_angle;
    private float max_angle;
    // assume min_angle is 0

    public void select() {
        GetComponent<Renderer>().material = selectedMat;
    }

    public void deselect() {
        GetComponent<Renderer>().material = defaultMat;
    }

    private void Start()
	{
        total_angle = 0f;

        start_rot_y = this.transform.eulerAngles.y;
        min_y = start_rot_y;
        max_y = start_rot_y + 180;
        min_rot = this.transform.rotation;
        max_rot = Quaternion.Euler(min_rot.eulerAngles.x, max_y, min_rot.eulerAngles.z);
	}

	private void OnTriggerEnter(Collider other) {
		//Debug.Log("OnTriggerEnter");
        if (other.gameObject.tag == "Player") {
			//change valve material to selected
			GetComponent<Renderer>().material = selectedMat;
			// handController 'grabs' this valve
			other.GetComponent<HandController>().selectValve(this);
		}
    }

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {

			//change valve material to default
			//GetComponent<Renderer>().material = defaultMat;

			//other.GetComponent<HandController>().deselectValve();
		}
	}

	public void rotateValve(float angle) {
        
        total_angle += angle;
        if (total_angle <= max_angle) {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up) * transform.rotation;
        }
        else {
            total_angle = max_angle;
        }

	}

  

}
