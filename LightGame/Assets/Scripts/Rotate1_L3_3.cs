using UnityEngine;
using System.Collections;

public class Rotate1_L3_3 : MonoBehaviour {


	//called when the rotate clockwise button is pressed 
	public void OnMouseDown() 
	{
		Shooter_L3_3.RLeft = true;
	}
	
	
	//called when the rotate clockwise button is released  
	public void OnMouseUp()
	{
		Shooter_L3_3.RLeft = false;
	}


}
