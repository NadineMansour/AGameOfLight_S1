using UnityEngine;
using System.Collections;

public class MirrorMove_L3_3 : MonoBehaviour {


	void OnMouseDown(){
		//if(Shooter.state == ){
			if (tag == "MirrorRB") 
			{
				Mirror_L3_3.moveMirror (0);
			}
			if (tag == "MirrorLB") 
			{
				Mirror_L3_3.moveMirror (1);
			}
		//}
	}


	void OnMouseUp(){
		if (tag == "MirrorRB") 
		{
			Mirror_L3_3.moveMirror (2);
		}
		if (tag == "MirrorLB") 
		{
			Mirror_L3_3.moveMirror (3);
		}
	}
}
