using UnityEngine;
using System.Collections;

public class ToLevels : MonoBehaviour 
{


	public void OnMouseDown() 
	{
		int Current = Application.loadedLevel;
		Current += 1;
		Application.LoadLevel (Current);
	}
}
