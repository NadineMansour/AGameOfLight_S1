using UnityEngine;
using System.Collections;

public class R2 : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnMouseDown() 
	{
		PlayerScriptL3.RLeftTrue();
	}
	
	
	public void OnMouseUp()
	{
		PlayerScriptL3.RLeftFalse ();
	}
}

