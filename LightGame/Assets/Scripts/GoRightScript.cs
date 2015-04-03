using UnityEngine;
using System.Collections;

public class GoRightScript : MonoBehaviour 
{


	public void OnMouseDown() 
	{
		ShooterScript5.movement(1);
	}
	
	
	public void OnMouseUp()
	{
		ShooterScript5.movement(2);
	}
}
