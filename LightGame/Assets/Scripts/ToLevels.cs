using UnityEngine;
using System.Collections;

public class ToLevels : MonoBehaviour {
	
	void Start () {
	}



	public void OnMouseDown() {
		if (Application.loadedLevelName == "Level1") {
			Application.LoadLevel("Level2");
		}
		if (Application.loadedLevelName == "Level2") {
			Application.LoadLevel("END");
		}
	}
	
	

	// Update is called once per frame
	void Update () {
		
	}
}
