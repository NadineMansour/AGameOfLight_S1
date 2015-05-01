using UnityEngine;
using System.Collections;

public class rotate_level4 : MonoBehaviour 
{

	public bool right;
	public bool left;


	void OnMouseDown()
	{
		if (right) 
		{
			reflection_level_4.RotateRightTrue();
		} 
		else 
		{
			if (left) 
			{
				reflection_level_4.RotateLeftTrue();
			}
		}
	}

	
	void OnMouseUp()
	{
		if (right) 
		{
			reflection_level_4.RotateRightFalse();
		}
		else 
		{
			if (left) 
			{
				reflection_level_4.RotateLeftFalse();
			}
		}
	}
}
