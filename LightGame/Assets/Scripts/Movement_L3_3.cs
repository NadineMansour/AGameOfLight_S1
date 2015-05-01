using UnityEngine;
using System.Collections;


public class Movement_L3_3 : MonoBehaviour {


	public GameObject Mirror;


	void OnMouseDown()
	{
		if(Player_L3_3.state == 1)
		{
			if(tag == "MirrorRB")
			{
				if (!Mirror_8.MirrorR)
				{
					Player_L3_3.clicks++;
					Player_L3_3.log += "-Mirror R, xStart: " + Mirror.transform.position.x +", ";
				}
				Mirror_8.moveMirror(0);
			}
			
			
			if(tag == "MirrorLB")
			{
				if (!Mirror_8.MirrorL)
				{
					Player_L3_3.clicks++;
					Player_L3_3.log += "-Mirror B, xStart: " + Mirror.transform.position.x +", ";
				}
				Mirror_8.moveMirror(1);
			}
			
			
			if(tag == "ccw")
			{
				Player_L3_3.movement(5);
			}
			
			
			if(tag == "cw")
			{
				Player_L3_3.movement(7);
			}
		}
		
		
		if (tag == "Next") 
		{
			if(Player_L3_3.state == 2)
			{
				Application.LoadLevel("Quiz3");
			}
			else
			{
				Player_L3_3.state++;
			}
		}
	}
	
	void OnMouseUp()
	{
		if (Player_L3_3.state == 1) 
		{
			if (tag == "MirrorRB") 
			{
				if (Mirror_8.MirrorR) 
				{
					Player_L3_3.log += "xEnd: " + Mirror.transform.position.x + '\n';
				}
				Mirror_8.moveMirror (2);
			}
		
			
			if (tag == "MirrorLB") 
			{
				if (Mirror_8.MirrorL) 
				{
					Player_L3_3.log += "xEnd: " + Mirror.transform.position.x + '\n';
				}
				Mirror_8.moveMirror (3);
			}
			
			
			if (tag == "ccw") 
			{
				Player_L3_3.movement (6);
			}
			
			
			if (tag == "cw") 
			{
				Player_L3_3.movement (8);
			}
		}
	}
}
