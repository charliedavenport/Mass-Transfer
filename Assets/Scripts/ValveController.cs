using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveController : MonoBehaviour {

    [SerializeField]
    private GameObject bathTub;
    [SerializeField]
    private ParticleSystem flow_ps;

	[SerializeField]
	private Material defaultMat;
	[SerializeField]
	private Material selectedMat;
    private Quaternion start_rot;
    private float total_angle;

    [SerializeField]
    private float flow_rate; //in or out depending on which valve this is

    public void select() {
        GetComponent<Renderer>().material = selectedMat;
    }

    public void deselect() {
        GetComponent<Renderer>().material = defaultMat;
    }

    private void Start()
	{
        total_angle = 0f;

        start_rot = transform.rotation;

        //bathTub = GameObject.Find("BathTub");
	}

    private void Update()
    {
        if (flow_rate > 0 && !flow_ps.isPlaying)
        {
            flow_ps.Play();
        }
        else if (flow_rate == 0 && flow_ps.isPlaying)
        {
            flow_ps.Stop();
        }
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

        float max_angle = -180f;
        
        total_angle += angle;

        //Debug.Log(total_angle);

        if (total_angle < max_angle)
        {
            total_angle = max_angle;
            transform.rotation = Quaternion.AngleAxis(max_angle, Vector3.up) * start_rot;
        }
        else if (total_angle > 0)
        {
            total_angle = 0;
            transform.rotation = start_rot;

        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up) * transform.rotation;
        }

        //update flow rate
        flow_rate = -total_angle / 90f;

	}

    public float getFlowRate() { return flow_rate; }

}
