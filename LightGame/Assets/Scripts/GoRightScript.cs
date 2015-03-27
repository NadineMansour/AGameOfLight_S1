using UnityEngine;
using System.Collections;

public class GoRightScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	

	// Update is called once per frame
	void Update () 
	{
	
	}


	public void OnMouseDown() 
	{
		PlayerScript2.movement(1);
	}
	
	
	public void OnMouseUp()
	{
		PlayerScript2.movement(2);
	}
}
