using UnityEngine;
using System.Collections;

public class next_Quiz1 : MonoBehaviour {
	void OnMouseDown()
	{
		if(Quiz1.state == 0)
		{
			Quiz1.state++;
		}


		if(Quiz1.state == 2)
		{
			Application.LoadLevel("Level3");
		}
	}
}
