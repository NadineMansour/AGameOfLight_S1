using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TextScript : MonoBehaviour {

	public float StartTime;

	void Start () {
		StartTime = Time.realtimeSinceStartup;
	}

	public void SetText(string text) 
	{
		// you can try to get this component
		var myText = gameObject.GetComponent<Text>();
		// but it can be null, so you might want to add it
		if (myText == null) {
			myText = gameObject.AddComponent<Text>();
		}
		myText.text = text;
	}

	
	// Update is called once per frame
	void Update ()
	{
		if (!PlayerScript.isGameOver ()) 
		{
			if (Application.loadedLevelName == "Level1")
			{
				if (Time.realtimeSinceStartup - StartTime < 5) {
					SetText ("You can use this button to move up.");
				} else {
					if (Time.realtimeSinceStartup - StartTime  < 10) {
						SetText ("You can use this button to move down.");
					} else {
						if (Time.realtimeSinceStartup - StartTime  < 15) {
							SetText ("You can use these 2 buttons to rotate the light source .");
						} else {
							SetText (" ");
						}
					}
				}
			}
			else
			{
				if (Time.realtimeSinceStartup  - StartTime  < 5) {
					SetText ("This is a blackhole that won't let light light pass through it.");
				}
				else
				{
					if (Time.realtimeSinceStartup - StartTime  < 10)
					{
						SetText ("Find a way to reach the target. Remember light travels in straight lines");
					}
				}
			}
		}
		else
		{
			if (Application.loadedLevelName == "Level1")
				SetText ("Congrats!! People of planet Redo thank you");
			else
				SetText ("Congrats!! People of planet Safarawy thank you");
		}
	}

}
