using UnityEngine;
using System.Collections;

public class Timer_1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		TextMesh textObject = GetComponent<TextMesh>();
		if (ShooterScript_1.state < 2)
		{
			textObject.text =  "00:00";
		} 
		else 
		{
			if (ShooterScript_1.gameover)
			{
				int mins = (int)(ShooterScript_1.time / 60.0f);
				int secs = (int)(ShooterScript_1.time) - (mins * 60);
				if (secs / 10 == 0)
					textObject.text = (mins + ":0" + secs);
				else
					textObject.text = (mins+":"+secs);
			}
			else
			{
				int mins = (int)((Time.timeSinceLevelLoad - ShooterScript_1.startTime) / 60.0f);
				int secs = (int)(Time.timeSinceLevelLoad  - ShooterScript_1.startTime) - (mins * 60);
				if (secs / 10 == 0)
					textObject.text = (mins + ":0" + secs);
				else
					textObject.text = (mins+":"+secs);
			}
		}
	}
}
