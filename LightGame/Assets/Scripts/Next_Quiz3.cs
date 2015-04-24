using UnityEngine;
using System.Collections;

public class Next_Quiz3 : MonoBehaviour {
	void OnMouseDown()
	{
		if(Quiz3.state == 0)
		{
			Quiz3.state++;
		}
		
		
		if(Quiz3.state == 2)
		{
			Application.LoadLevel("End");
		}
	}
}
