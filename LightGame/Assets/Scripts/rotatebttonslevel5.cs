using UnityEngine;
using System.Collections;

public class rotatebttonslevel5 : MonoBehaviour 
{
	public bool right;
	public bool left;


	void OnMouseDown()
	{
		if (right) 
		{
			reflection_level_5.RotateRightTrue();
		} 
		else 
		{
			if (left) 
			{
				reflection_level_5.RotateLeftTrue();
			}
		}
	}

	
	void OnMouseUp()
	{
		if (right) 
		{
			reflection_level_5.RotateRightFalse();
		}
		else 
		{
			if (left) 
			{
				reflection_level_5.RotateLeftFalse();
			}
		}
	}
}
