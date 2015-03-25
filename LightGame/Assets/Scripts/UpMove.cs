using UnityEngine;
using System.Collections;


public class UpMove : MonoBehaviour {

	public float StartTime;
	
	void Start () {
		StartTime = Time.realtimeSinceStartup;
	}
	
	void Update()
	{
		//during the 1st 5 secs the halo around the up button is on
		//then it is disabled on the 5th second
		//This halo appears in level 1 only
		if (Time.realtimeSinceStartup - StartTime > 5.0f || !(Application.loadedLevelName == "Level1")) 
		{
			Component halo = GetComponent ("Halo"); 
			halo.GetType ().GetProperty ("enabled").SetValue (halo, false, null);
		} 
		else 
		{
			Component halo = GetComponent ("Halo"); 
			halo.GetType ().GetProperty ("enabled").SetValue (halo, true, null);
		}
	}
	
	public void OnMouseDown() 
	{
		PlayerScript.UpTrue();
	}


	public void OnMouseUp()
	{
		PlayerScript.UpFalse ();
	}
}
