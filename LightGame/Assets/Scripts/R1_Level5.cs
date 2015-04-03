using UnityEngine;
using System.Collections;


public class R1_Level5 : MonoBehaviour
{


	//called when the rotate anti clockwise button is pressed 
	public void OnMouseDown() 
	{
		ShooterScript5.movement (5);
	}
	

	//called when the rotate anti clockwise button is released  
	public void OnMouseUp()
	{
		ShooterScript5.movement (6);
	}
}