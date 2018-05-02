using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterController : MonoBehaviour {

    [SerializeField]
    private float waterLevel;

    private float maxWaterLevel = 2f;
    private float max_y_scale, max_y_pos, min_y_pos; // adjust these every frame depending on water level
    private float scale_factor, pos_factor; // used to calculate cube dimensions in update

    public void setWaterLevel(float f)
    {
        waterLevel = f;
    }

    public float getVolume()
    {
        return waterLevel * 4f * 3f; // rectangular dimensions of tub
    }

    private void Awake()
    {
        waterLevel = 0f;
        max_y_pos = transform.position.y;
        min_y_pos = 0.2f;
        max_y_scale = transform.localScale.y;

        scale_factor = max_y_scale / maxWaterLevel;
        pos_factor = (max_y_pos - min_y_pos) / maxWaterLevel;
        Debug.Log("pos_factor: " + pos_factor);
        Debug.Log("scale_factor: " + scale_factor);
    }

    private void Update()
    {
        if (waterLevel <= 0f) // do not draw water at all if level is 0
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            this.GetComponent<MeshRenderer>().enabled = true;

            // set y position and scale to change water level "in place"
            transform.localScale = new Vector3(transform.localScale.x,
                scale_factor * waterLevel,
                transform.localScale.z);
            transform.position = new Vector3(transform.position.x,
                min_y_pos + (scale_factor / 2f * waterLevel),
                transform.position.z);

        }
    }


}
