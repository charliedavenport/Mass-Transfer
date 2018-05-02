using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterController : MonoBehaviour {

    [SerializeField]
    private float waterLevel;
    [SerializeField]
    private bool overflowing;
	[SerializeField]
	private ValveController valveIn;
	[SerializeField]
	private ValveController valveOut;
	[SerializeField]
	private ParticleSystem[] overflow_ps;


    private float maxWaterLevel = 2f;
    private float max_y_scale, max_y_pos, min_y_pos; // adjust these every frame depending on water level
    private float scale_factor, pos_factor; // used to calculate cube dimensions in update

    public void setWaterLevel(float f)
    {
        waterLevel = f;
    }

	public float getWaterLevel()
	{
		return waterLevel;
	}

    public void incrementWaterLevel(float dy)
    {
        waterLevel += dy;
        if (waterLevel > maxWaterLevel) waterLevel = maxWaterLevel;
        else if (waterLevel < 0f) waterLevel = 0f;
    }

    public float getVolume()
    {
        return waterLevel * 4f * 3f; // rectangular dimensions of tub
    }

    private void Awake()
    {
        waterLevel = 0.1f;
        max_y_pos = transform.position.y;
        min_y_pos = 0.2f;
        max_y_scale = transform.localScale.y;

        overflowing = false;

        scale_factor = max_y_scale / maxWaterLevel;
        pos_factor = (max_y_pos - min_y_pos) / maxWaterLevel;
        Debug.Log("pos_factor: " + pos_factor);
        Debug.Log("scale_factor: " + scale_factor);
    }

    private void Update()
    {
        if (waterLevel < 0f) // do not draw water at all if level is 0
        {
            waterLevel = 0f;
            this.GetComponent<MeshRenderer>().enabled = false;
            overflowing = false; 
        }
        else if (waterLevel >= maxWaterLevel)
        {
            this.GetComponent<MeshRenderer>().enabled = true;

            waterLevel = maxWaterLevel;
            // overflow if full and water is still flowing in
            overflowing = (valveIn.getFlowRate() > 0f);
			
        }
        else
        {
            this.GetComponent<MeshRenderer>().enabled = true;

            overflowing = false;

            // set y position and scale to change water level "in place"
            transform.localScale = new Vector3(transform.localScale.x,
                scale_factor * waterLevel,
                transform.localScale.z);
            transform.position = new Vector3(transform.position.x,
                min_y_pos + (scale_factor / 2f * waterLevel),
                transform.position.z);

        }

		if (overflowing)
		{
			for (int i=0; i<4; i++)
			{
				if (!overflow_ps[i].isPlaying) overflow_ps[i].Play();
			}
		}
		else
		{
			for (int i = 0; i < 4; i++)
			{
				if (overflow_ps[i].isPlaying) overflow_ps[i].Stop();
			}
		}

    }


}
