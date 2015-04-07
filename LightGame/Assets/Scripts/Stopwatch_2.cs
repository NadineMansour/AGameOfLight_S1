using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Stopwatch_2 : MonoBehaviour {
	
	
	public void SetText(string text) 
	{
		// you can try to get this component
		var myText = gameObject.GetComponent<Text> ();
		// but it can be null, so you might want to add it
		if (myText == null) 
		{
			myText = gameObject.AddComponent<Text> ();
		}
		myText.text = text;
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		if (ShooterScript_2.state < 1) {
			SetText ("00:00");
		} 
		else 
		{
			if (ShooterScript_2.gameover)
			{
				int mins = (int)(ShooterScript_2.time / 60.0f);
				int secs = (int)(ShooterScript_2.time) - (mins * 60);
				if (secs / 10 == 0)
					SetText (mins + ":0" + secs);
				else
					SetText(mins+":"+secs);
			}
			else
			{
				int mins = (int)((Time.timeSinceLevelLoad - ShooterScript_2.startTime) / 60.0f);
				int secs = (int)(Time.timeSinceLevelLoad  - ShooterScript_2.startTime) - (mins * 60);
				if (secs / 10 == 0)
					SetText (mins + ":0" + secs);
				else
					SetText(mins+":"+secs);
			}
		}
		
		
	}
}
