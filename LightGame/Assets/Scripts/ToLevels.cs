using UnityEngine;
using System.Collections;

public class ToLevels : MonoBehaviour 
{	
	//boolean values to detect the buttons
	public bool level1;
	public bool level2;
	public bool level3;
	public bool level4;
	public bool level5;
	public bool level6;
	public bool level7;
	public bool level8;
	public void OnMouseDown() 
	{
		if (level1)
		{
			Application.LoadLevel("Level1_MM");
		}
		if (level2)
		{
			Application.LoadLevel("Level2_MM");
		}
		if (level3)
		{
			Application.LoadLevel("Level3");
		}
		if (level4)
		{
			Application.LoadLevel("Level4");
		}
		if (level5)
		{
			Application.LoadLevel("reflection level 5");
		}
		if (level6)
		{
			Application.LoadLevel("Level6");
		}
		if (level7)
		{
			Application.LoadLevel("Level7");
		}
		if (level8)
		{
			Application.LoadLevel("Level8");
		}
		if(GetComponent<Collider>().tag == "Finish")
		{
		int Current = Application.loadedLevel;
		Current += 1;
		Application.LoadLevel (Current);
		}
		if (GetComponent<Collider> ().tag == "Reflection") 
		{
			Application.LoadLevel("ReflectionWorld");
		}
		if (GetComponent<Collider> ().tag == "StraightLine") 
		{
			Application.LoadLevel("StraightWorld");
		}
		if (GetComponent<Collider> ().tag == "Refraction") 
		{
			Application.LoadLevel("RefractionWorld");
		}
		if (GetComponent<Collider> ().tag == "Records") 
		{
			Application.LoadLevel("score");
		}
		if (GetComponent<Collider> ().tag == "MainMenu") 
		{
			Application.LoadLevel("MainMenu");
		}
	}
}
