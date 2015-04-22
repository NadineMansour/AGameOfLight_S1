using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Shooter_L3_3 : MonoBehaviour {


	//For score
	public static int timeInLevel;
	public static int clicks;
	public int level;
	public int score;
	public static string log;


	public static bool gameOver;
	public LineRenderer lightBeam;               //Lightbeam gameobject to edit positions and end points
	private static List <Vector3> linePositions;        //array containing lightbeam points for setting and editing


	// Use this for initialization
	void Start () 
	{
		float x = transform.position.x;
		float y = transform.position.y;
		linePositions = new List<Vector3> ();
		linePositions.Add (new Vector3(x, y, 0));
		linePositions.Add (new Vector3(x, 0, 0));
		linePositions.Add (new Vector3(x, -5.0f, 0));
		linePositions.Add (new Vector3(x, 0, 0));
		gameOver = false;
	}


	void Update () 
	{
		if (!gameOver) 
		{
			//lineExtender();
			detector();
			setLightBeam ();
		}
	}


	//Makes sure that line is extended before it tests collision 
	void lineExtender()
	{
		Vector3 testedPoint = linePositions [1];
		//Making sure that point 1 is exactly on the water surface
		if (testedPoint.y != 0) 
		{
			Vector3 P0 = linePositions[0];
			Vector3 P1 = linePositions[1];
			if(P0.x != P1.x) //To avoid dividing by 0
			{
				float slope = (P1.y - P0.y) / (P1.x - P0.x);
				float yIntercept = P1.y - slope*P1.x;
				float newX = (-1*yIntercept)/slope;
				linePositions[1] = new Vector3(newX, 0, 0);
			}
			else
			{
				linePositions[1] = new Vector3 (P0.x, 0, 0);
			}
		}


		testedPoint = linePositions [2];
		if (testedPoint.y != -5.0f) 
		{
			Vector3 P1 = linePositions[1];
			Vector3 P2 = linePositions[2];
			if(P2.x != P1.x) //To avoid dividing by 0
			{
				float slope = (P2.y - P1.y) / (P2.x - P1.x);
				float yIntercept = P2.y - slope*P2.x;
				float newX = (-5.0f - yIntercept)/slope;
				linePositions[2] = new Vector3(newX, -5.0f, 0);
			}
			else
			{
				linePositions[1] = new Vector3 (P1.x, -5.0f, 0);
			}
		}


		linePositions [3] = linePositions [1];
	}


	void detector()
	{
		RaycastHit hit;
		if (Physics.Linecast (linePositions [1], linePositions [2], out hit)) 
		{
			if (hit.collider.tag == "Obstacle") 
			{
				linePositions [2] = hit.point;
				//linePositions[3] = linePositions[1];
			}
			if(hit.collider.tag == "Mirror")
			{
				//Have fun Doudy
			}
		}


		RaycastHit hit1;
		if (Physics.Linecast (linePositions [2], linePositions [3], out hit1)) 
		{
			if (hit1.collider.tag == "Target") 
			{
				//gameOver = true;
				//Save record coroutine
			}
		}
	}


	void setLightBeam()
	{
		for(int i = 0; i < linePositions.Count; i++)
		{
			lightBeam.SetPosition(i, linePositions[i]);
		}
	}
}
