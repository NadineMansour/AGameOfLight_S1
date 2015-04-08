using UnityEngine;
using System.Collections;

public class rotateButtons : MonoBehaviour 
{
	public bool right;
	public bool left;


	void OnMouseDown()
	{
		if (right) 
		{
			reflection_level_3.RotateRightTrue ();
		} 
		else 
		{ 
			if (left) 
			{
				reflection_level_3.RotateLeftTrue ();
			}
		}
	}


	void OnMouseUp()
	{
		if (right) 
		{
			reflection_level_3.RotateRightFalse ();
		} 
		else 
		{
			if (left) 
			{
				reflection_level_3.RotateLeftFalse ();
			}
		}
	}
}