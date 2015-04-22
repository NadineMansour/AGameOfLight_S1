using UnityEngine;
using System.Collections;


public class Quiz1 : MonoBehaviour {


	public static int state;
	public static bool clickable;
	public static string chosenSolution;
	public static bool rightAnswer;


	public GameObject nextButton;
	public GameObject introTip;
	public GameObject correct;
	public GameObject incorrect;
	

	void Start () 
	{
		state = 0;
		clickable = false;
		nextButton.SetActive (true);
		introTip.SetActive (true);
		correct.SetActive (false);
		incorrect.SetActive (false);
	}


	void Update () 
	{
		if (state == 1) 
		{
			clickable = true;
			nextButton.SetActive(false);
			introTip.SetActive(false);
		}


		if (state == 2) 
		{
			nextButton.SetActive(true);
			clickable = false;
		}
	}
}
