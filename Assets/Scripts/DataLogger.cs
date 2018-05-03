using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class DataLogger : MonoBehaviour {

	[SerializeField]
	private WaterController water;
	[SerializeField]
	private BathTubController bathTub;
	[SerializeField]
	private ValveController valveIn;
	[SerializeField]
	private ValveController valveOut;

	private string timestamp;
	private string path;
	private string path_waterLevel;
	private string path_FlowRates;


	private void Start()
	{
		timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
		path = Directory.GetCurrentDirectory() + "\\log_" + timestamp + ".txt";
		path_waterLevel = Directory.GetCurrentDirectory() + "\\log_" + timestamp + "_waterLevel" + ".txt";
		path_FlowRates = Directory.GetCurrentDirectory() + "\\log_" + timestamp + "_flowRates" + ".txt";

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
		StartCoroutine(doLogFlowRates());
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
	 * Logs water level every 0.25 sec
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
					file.WriteLine(string.Format("{0},{1}", Time.time, waterLevel));
				}
			}
			else
			{
				string[] contents = { string.Format("{0},{1}", Time.time, waterLevel) };
				System.IO.File.WriteAllLines(path_waterLevel, contents);
			}
			yield return new WaitForSeconds(0.25f);
		}

	}

	/**
	 * Logs FlowIn, FlowOut, dV, every 0.25 sec
	 */
	IEnumerator doLogFlowRates()
	{
		while (true)
		{

			float Q_in = valveIn.getFlowRate();
			float Q_out = valveOut.getFlowRate();
			float dV = bathTub.get_dV();

			if (File.Exists(path_FlowRates))
			{
				using (System.IO.StreamWriter file =
					new System.IO.StreamWriter(path_FlowRates, true))
				{
					file.WriteLine(string.Format("{0},{1},{2},{3}", Time.time, Q_in, Q_out, dV));
				}
			}
			else
			{
				string[] contents = { string.Format("{0},{1},{2},{3}", Time.time, Q_in, Q_out, dV) };
				System.IO.File.WriteAllLines(path_FlowRates, contents);
			}
			yield return new WaitForSeconds(0.25f);
		}

	}
}
