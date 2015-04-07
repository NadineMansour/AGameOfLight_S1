using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Stopwatch : MonoBehaviour {


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


	// Use this for initialization
	void Start () 
	{
	
	}


	// Update is called once per frame
	void Update () 
	{
		if (ShooterScript5.gameOver) 
		{
			int mins = (int)(ShooterScript5.timeInLevel / 60.0f);
			int secs = (int)(ShooterScript5.timeInLevel) - (mins * 60);
			if (secs / 10 == 0)
				SetText (mins + ":0" + secs);
			else
				SetText(mins+":"+secs);
		} 
		else 
		{
			int mins = (int)(Time.timeSinceLevelLoad / 60.0f);
			int secs = (int)(Time.timeSinceLevelLoad) - (mins * 60);
			if (secs / 10 == 0)
				SetText (mins + ":0" + secs);
			else
				SetText(mins+":"+secs);
		}

	}
}
