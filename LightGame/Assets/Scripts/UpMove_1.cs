using UnityEngine;
using System.Collections;


public class UpMove_1 : MonoBehaviour {

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
		if (!ShooterScript_1.up) 
		{
			ShooterScript_1.clicks++;
			ShooterScript_1.log += "-Moving up, yStart: " + Shooter.transform.position.y;
		}
		ShooterScript_1.up = true;
	}


	public void OnMouseUp()
	{
		if (ShooterScript_1.up) 
		{
			ShooterScript_1.log += ", yEnd: " + Shooter.transform.position.y;
		}
		ShooterScript_1.up = false;
	}
}
