using UnityEngine;
using System.Collections;

public class Rotate2_L3_3 : MonoBehaviour {


	public GameObject Shooter;


	//called when the rotate anti clockwise button is pressed 
	public void OnMouseDown() 
	{
		if (Shooter_L3_3.state == 2) 
		{
			if(!Shooter_L3_3.RRight)
			{
				Shooter_L3_3.clicks++;
				float angle = Shooter.transform.rotation.z * Mathf.Rad2Deg;
				Shooter_L3_3.log += "-Rotation ccw, angleStart: " + angle + ", ";
			}
			Shooter_L3_3.RRight = true;
		}
	}
	
	
	//called when the rotate anti clockwise button is released  
	public void OnMouseUp()
	{
		if (Shooter_L3_3.state == 2) 
		{
			if(Shooter_L3_3.RRight)
			{
				float angle = Shooter.transform.rotation.z * Mathf.Rad2Deg;
				Shooter_L3_3.log += "angleEnd: " + angle + '\n';
			}
			Shooter_L3_3.RRight = false;
		}
	}
}
