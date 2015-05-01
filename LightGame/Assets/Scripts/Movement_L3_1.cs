using UnityEngine;
using System.Collections;


public class Movement_L3_1 : MonoBehaviour {


	void OnMouseDown()
	{
		if(Player_L3_1.state == 1)
		{
			if(tag == "right")
			{
				Player_L3_1.movement(1);
			}


			if(tag == "left")
			{
				Player_L3_1.movement(3);
			}


			if(tag == "ccw")
			{
				Player_L3_1.movement(5);
			}


			if(tag == "cw")
			{
				Player_L3_1.movement(7);
			}
		}


		if (tag == "Next") 
		{
			if(Player_L3_1.state == 2)
			{
				Application.LoadLevel("Level7");
			}
			else
			{
				if(Player_L3_1.state == 0)
					Player_L3_1.startTime = (int)Time.timeSinceLevelLoad;
				Player_L3_1.state++;
			}
		}


		if (tag == "Quit") 
		{
			Application.LoadLevel("MainMenu");
		}
	}

	void OnMouseUp()
	{
		if(tag == "right")
		{
			Player_L3_1.movement(2);
		}
		
		
		if(tag == "left")
		{
			Player_L3_1.movement(4);
		}
		
		
		if(tag == "ccw")
		{
			Player_L3_1.movement(6);
		}
		
		
		if(tag == "cw")
		{
			Player_L3_1.movement(8);
		}
	}
}
