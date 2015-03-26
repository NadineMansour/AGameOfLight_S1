using UnityEngine;
using System.Collections;

public class GoLeftScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMouseDown() 
	{
		PlayerScript2.movement(3);
	}
	
	
	public void OnMouseUp()
	{
		PlayerScript2.movement(4);
	}
}
