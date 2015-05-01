using UnityEngine;
using System.Collections;

public class Timer_2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		TextMesh textObject = GetComponent<TextMesh>();
		if (ShooterScript_2.state < 1) 
		{
			textObject.text = ("00:00");
		} 
		else 
		{
			if (ShooterScript_2.gameover)
			{
				//Show total time spent in level before winning
				int mins = (int)(ShooterScript_2.time / 60.0f);
				int secs = (int)(ShooterScript_2.time) - (mins * 60);
				if (secs / 10 == 0)
					textObject.text = (mins + ":0" + secs);
				else
					textObject.text = (mins+":"+secs);
			}
			else
			{
				//Show real time since finishing the tips
				int mins = (int)((Time.timeSinceLevelLoad - ShooterScript_2.startTime) / 60.0f);
				int secs = (int)(Time.timeSinceLevelLoad  - ShooterScript_2.startTime) - (mins * 60);
				if (secs / 10 == 0)
					textObject.text = (mins + ":0" + secs);
				else
					textObject.text = (mins+":"+secs);
			}
		}
	}
}
