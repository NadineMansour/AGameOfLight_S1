using UnityEngine;
using System.Collections;

public class nextButton_MM_2 : MonoBehaviour {


	public GameObject Tip1;


	// Use this for initialization
	void Start () {
	
	}


	void OnMouseDown()
	{
		if (ShooterScript_2.state == 2) 
		{
			//Uncomment following level and remove the level 5 one when level 3 is done.
			//Application.LoadLevel("Level3");
			Application.LoadLevel("Level5");
		}
		if (ShooterScript_2.state == 0) 
		{
			Tip1.SetActive (false);
			ShooterScript_2.state = 1;
		}

	}


	// Update is called once per frame
	void Update () {
	
	}
}
