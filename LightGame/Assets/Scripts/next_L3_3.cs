using UnityEngine;
using System.Collections;

public class next_L3_3 : MonoBehaviour {


	void OnMouseDown()
	{
		if (Shooter_L3_3.state == 3) 
		{
			Application.LoadLevel ("End");

		} 
		else 
		{
			if (Shooter_L3_3.state == 1)
			{
				Shooter_L3_3.startTime = (int)Time.timeSinceLevelLoad;
			}
			Shooter_L3_3.state++;
		}
	}
}
