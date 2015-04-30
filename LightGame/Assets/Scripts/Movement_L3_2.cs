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
				Player_L3_2.state++;
			}
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
