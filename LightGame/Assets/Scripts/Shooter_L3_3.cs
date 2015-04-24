using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Shooter_L3_3 : MonoBehaviour {


	//For The Rotation 
	public static bool RRight;					
	public static bool RLeft;


	//Used in the Refraction
	private static float angle ;
	float NI = 1.000293f;						 
	float NR = 1.3330f;
	

	//For score
	public static int timeInLevel;
	public static int clicks;
	public int level;
	public int score;
	public static string log;

	
	public static bool gameOver;
	public static int state;


	public GameObject nextButton;
	public GameObject Tip1;
	public GameObject Tip2;
	public GameObject Tip3;


	public LineRenderer lightBeam;                      //the main lightbeam used in the refraction 
	public LineRenderer lightBeam2;                     //simulates the reflection of the 1st lightbeam
	private static List <Vector3> linePositions;        //array containing lightbeam  points for setting and editing
	private static List <Vector3> linePositions2; 		//array containing lightbeam2 points for setting and editing
	

	// Use this for initialization
	void Start () 
	{
		float x = transform.position.x;
		float y = transform.position.y;
		linePositions  = new List<Vector3> ();
		linePositions2 = new List<Vector3> (); 
		linePositions.Add (new Vector3(x, y, 0));
		linePositions.Add (new Vector3(x, 0, 0));
		linePositions.Add (new Vector3(x, -5.0f, 0));
		linePositions2.Add (new Vector3 (0, 0, 0));
		linePositions2.Add (new Vector3 (0, 0, 0));
		gameOver = false;
		RRight = false;
		RLeft  = false;
		angle = 0.0f;
		level = 8; 
		state = 0;
		nextButton.SetActive (true);
		Tip1.SetActive (true);
		Tip2.SetActive (false);
		Tip3.SetActive (false);
	}


	void Update () 
	{
		if (state == 1) 
		{
			Tip1.SetActive(false);
			Tip2.SetActive(true);
		}
		if (state == 2) 
		{
			Tip1.SetActive(false);
			Tip2.SetActive(false);
			nextButton.SetActive(false);
			linePositions2 [0] = new Vector3 (0, 0, 0);
			linePositions2 [1] = new Vector3 (0, 0, 0);
			if (!gameOver) 
			{
				if (RRight) 
				{
					RotateRight ();
				}
				if (RLeft) 
				{
					RotateLeft ();
				}
				detector ();
				setLightBeam ();
			}
		}
		if (state == 3) 
		{
			nextButton.SetActive(true);
			Tip3.SetActive(true);
		}
	}


	void RotateRight()
	{
		float zCoordinate = transform.eulerAngles.z;
		if ((zCoordinate < 60 || zCoordinate >=300)) 
		{
			transform.Rotate (new Vector3(0,0,0.5f));
			angle+= 0.5f;
			RotateLightBeam();
		}
	}
	
	
	void RotateLeft()
	{
		float zCoordinate = transform.eulerAngles.z;
		if ((zCoordinate<=61 || zCoordinate > 301)) 
		{
			transform.Rotate (new Vector3(0,0,-0.5f));
			angle-= 0.5f;
			RotateLightBeam();
		}
	}


	void PointRotator()
	{
		//Rotates light beam with specified degree indicate dy variable degree around its starting point (Center of shooter
		Vector3 pivotPoint = linePositions [0];
		Vector3 pointToRotate = new Vector3 (linePositions [0].x, 0, 0);
		float Nx = (pointToRotate.x - pivotPoint.x);
		float Ny = (pointToRotate.y - pivotPoint.y);
		float angle1 = angle * Mathf.PI / 180.0f;
		linePositions[1] = new Vector3((float)(Mathf.Cos(angle1) * Nx - Mathf.Sin(angle1) * Ny + pivotPoint.x), (float)(Mathf.Sin(angle1) * Nx + Mathf.Cos(angle1) * Ny + pivotPoint.y), 0);
		PointChecker ();
		//rotating the 2nd half of the lightbeam around the mid point 
		float AI = angle1;                                                //incidense angle
		float AR = ((float)Math.Asin(Math.Sin (AI) * NI / NR));           //angle of refraction
		Vector3 pivotPoint2 = linePositions [1];
		Vector3 pointToRotate2 = new Vector3 (linePositions[1].x, linePositions [1].y-6, linePositions [1].z);
		float Nx2 = (pointToRotate2.x - pivotPoint2.x);
		float Ny2 = (pointToRotate2.y - pivotPoint2.y);
		linePositions[2] = new Vector3((float)(Mathf.Cos(AR) * Nx2 - Mathf.Sin(AR) * Ny2 + pivotPoint2.x), (float)(Mathf.Sin(AR) * Nx2 + Mathf.Cos(AR) * Ny2 + pivotPoint2.y), 0);
	}
	
	
	//Extends the 1st part of the light beam to make sure it always ends at the water surface 
	void PointChecker()
	{
		Vector3 endP = linePositions [1];
		if (endP.y >0) 
		{
			Vector3 startP = linePositions[0];
			float slope = (endP.y - startP.y)/(endP.x - startP.x);
			float newX =  startP.x-(startP.y / slope);
			linePositions[1] = new Vector3(newX, 0.0f, 0.0f);
		}
	}
	
	
	//Called when player clicks on rotation buttons to rotate light beam with shooter
	void RotateLightBeam()
	{
		PointRotator ();
		setLightBeam ();
	}


	//Makes sure that line is extended before it tests collision 
	void lineExtender()
	{
		Vector3 testedPoint = linePositions [1];
		testedPoint = linePositions [2];
		if (testedPoint.y != -5.0f) 
		{
			Vector3 P1 = linePositions[1];
			Vector3 P2 = linePositions[2];
			if(P2.x != P1.x) //To avoid dividing by 0
			{
				float slope = (P2.y - P1.y) / (P2.x - P1.x);
				float yIntercept = P1.y - slope*P1.x;
				float newX = (-5.0f - yIntercept)/slope;
				linePositions[2] = new Vector3(newX, -5.0f, 0);
			}
			else
			{
				linePositions[1] = new Vector3 (P1.x, -5.0f, 0);
			}
		}
	}


	//extends the light after being reflected 
	void lineExtender2(){
		Vector3 testedPoint2 = linePositions2 [1];
		if (testedPoint2.y != -5.0f) 
		{
			Vector3 P1 = linePositions2[0];
			Vector3 P2 = linePositions2[1];
			if(P2.x != P1.x) //To avoid dividing by 0
			{
				float slope = (P2.y - P1.y) / (P2.x - P1.x);
				float yIntercept = P1.y - slope*P1.x;
				float newX = (-5.0f - yIntercept)/slope;
				linePositions2[1] = new Vector3(newX, -5.0f, 0);
			}
		}
	}


	void detector()
	{
		//detects collision between the light beam and the mirror 
		RaycastHit hit;
		if (Physics.Linecast (linePositions [1], linePositions [2], out hit)) {
			if (hit.collider.tag == "Obstacle") 
			{
				linePositions [2] = hit.point;
			}


			if (hit.collider.tag == "Mirror") {
				Vector3 temp = hit.point;
				Vector3 p0 = linePositions[1];
				float slope = (p0.y - temp.y)/(p0.x - temp.x);
				float yIntercept = p0.y - p0.x*slope;
				float newX = hit.point.x - 0.1f;
				float newY = newX*slope + yIntercept;
				linePositions[2] = new Vector3(newX, newY, 0);
				linePositions2[0] = hit.point;
				Vector3 P0 = linePositions2[0];
				Vector3 P1 = new Vector3();
				P1.x = linePositions[1].x;
				P1.y = 2*(P0.y);
				P1.z = 0;
				linePositions2[1] = P1;
				lineExtender2();
			}
		}


		RaycastHit hit1;
		if (Physics.Linecast (linePositions2 [0], linePositions2 [1], out hit1)) 
		{
			if (hit1.collider.tag == "Target") 
			{
				print("Hit target");
				gameOver = true;
				state++;
				//conditions to adjust log
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


		for (int i = 0; i < linePositions2.Count; i++) 
		{
			lightBeam2.SetPosition(i, linePositions2[i]);
		}
	}
}
