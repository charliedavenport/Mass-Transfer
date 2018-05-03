using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class DataLogger : MonoBehaviour {

	[SerializeField]
	private WaterController water;

	private string timestamp;
	private string path;
	private string path_waterLevel;

	private void Start()
	{
		timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
		path = Directory.GetCurrentDirectory() + "\\log_" + timestamp + ".txt";
		path_waterLevel = Directory.GetCurrentDirectory() + "\\log_" + timestamp + "_waterLevel" + ".txt";

		if (File.Exists(path))
		{
			using (System.IO.StreamWriter file =
				new System.IO.StreamWriter(path, true))
			{
				file.WriteLine(string.Format("DATA LOG FOR MASS TRANSFER {0}\n", timestamp));
			}
		}
		else
		{
			string[] contents = { string.Format("DATA LOG FOR MASS TRANSFER {0}\n", timestamp)};
			System.IO.File.WriteAllLines(path, contents );
		}

		StartCoroutine(doLogWaterLevel());
	}//Start

	/**
	 * Writes some string to the data log as a new line (append)
	 */ 
	public void logSomething(string something)
	{
		if (File.Exists(path))
		{
			using (System.IO.StreamWriter file =
				new System.IO.StreamWriter(path, true))
			{
				file.WriteLine(something);
			}
		}
	}


	/**
	 * Logs water level every second
	 */
	IEnumerator doLogWaterLevel()
	{
		while (true)
		{

			float waterLevel = water.getWaterLevel();

			if (File.Exists(path_waterLevel))
			{
				using (System.IO.StreamWriter file =
					new System.IO.StreamWriter(path_waterLevel, true))
				{
					file.WriteLine(string.Format("Tub water level at t={0}:\t{1}", Time.time, waterLevel));
				}
			}
			else
			{
				string[] contents = { string.Format("Tub water level at t={0}:\t{1}", Time.time, waterLevel) };
				System.IO.File.WriteAllLines(path_waterLevel, contents);
			}
			yield return new WaitForSeconds(1);
		}

	}
}
