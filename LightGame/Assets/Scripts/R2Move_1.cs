using UnityEngine;
using System.Collections;


public class R2Move_1 : MonoBehaviour {

	
	void Start () {
	
	}

	//during the 3rd 5 secs (between 10th and 15th seconds) the halo around the rotation buttons is on
	//then it is disabled on the 15th second
	void Update()
	{	
		//This halo appears in level 1 only
		if (Time.timeSinceLevelLoad > 10.0f && Time.timeSinceLevelLoad  < 15.0f && Application.loadedLevelName == "Level1") {
			Component halo = GetComponent ("Halo"); 
			halo.GetType ().GetProperty ("enabled").SetValue (halo, true, null);			
		} else {
			Component halo = GetComponent ("Halo"); 
			halo.GetType ().GetProperty ("enabled").SetValue (halo, false, null);			
		}
	}


	public void OnMouseDown() 
	{
		if (!ShooterScript_1.RDown) 
		{
			ShooterScript_1.clicks++;
			ShooterScript_1.log += "-Rotation cw, started: " + ShooterScript_1.angle;
		}
		ShooterScript_1.RDown = true;
	}


	public void OnMouseUp()
	{
		if (ShooterScript_1.RDown)
			ShooterScript_1.log += ", ended: " + ShooterScript_1.angle;
		ShooterScript_1.RDown = false;
	}
}
