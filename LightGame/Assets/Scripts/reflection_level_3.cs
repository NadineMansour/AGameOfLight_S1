﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class reflection_level_3 : MonoBehaviour 
{
	public static bool rotateRight;
	public static bool rotateLeft;
	public LineRenderer lightBeam;//Lightbeam gameobject to edit positions and end points
	private List <Vector3> linePositions;//array containing lightbeam points for setting and editing
	private float angle;//degree of rotation of light beam
	public static bool gameOver;//check whether target is reached or not
	public GameObject target;
	public GameObject bam;
	public GameObject toLevelsButton; //button that redirect to levels menu after winning
	public GameObject numOfClicks;
	//Variables for records table
	public float StartTime;
	public float FinishTime;
	public static int timeInLevel;
	public static int clicks;
	public static string log;
	public int score;
	public int level;


	void Start () 
	{
		linePositions = new List<Vector3> ();
		Vector3 start = transform.position;
		Vector3 mid = start;
		Vector3 end = start;
		end.y = -3;
		linePositions.Add (start);//Adding shooter's position
		linePositions.Add (mid);//Initially setting my middle point to the shooter's position
		linePositions.Add (end);//End of the lightbeam
		SetLightBeam ();//Setting the values of the lightbeam
		toLevelsButton.SetActive(false);//Disabling the tolevels button initially
		gameOver = false;
		target.SetActive (true);
		bam.SetActive (false);
		level = 3;
		StartTime = Time.realtimeSinceStartup;
		clicks = 0;
	}


	void calculateScore()
	{
		score = (120 - timeInLevel) * 100 + (5 - clicks) * 50;
		if (score <= 50) //to exclude negative scores
			score = 50;
	}


	void EndGame()
	{
		StartCoroutine (save_record ()); // save the record when the game ends
		target.SetActive (false); //enables target halo, indicatingt that light reached it
		toLevelsButton.SetActive(true); //enables the button that's used to redirect to other scene
		bam.SetActive(true); //enables target halo, indicatingt that light reached it
	}


	// Update is called once per frame
	void Update () 
	{
		TextMesh textObject = numOfClicks.GetComponent<TextMesh>();
		textObject.text = "Moves: " + clicks;

		//To prevent shooter from moving if game is over (light reached target).
		if (!gameOver) 
		{
			//Checks if any of the buttons are pressed and calls the method responsible for moving/rotating in specified direction
			if (rotateRight) 
			{
				RotateRight ();

			}
			if (rotateLeft) 
			{
				RotateLeft ();
			}
			detector ();
			SetLightBeam ();
		} 
		else
		{
			EndGame ();
		}
	}


	public void detector()
	{
		if (!gameOver) 
		{
			//Raycast to trace the lineposition with rescpect to any collider in it's way
			RaycastHit hit;
			if (Physics.Linecast (linePositions [1], linePositions [2], out hit)) 
			{
				//Player's light beam reached the target
				if (hit.collider.tag == "Target") 
				{
					linePositions[2] = hit.point;
					gameOver = true;
					FinishTime = Time.realtimeSinceStartup;
					timeInLevel = (int)FinishTime - (int)StartTime;
					calculateScore(); //score now has its right value
					//Mariam, at this point -> Clicks, score, timeInLevel, log & level are all ready. Good luck
					//Insert Post records method here.
					StartCoroutine(save_record());
				}
				//An obstacle is in the lightbeam's way
				if (hit.collider.tag == "Obstacle")
				{
					linePositions[2] = hit.point;
				}
				//The lightbeam collided with the mirror
				if (hit.collider.tag == "Mirror")
				{	
					Vector3 collision = hit.point;
					linePositions[1] = collision;
					float DistanceY  = collision.y - linePositions[0].y;//Calculating the distance due to the reflection
					DistanceY = collision.y + (2*DistanceY);
					linePositions[2] = new Vector3 (linePositions[0].x,DistanceY,0);
				}
			}
		}
	}


	//updates light renderer parameters with those stored in linePositions array
	void SetLightBeam()
	{
		for (int i = 0; i < linePositions.Count; i++) 
		{
			lightBeam.SetPosition(i, linePositions[i]);
		}
	}


	public static void RotateRightTrue()
	{
		if (!rotateRight) 
		{
			clicks++;
			log+= "RRight-";
		}
		rotateRight = true;
	}
	
	
	public static void RotateRightFalse()
	{
		rotateRight = false;
	}
	
	
	public static void RotateLeftTrue()
	{
		if (!rotateLeft) 
		{
			clicks++;
			log+= "RLeft-";
		}
		rotateLeft = true;
	}
	
	
	public static void RotateLeftFalse()
	{
		rotateLeft = false;
	}


	//Rotates shooter cw, if limit wasn't reached
	public void RotateRight()
	{
		float zz = transform.eulerAngles.z;
		if ((zz < 60 || zz >=300)) 
		{
			transform.Rotate (new Vector3(0,0,0.5f));
			angle+= 0.5f;
			RotateLightBeam();
		}
		
	}


	//Rotates shooter ccw, if limit wasn't reached
	void RotateLeft()
	{
		float zz = transform.eulerAngles.z;
		if ((zz<=61 || zz > 301)) 
		{
			transform.Rotate (new Vector3(0,0,-0.5f));
			angle-= 0.5f;
			RotateLightBeam();
		}
	}


	void PointRotator()
	{
		Vector3 pivotPoint = linePositions [0];
		Vector3 pointToRotate = new Vector3 (0, -3, 0);
		Vector3 midpointToRotate = new Vector3 (linePositions [0].x, 0, 0);
		float Nx = (pointToRotate.x - pivotPoint.x);
		float Ny = (pointToRotate.y - pivotPoint.y);
		float mx = 0;
		float my = 0;
		float angle1 = angle * Mathf.PI / 180.0f;
		linePositions[2] = new Vector3((float)(Mathf.Cos(angle1) * Nx - Mathf.Sin(angle1) * Ny + pivotPoint.x), (float)(Mathf.Sin(angle1) * Nx + Mathf.Cos(angle1) * Ny + pivotPoint.y), 0);
		linePositions [1]  = new Vector3((float)(Mathf.Cos(angle1) * mx - Mathf.Sin(angle1) * my + pivotPoint.x), (float)(Mathf.Sin(angle1) * mx + Mathf.Cos(angle1) * my + pivotPoint.y), 0);
	}


	void PointChecker()
	{
		Vector3 endP = linePositions [2];
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
		string urlMessage = "https://k12-mariammohamed.c9.io/api/records/save_record";
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