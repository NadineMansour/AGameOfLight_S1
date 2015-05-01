using UnityEngine;
using System.Collections;


public class Movement_L3_2 : MonoBehaviour {


	void OnMouseDown()
	{
		if(Player_L3_2.state == 1)
		{	
			if(tag == "ccw")
			{
				Player_L3_2.movement(5);
			}
			
			
			if(tag == "cw")
			{
				Player_L3_2.movement(7);
			}
		}
		
		
		if (tag == "Next") 
		{
			if(Player_L3_2.state == 2)
			{
				Application.LoadLevel("Level8");
			}
			else
			{
				if(Player_L3_2.state == 0)
					Player_L3_2.startTime = (int)Time.timeSinceLevelLoad;
				Player_L3_2.state++;
			}
		}


		if (tag == "Quit") 
		{
			Application.LoadLevel("MainMenu");
		}
	}


	void OnMouseUp()
	{	
		if(tag == "ccw")
		{
			Player_L3_2.movement(6);
		}
		
		
		if(tag == "cw")
		{
			Player_L3_2.movement(8);
		}
	}
}
