using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TextScript2 : MonoBehaviour {

	
	public void SetText(string text) 
	{
		// you can try to get this component
		var myText = gameObject.GetComponent<Text>();
		// but it can be null, so you might want to add it
		if (myText == null) 
		{
			myText = gameObject.AddComponent<Text>();
		}
		myText.text = text;
	}

	
	// Update is called once per frame
	void Update ()
	{
		//Text saying "Congrats!! You win!!" appears when target is reached
		if (!ShooterScript5.gameOver) 
		{
			SetText(" ");
		}
		else
		{
			SetText ("Congrats!! You win!!");
		}
	}
}
