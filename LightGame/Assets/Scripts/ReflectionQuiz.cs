using UnityEngine;
using System.Collections;

public class ReflectionQuiz : MonoBehaviour {

	// GameObjects links to control their appearence in the Scene
	public GameObject correctAnswer;
	public GameObject incorrectAnswer1;
	public GameObject incorrectAnswer2;
	public GameObject incorrectAnswer3;
	public GameObject quizTip;
	public GameObject CorrectImage;
	public GameObject IncorrectImage;
	public GameObject availableButtons;


	//For Saving the records
	private string chosenSolution;
	private bool rightAnswer;


	//Switch the Scenes
	public GameObject toLevelsButton;


	void Start()
	{
		toLevelsButton.SetActive (false);   //---------->
		availableButtons.SetActive (false); //---------->Setting all the gameObjects to dissappear in the beginning of the scene except for the Quiz Tip.
		CorrectImage.SetActive (false);     //---------->
		IncorrectImage.SetActive (false);   //---------->
		StartCoroutine (waitButtons());     //Quiz tip will be displayed for 4 seconds till the question and the buttons appear
	}


	void Update()
	{
		if (correctAnswer.tag == "Correct")
		{
			rightAnswer = true; 
			chosenSolution = "Reflect";
			StartCoroutine (save_answer());
			correctAnswer.tag="Untagged";//turn the button clicked tag to Untagged in order not to fall in an infinite loop	
			StartCoroutine (waitCorrect());//User will have 2 seconds of wait time till the correct Answer texture come up
		}
		if (incorrectAnswer1.tag == "Incorrect")
		{
			rightAnswer = false;
			chosenSolution = "Pass";
			StartCoroutine (save_answer());
			incorrectAnswer1.tag="Untagged";
			StartCoroutine (waitIncorrect());//User will have 2 seconds of wait time till the Incorrect Answer texture come up
		}
		if (incorrectAnswer2.tag == "Incorrect")
		{
			rightAnswer = false;
			chosenSolution = "Refract";
			StartCoroutine (save_answer());
			incorrectAnswer2.tag="Untagged";
			StartCoroutine (waitIncorrect());
		}
		if (incorrectAnswer3.tag == "Incorrect")
		{
			rightAnswer = false;
			chosenSolution="Defract";
			StartCoroutine (save_answer());
			incorrectAnswer3.tag="Untagged";
			StartCoroutine (waitIncorrect());
		}
	}


	IEnumerator waitButtons()
	{
		yield return new WaitForSeconds(4);//Wait 4 seconds until the next line is executed
		quizTip.SetActive (false);
		availableButtons.SetActive (true);
	}


	IEnumerator waitCorrect()
	{
		yield return new WaitForSeconds(2);//Wait 2 seconds until the next line is executed
		CorrectImage.SetActive (true);
		availableButtons.SetActive (false);
		toLevelsButton.SetActive (true);	
	}


	IEnumerator waitIncorrect()
	{
		yield return new WaitForSeconds(4);//Wait 2 seconds until the next line is executed
		IncorrectImage.SetActive (true);
		availableButtons.SetActive (false);
		toLevelsButton.SetActive (true);
	}


	IEnumerator save_answer() 
	{
		string urlMessage = "https://ilearn-td.herokuapp.com/api/records/save_answer";
		WWWForm form = new WWWForm ();
		// pass the email authentication
		string user_email = ButtonLogin.user_email;
		form.AddField ("email", user_email);
		form.AddField ("quiz", 2);
		form.AddField ("question", "When a Light hits a mirror Light will ..............");
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
