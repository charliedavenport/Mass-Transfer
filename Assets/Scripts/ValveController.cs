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
        Debug.Log(start_rot_y);
        min_y = start_rot_y;
        max_y = start_rot_y + 90;
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

            if (!other.GetComponent<HandController>().grabbing)
            { // not grabbing
                deselect();
                other.GetComponent<HandController>().releaseValve();
            }
			//change valve material to default
			//GetComponent<Renderer>().material = defaultMat;

			//other.GetComponent<HandController>().deselectValve();
		}
	}

	public void rotateValve(float angle) {
        
        total_angle += angle;
        float relative_angle = start_rot_y + total_angle;
        if (relative_angle <= max_y && relative_angle <= min_y) {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up) * transform.rotation;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
        else if (relative_angle > max_y) {
            Debug.Log("max angle");
            total_angle = max_angle;
        }
        else if (relative_angle < min_y)
        {
            Debug.Log("min angle");
            total_angle = min_y;
        }

	}

  

}
