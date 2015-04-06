using UnityEngine;
using System.Collections;


public class UpMove_2 : MonoBehaviour {

	public GameObject Shooter;
	
	void Start () {
	}
	
	void Update()
	{
		//during the 1st 5 secs the halo around the up button is on
		//then it is disabled on the 5th second
		//This halo appears in level 1 only
		if (Time.timeSinceLevelLoad > 5.0f || !(Application.loadedLevelName == "Level1")) 
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
		if (!ShooterScript_2.up && ShooterScript_2.state==1) 
		{
			ShooterScript_2.clicks++;
			ShooterScript_2.log += "-Moving up, yStart: " + Shooter.transform.position.y;
		}
		ShooterScript_2.up = true;
	}


	public void OnMouseUp()
	{
		if (ShooterScript_2.up) 
		{
			ShooterScript_2.log += ", yEnd: " + Shooter.transform.position.y;
		}
		ShooterScript_2.up = false;
	}
}
