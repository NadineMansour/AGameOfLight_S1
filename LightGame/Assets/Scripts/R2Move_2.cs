using UnityEngine;
using System.Collections;


public class R2Move_2 : MonoBehaviour {

	
	void Start () {
	
	}

	//during the 3rd 5 secs (between 10th and 15th seconds) the halo around the rotation buttons is on
	//then it is disabled on the 15th second
	void Update()
	{	
		//This halo appears in level 1 only
		if (Time.timeSinceLevelLoad > 10.0f && Time.timeSinceLevelLoad  < 15.0f && Application.loadedLevelName == "Level2") {
			Component halo = GetComponent ("Halo"); 
			halo.GetType ().GetProperty ("enabled").SetValue (halo, true, null);			
		} else {
			Component halo = GetComponent ("Halo"); 
			halo.GetType ().GetProperty ("enabled").SetValue (halo, false, null);			
		}
	}


	public void OnMouseDown() 
	{
		if (!ShooterScript_2.RDown && ShooterScript_2.state==1) 
		{
			ShooterScript_2.clicks++;
			ShooterScript_2.log += "-Rotation cw, started: " + ShooterScript_2.angle;
		}
		ShooterScript_2.RDown = true;
	}


	public void OnMouseUp()
	{
		if (ShooterScript_2.RDown)
			ShooterScript_2.log += ", ended: " + ShooterScript_2.angle;
		ShooterScript_2.RDown = false;
	}
}
