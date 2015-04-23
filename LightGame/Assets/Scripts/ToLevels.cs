using UnityEngine;
using System.Collections;

public class ToLevels : MonoBehaviour 
{
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
			Application.LoadLevel("level1");
		}
		if (level2)
		{
			Application.LoadLevel("level2");
		}
		if (level3)
		{
			Application.LoadLevel("level3");
		}
		if (level4)
		{
			Application.LoadLevel("level4");
		}
		if (level5)
		{
			Application.LoadLevel("level5");
		}
		if (level6)
		{
			Application.LoadLevel("level6");
		}
		if (level7)
		{
			Application.LoadLevel("level7");
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
