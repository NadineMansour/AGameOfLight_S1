using UnityEngine;
using System.Collections;

public class next_Quiz1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

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

	// Update is called once per frame
	void Update () {
	
	}
}
