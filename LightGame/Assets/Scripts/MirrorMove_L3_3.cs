using UnityEngine;
using System.Collections;

public class MirrorMove_L3_3 : MonoBehaviour {


	public GameObject Mirror;


	void OnMouseDown(){
		if(Player_L3_3.state == 1){
			if (tag == "MirrorRB") 
			{
				if(!Mirror_L3_3.MirrorR)
				{
					Shooter_L3_3.clicks++;
					Shooter_L3_3.log += "-Mirror R, xStart: " + Mirror.transform.position.x +", ";
				}
				Mirror_L3_3.moveMirror (0);
			}
			if (tag == "MirrorLB") 
			{
				if(!Mirror_L3_3.MirrorL)
				{
					Shooter_L3_3.clicks++;
					Shooter_L3_3.log += "-Mirror L, xStart: " + Mirror.transform.position.x +", ";
				}
				Mirror_L3_3.moveMirror (1);
			}
		}
	}


	void OnMouseUp(){
		if (Player_L3_3.state == 1) 
		{
			if (tag == "MirrorRB") 
			{
				if(Mirror_L3_3.MirrorR)
				{
					Shooter_L3_3.log += "xEnd: " + Mirror.transform.position.x + '\n';
				}
				Mirror_L3_3.moveMirror (2);
			}
			if (tag == "MirrorLB") 
			{
				if(Mirror_L3_3.MirrorL)
				{
					Shooter_L3_3.log += "xEnd: " + Mirror.transform.position.x + '\n';
				}
				Mirror_L3_3.moveMirror (3);
			}
		}
	}
}
