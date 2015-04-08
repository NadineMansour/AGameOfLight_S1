using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		TextMesh textObject = GetComponent<TextMesh>();

		if (Application.loadedLevelName.Equals ("Level5") || Application.loadedLevelName.Equals ("Level6")) 
		{
			if (ShooterScript5.gameOver) 
			{
				int mins = (int)(ShooterScript5.timeInLevel / 60.0f);
				int secs = (int)(ShooterScript5.timeInLevel) - (mins * 60);
				if (secs / 10 == 0)
					textObject.text = (mins + ":0" + secs);
				else
					textObject.text = (mins + ":" + secs);
			} 
			else 
			{
				int mins = (int)(Time.timeSinceLevelLoad / 60.0f);
				int secs = (int)(Time.timeSinceLevelLoad) - (mins * 60);
				if (secs / 10 == 0)
					textObject.text = (mins + ":0" + secs);
				else
					textObject.text = (mins + ":" + secs);
			}
		} 
		else 
		{
			if (Application.loadedLevelName.Equals("Level3"))
			{
				if (reflection_level_3.gameOver) 
				{
					int mins = (int)(reflection_level_3.timeInLevel / 60.0f);
					int secs = (int)(reflection_level_3.timeInLevel) - (mins * 60);
					if (secs / 10 == 0)
						textObject.text = (mins + ":0" + secs);
					else
						textObject.text = (mins + ":" + secs);
				} 
				else 
				{
					int mins = (int)(Time.timeSinceLevelLoad / 60.0f);
					int secs = (int)(Time.timeSinceLevelLoad) - (mins * 60);
					if (secs / 10 == 0)
						textObject.text = (mins + ":0" + secs);
					else
						textObject.text = (mins + ":" + secs);
				}
			}
			//Level 4
			else
			{
				if (reflection_level_4.gameOver) 
				{
					int mins = (int)(reflection_level_4.timeInLevel / 60.0f);
					int secs = (int)(reflection_level_4.timeInLevel) - (mins * 60);
					if (secs / 10 == 0)
						textObject.text = (mins + ":0" + secs);
					else
						textObject.text = (mins + ":" + secs);
				} 
				else 
				{
					int mins = (int)(Time.timeSinceLevelLoad / 60.0f);
					int secs = (int)(Time.timeSinceLevelLoad) - (mins * 60);
					if (secs / 10 == 0)
						textObject.text = (mins + ":0" + secs);
					else
						textObject.text = (mins + ":" + secs);
				}
			}
		}
	}
}
