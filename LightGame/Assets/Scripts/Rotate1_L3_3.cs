using UnityEngine;
using System.Collections;

public class Rotate1_L3_3 : MonoBehaviour {


	public GameObject Shooter;


	//called when the rotate clockwise button is pressed 
	public void OnMouseDown() 
	{
		if (Shooter_L3_3.state == 2) 
		{

			if(!Shooter_L3_3.RLeft)
			{
				Shooter_L3_3.clicks++;
				float angle = Shooter.transform.rotation.z * Mathf.Rad2Deg;
				Shooter_L3_3.log += "-Rotation cw, angleStart: " + angle +", ";
			}
			Shooter_L3_3.RLeft = true;
		}
	}
	
	
	//called when the rotate clockwise button is released  
	public void OnMouseUp()
	{
		if (Shooter_L3_3.state == 2) 
		{
			if(Shooter_L3_3.RLeft)
			{
				float angle = Shooter.transform.rotation.z * Mathf.Rad2Deg;
				Shooter_L3_3.log += "angleEnd: " + angle + '\n';

			}
			Shooter_L3_3.RLeft = false;
		}
	}


}
