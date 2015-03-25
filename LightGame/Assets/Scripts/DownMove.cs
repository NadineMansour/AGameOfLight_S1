using UnityEngine;
using System.Collections;

public class DownMove : MonoBehaviour {

	public float StartTime;
	
	void Start () {
		StartTime = Time.realtimeSinceStartup;
	}

	void Update()
	{
		//during the 2nd 5 secs (between 5th and 10th seconds) the halo around the down button is on
		//then it is disabled after the 10th second
		//This halo appears in level 1 only
		if (Time.realtimeSinceStartup - StartTime > 5.0f && Time.realtimeSinceStartup - StartTime < 10.0f && Application.loadedLevelName == "Level1") {
				Component halo = GetComponent ("Halo"); 
				halo.GetType ().GetProperty ("enabled").SetValue (halo, true, null);
			} 
			else {
				Component halo = GetComponent ("Halo"); 
				halo.GetType ().GetProperty ("enabled").SetValue (halo, false, null);
			}

	}


	public void OnMouseDown() 
	{
		PlayerScript.DownTrue();
	}

	
	public void OnMouseUp()
	{
		PlayerScript.DownFalse ();
	}
}
