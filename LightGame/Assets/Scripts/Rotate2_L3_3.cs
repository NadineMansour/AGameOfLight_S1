using UnityEngine;
using System.Collections;

public class Rotate2_L3_3 : MonoBehaviour {

	//called when the rotate anti clockwise button is pressed 
	public void OnMouseDown() 
	{
		Shooter_L3_3.RRight = true;
	}
	
	
	//called when the rotate anti clockwise button is released  
	public void OnMouseUp()
	{
		Shooter_L3_3.RRight = false;
	}
}
