using UnityEngine;
using System.Collections;

public class DownMove_1 : MonoBehaviour {

	public GameObject Shooter;
	
	void Start () {
	}

	void Update()
	{
		//during the 2nd 5 secs (between 5th and 10th seconds) the halo around the down button is on
		//then it is disabled after the 10th second
		//This halo appears in level 1 only
		if (Time.timeSinceLevelLoad  > 5.0f && Time.timeSinceLevelLoad < 10.0f && Application.loadedLevelName == "Level1") {
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
		if (!ShooterScript_1.down && ShooterScript_1.state==2) 
		{
			ShooterScript_1.clicks++;
			ShooterScript_1.log += "-Moving down, yStart: " + Shooter.transform.position.y;
		}
		ShooterScript_1.down = true;
	}

	
	public void OnMouseUp()
	{
		if (ShooterScript_1.down) 
		{
			ShooterScript_1.log += ", yEnd: " + Shooter.transform.position.y;
		}
		ShooterScript_1.down = false;
	}
}
