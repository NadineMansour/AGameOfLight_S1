using UnityEngine;
using System.Collections;

public class GoLeftScript : MonoBehaviour 
{


	public void OnMouseDown() 
	{
		ShooterScript5.movement(3);
	}
	
	
	public void OnMouseUp()
	{
		ShooterScript5.movement(4);
	}
}
