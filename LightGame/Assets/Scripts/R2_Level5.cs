using UnityEngine;
using System.Collections;


public class R2_Level5 : MonoBehaviour 
{
	public float StartTime;


	// Use this for initialization
	void Start () 
	{
		StartTime = Time.realtimeSinceStartup;
	}


	// Update is called once per frame
	void Update () 
	{
		// not needed now 
		/*if (Time.realtimeSinceStartup - StartTime > 3.0f && Time.realtimeSinceStartup - StartTime < 6.0f)
		{
			Component halo = GetComponent ("Halo"); 
			halo.GetType ().GetProperty ("enabled").SetValue (halo, true, null);
		} 
		else 
		{
			Component halo = GetComponent ("Halo"); 
			halo.GetType ().GetProperty ("enabled").SetValue (halo, false, null);
		}*/
	}


	//called when the rotate anti clockwise button is pressed 
	public void OnMouseDown() 
	{
		Player_Level5_nadine.RLeftTrue();
	}
	

	//called when the rotate anti clockwise button is released 
	public void OnMouseUp()
	{
		Player_Level5_nadine.RLeftFalse ();
	}
}

