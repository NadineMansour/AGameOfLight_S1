using UnityEngine;
using System.Collections;


public class Quiz3 : MonoBehaviour {
	
	
	public static int state;
	public static bool clickable;
	public static string chosenSolution;
	public static bool rightAnswer;
	
	
	public bool saved;
	
	
	public GameObject nextButton;
	public GameObject introTip;
	public GameObject correct;
	public GameObject incorrect;
	
	
	void Start () 
	{
		state = 0;
		saved = false;
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
			if(!saved)
			{
				StartCoroutine(save_answer());
				saved = true;
			}
			nextButton.SetActive(true);
			clickable = false;
		}
	}
	
	
	IEnumerator save_answer() 
	{
		string urlMessage = "https://ilearn-td.herokuapp.com/api/records/save_answer";
		WWWForm form = new WWWForm ();
		// pass the email authentication
		string user_email = ButtonLogin.user_email;
		form.AddField ("email", user_email);
		form.AddField ("quiz", 3);
		form.AddField ("question", "The bending of a light beam when it passes obliquely from one medium to another is .....");
		form.AddField ("answer", chosenSolution);
		if (rightAnswer) {
			form.AddField ("correct", 1);
		} 
		else 
		{
			form.AddField("correct", 0);
		}
		WWW w = new WWW(urlMessage, form);
		yield return w;
		if (!string.IsNullOrEmpty (w.error)) 
		{
			// this is done if the authentication is rejected or the response has
			// value >= 400 which means error in authentication or connection or server is down
			Debug.Log("The record is not saved");
		} 
		else 
		{
			// if the response has OK status
		}
	}
}
