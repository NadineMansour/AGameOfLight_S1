using UnityEngine;
using System.Collections;

public class R1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnMouseDown() 
	{
		PlayerScriptL3.RRightTrue();
	}
	
	
	public void OnMouseUp()
	{
		PlayerScriptL3.RRightFalse ();
	}
}
