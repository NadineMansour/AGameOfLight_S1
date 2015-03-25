using UnityEngine;
using System.Collections;


public class R2Move : MonoBehaviour {

	public float StartTime;
	
	void Start () {
		StartTime = Time.realtimeSinceStartup;
	}

	//during the 3rd 5 secs (between 10th and 15th seconds) the halo around the rotation buttons is on
	//then it is disabled on the 15th second
	void Update()
	{	
		//This halo appears in level 1 only
		if (Time.realtimeSinceStartup - StartTime > 10.0f && Time.realtimeSinceStartup - StartTime < 15.0f && Application.loadedLevelName == "Level1") {
			Component halo = GetComponent ("Halo"); 
			halo.GetType ().GetProperty ("enabled").SetValue (halo, true, null);			
		} else {
			Component halo = GetComponent ("Halo"); 
			halo.GetType ().GetProperty ("enabled").SetValue (halo, false, null);			
		}
	}


	public void OnMouseDown() 
	{
		PlayerScript.RDownTrue();
	}


	public void OnMouseUp()
	{
		PlayerScript.RDownFalse();
	}
}
