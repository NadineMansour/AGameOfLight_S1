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
			Application.LoadLevel ("Quiz1");
		}
		if (ShooterScript_2.state == 0) 
		{
			Tip1.SetActive (false);
			ShooterScript_1.startTime = (int)Time.timeSinceLevelLoad;
			ShooterScript_2.state = 1;
		}

	}


	// Update is called once per frame
	void Update () {
	
	}
}
