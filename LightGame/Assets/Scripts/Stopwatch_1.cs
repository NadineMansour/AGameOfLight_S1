using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Stopwatch_1 : MonoBehaviour {
	
	
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
		if (ShooterScript_1.state < 2) {
			SetText ("00:00");
		} 
		else 
		{
			if (ShooterScript_1.gameover)
			{
				int mins = (int)(ShooterScript_1.time / 60.0f);
				int secs = (int)(ShooterScript_1.time) - (mins * 60);
				if (secs / 10 == 0)
					SetText (mins + ":0" + secs);
				else
					SetText(mins+":"+secs);
			}
			else
			{
				int mins = (int)((Time.timeSinceLevelLoad - ShooterScript_1.startTime) / 60.0f);
				int secs = (int)(Time.timeSinceLevelLoad  - ShooterScript_1.startTime) - (mins * 60);
				if (secs / 10 == 0)
					SetText (mins + ":0" + secs);
				else
					SetText(mins+":"+secs);
			}
		}
	

	}
}
