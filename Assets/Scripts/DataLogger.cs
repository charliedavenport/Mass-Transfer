using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLogger : MonoBehaviour {

	[SerializeField]
	private WaterController water;


	private void Start()
	{
		
	}

	IEnumerator doLogWaterLevel()
	{
		yield return new WaitForSeconds(1);
	}
}
