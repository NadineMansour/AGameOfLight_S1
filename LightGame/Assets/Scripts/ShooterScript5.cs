﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class ShooterScript5 : MonoBehaviour {


	//For movement & rotation
	public static bool left;
	public static bool right;
	public static bool RRight;					
	public static bool RLeft;


	//For score
	public static int timeInLevel;
	public static int clicks;
	public int level;
	public int score;
	public static string log;


	//Other variables for state recognition and calculations
	public static Vector3 position;
	public static bool gameOver;
	public LineRenderer lightBeam;               //Lightbeam gameobject to edit positions and end points
	private static List <Vector3> linePositions;        //array containing lightbeam points for setting and editing
	private static float angle ;                        //degree of rotation of light beam
	float NI = 1.000293f;						 
	float NR = 1.3330f;

	//gameObjects
	public GameObject numOfClicks;
	public GameObject nextLevel;


	// Use this for initialization
	void Start () {
		//Initializing variables
		nextLevel.SetActive (false);
		left = false;
		right = false;
		RRight = false;
		RLeft = false;
		clicks = 0;
		score = 0;
		log = "";
		if (Application.loadedLevelName == "Level5")
			level = 6;
		else
			level = 7;
		position = transform.position;
		//Initializing light beam
		linePositions = new List<Vector3> ();   //a list that contains the main three points od the light beam 
		Vector3 start = transform.position;     // the starting point at the center of the player 
		Vector3 mid = start;					
		mid.y = 0;
		Vector3 end = start;
		end.y = -6;                             
		linePositions.Add (start);              //adding shooter's position as start point to light beam
		linePositions.Add (mid);                //adding the mid point to light beams points list
		linePositions.Add (end);                //adding the end point to light beams points list
	}


	void endExtender()
	{
		if (linePositions[2].y > -7.0f)
		{
			if (linePositions[1].x == linePositions[2].x)
			{
				linePositions[2] = new Vector3(linePositions[1].x , -7.0f, 0);
			}
			else
			{
				Vector3 diff = linePositions[2] - linePositions[1];
				float slope = (float)(diff.y / diff.x);
				float yIntercept = linePositions[2].y - linePositions[2].x*slope;
				float extendedX = (-7.0f - yIntercept)/slope;
				linePositions[2] = new Vector3(extendedX, -7.0f, 0.0f);
			}
		}
	}


	// Update is called once per frame
	void Update () {
		position = transform.position;
		if (!gameOver) 
		{
			if (right)
			{
				moveRight();
			} 
			if (left)
			{
				moveLeft();
			}
			if (RRight)
			{
				RotateRight();
			}
			if (RLeft)
			{
				RotateLeft();
			}
			endExtender();
			detector ();
			SetLightBeam();
		} 
		//display the number of moves/clicks
		TextMesh textObject = numOfClicks.GetComponent<TextMesh>();
		textObject.text = "Moves: "+clicks;
	}


	void moveRight()
	{
		if (position.x < 6.5) 
		{
			transform.position = transform.position + (new Vector3 (0.05f, 0, 0));
			for (int i = 0; i < linePositions.Count; i++) 
			{
				linePositions [i] = linePositions [i] + new Vector3 (0.05f, 0, 0);
			}	
		}
	}


	void moveLeft()
	{
		if (position.x > -8)
		{
			transform.position = transform.position - (new Vector3 (0.05f, 0, 0));
			for (int i = 0; i < linePositions.Count; i++) 
			{
				linePositions [i] = linePositions [i] - new Vector3 (0.05f, 0, 0);
			}	
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


	void SetLightBeam()
	{
		for (int i = 0; i < linePositions.Count; i++) 
		{
			lightBeam.SetPosition(i, linePositions[i]);
		}
	}


	//Detects collision of light with other gameobjects
	void detector()
	{
		if (!gameOver) 
		{
			RaycastHit hit;
			if (Physics.Linecast (linePositions [1], linePositions [2], out hit)) 
			{
				if (hit.collider.tag == "Target") 
				{
					linePositions[2] = hit.point;
					EndGame();
					gameOver = true;
					if(left || right)
					{
						log += ", xEnd: " + position.x;
					}
					if(RRight || RLeft)
					{
						log += ", zEnd: " + angle + '\n';
					}
					timeInLevel = (int)Time.timeSinceLevelLoad;
					calculateScore(); //score now has its right value
					//timeInLevel, score, clicks, log & level ready	
					StartCoroutine(save_record());
				}
				if (hit.collider.tag == "Obstacle") 
				{
					linePositions [2] = hit.point;
				}
			}
		}
	}


	public static void movement(int x)
	{

		switch(x)
		{
		case 1: 
			if(!right)
			{
				clicks++;
				log += "-Moving right, xStart: " + position.x;
			}
			right = true;
			break;
		case 2:
			if (right)
			{
				log += ", xEnd: " + position.x + '\n';
			}
			right = false;
			break;
		case 3:
			if(!left)
			{
				clicks++;
				log+= "-Moving left, xStart: " + position.x;
			}
			left = true;
			break;
		case 4:
			if (left)
			{
				log += ", xEnd: " + position.x + '\n';
			}
			left = false;
			break;
		case 5:
			if(!RRight)
			{
				clicks++;
				log += "-Rotation ccw, zStart: " + angle;
			}
			RRight = true;
			break;
		case 6:
			if (RRight)
			{
				log += ", zEnd: " + angle + '\n';
			}
			RRight = false;
			break;
		case 7:
			if(!RLeft)
			{
				clicks++;
				log += "-Rotation cw, zStart: " + angle;
			}
			RLeft = true;
			break;
		case 8:
			if (RLeft)
			{
				log += ", zEnd: " + angle + '\n';
			}
			RLeft = false;
			break;
		}

	}


	void EndGame()
	{
		nextLevel.SetActive (true);
	}
	
	
	void calculateScore()
	{
		score = (120 - timeInLevel) * 100 + (5 - clicks) * 50;
		//to exclude negative scores
		if (score <= 50) score = 50;
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
	
	
	//Extends light beam so that it doesnt appear shortened
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
		SetLightBeam ();
	}


	IEnumerator save_record() 
	{
		string urlMessage = "https://ilearn-td.herokuapp.com/api/records/save_record";
		WWWForm form = new WWWForm ();
		// pass the email authentication
		string user_email = ButtonLogin.user_email;
		form.AddField ("email", user_email);
		form.AddField ("level", level);
		form.AddField ("score", score);
		form.AddField ("time", timeInLevel);
		form.AddField ("clicks", clicks);
		form.AddField ("logs", log);
		WWW w = new WWW(urlMessage, form);
		yield return w;
		if (!string.IsNullOrEmpty (w.error)) 
		{
			// this is done if the authentication is rejected or the response has
			// value >= 400 which means error in authentication or connection or server is down
			Debug.Log("The record is not saved");
		} 
		else 
		{
			// if the response has OK status
		}
	}
}
