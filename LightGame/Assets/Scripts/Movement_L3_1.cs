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
				Player_L3_1.state++;
			}
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
