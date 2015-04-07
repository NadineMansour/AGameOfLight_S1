using UnityEngine;
using System.Collections;


public class nextButton_MM : MonoBehaviour {


	public GameObject Tip1;
	public GameObject Tip2;


	// Use this for initialization
	void Start () {
	
	}


	void OnMouseDown()
	{
		if (ShooterScript_1.state == 3) 
		{
			Application.LoadLevel("Level2_MM");
		}
		if (ShooterScript_1.state == 1) 
		{
			Tip2.SetActive(false);
			ShooterScript_1.startTime = (int)Time.timeSinceLevelLoad;
			ShooterScript_1.state = 2;
		}
		if (ShooterScript_1.state == 0) 
		{
			Tip1.SetActive (false);
			ShooterScript_1.state = 1;
		}
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
