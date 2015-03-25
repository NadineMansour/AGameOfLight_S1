using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class PlayerScript : MonoBehaviour {


	public GameObject rail;
	//up, down, RUP and RDown are indicators to whether the buttons that are represented by them, are pressed.
	public static bool up;
	public static bool down;
	public static bool RUp;
	public static bool RDown;
	public LineRenderer lightBeam; //Lightbeam gameobject to edit positions and end points
	private List <Vector3> linePositions; //array containing lightbeam points for setting and editing
	private float angle; //degree of rotation of light beam
	private static bool gameOver; //check whether target is reached or not
	public GameObject targetHalo; //Halo around target that is enabled when target is reached
	public GameObject toLevelsButton; //button that redirect to levels menu after winning
	//Variables for records table
	public float StartTime;
	public float FinishTime;
	public int timeInLevel;
	public static int clicks;
	public int score;
	public int level;


	void Start () {
		linePositions = new List<Vector3> ();
		Vector3 start = transform.position;
		Vector3 end = start;
		end.x = 9; //Changing the x-value of end point to 9 (limit of the scene)
		linePositions.Add (start); //adding shooter's position as start point to light beam
		linePositions.Add (end); //adding the end point to light beams points array
		gameOver = false; //initialising gameOver variable to false
		targetHalo.SetActive(false); 
		toLevelsButton.SetActive(false);
		SetLightBeam (); //changing the values of the light beam parameters themselves
		StartTime = Time.realtimeSinceStartup;
		clicks = 0;
		if (Application.loadedLevelName == "Level1")
			level = 1;
		else
			level = 2;	
	}


	void calculateScore()
	{
		score = (120 - timeInLevel) * 100 + (5 - clicks) * 50;
		if (score <= 50) //to exclude negative scores
			score = 50;
	}


	void EndGame()
	{
		targetHalo.SetActive (true); //enables target halo, indicatingt that light reached it
		toLevelsButton.SetActive(true); //enables the button that's used to redirect to other scene (Levels/Level 2)
	}


	void Update () {
		//To prevent shooter from moving if game is over (light reached target).
		if (!gameOver) { 
			//Checks if any of the buttons are pressed and calls the method responsible for moving/rotating in specified direction
			if (up) {
				MoveUp ();
			}
			if (down) {
				MoveDown ();
			}
			if (RUp) {
				RotateUp ();
			}
			if (RDown) {
				RotateDown ();
			}	
		}

		detector (); //detects collision with target
		if (gameOver) EndGame ();
	}


	//updates light renderer parameters with those stored in linePositions array
	void SetLightBeam()
	{
		for (int i = 0; i < linePositions.Count; i++) {
			lightBeam.SetPosition(i, linePositions[i]);
		}
	}

	
	public static bool isGameOver()
	{
		return gameOver;
	}


	public static void UpTrue()
	{
		if (!up)
			clicks++;
		up = true;
	}


	public static void UpFalse()
	{
		up = false;
	}


	public static void DownTrue()
	{
		if(!down)
			clicks++;
		down = true;
	}


	public static void DownFalse()
	{
		down = false;
	}


	public static void RUpTrue()
	{
		if (!RUp)
			clicks++;
		RUp = true;
	}


	public static void RUpFalse()
	{
		RUp = false;
	}


	public static void RDownTrue()
	{
		if (!RDown)
			clicks++;
		RDown = true;
	}


	public static void RDownFalse()
	{
		RDown = false;
	}


	//Moves shooter up, if shooter didnt reach upper rail's boundaries
	public  void MoveUp()
	{
		if (transform.position.y+ GetComponent<Renderer>().bounds.size.y/2.0f< rail.transform.position.y+rail.GetComponent<Renderer>().bounds.size.y/2.0f && !gameOver) 
		{
			transform.position = transform.position+new Vector3(0, 0.05f, 0);
			for (int i = 0; i < linePositions.Count; i++) {
				linePositions[i] += new Vector3(0, 0.05f, 0);
			}
			SetLightBeam();
		}
	}


	//Moves shooter down, if shooter didnt reach lower rail's boundaries
	public void MoveDown()
	{
		if (transform.position.y- GetComponent<Renderer>().bounds.size.y/2.0f> rail.transform.position.y+rail.GetComponent<Renderer>().bounds.size.y/-2.0f && !gameOver) 
		{
			transform.position = transform.position-new Vector3(0, 0.05f, 0);
			for (int i = 0; i < linePositions.Count; i++) {
				linePositions[i] -= new Vector3(0, 0.05f, 0);
			}
			SetLightBeam();
		}
	}


	//Rotates shooter cw, if limit wasn't reached
	public void RotateUp()
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
	public void RotateDown()
	{
		float zz = transform.eulerAngles.z;
		if ((zz<=61 || zz > 301)) 
		{
			transform.Rotate (new Vector3 (0, 0, -0.5f));
			angle-= 0.5f;
			RotateLightBeam();
		}
	}


	//Rotates light beam with specified degree indicate dy variable degree around its starting point (Center of shooter)
	void PointRotator()
	{
		Vector3 pivotPoint = linePositions [0];
		Vector3 pointToRotate = new Vector3 (9, pivotPoint.y, 0);
		float Nx = (pointToRotate.x - pivotPoint.x);
		float Ny = (pointToRotate.y - pivotPoint.y);
		float angle1 = angle * Mathf.PI / 180.0f;
		linePositions[1] = new Vector3((float)(Mathf.Cos(angle1) * Nx - Mathf.Sin(angle1) * Ny + pivotPoint.x), (float)(Mathf.Sin(angle1) * Nx + Mathf.Cos(angle1) * Ny + pivotPoint.y), 0);
	}


	//Extends light beam to end of scene in x-axis direction so that it doesnt appear shortened 
	void PointChecker()
	{
		Vector3 endP = linePositions [1];
		if (endP.x < 9) {
			Vector3 startP = linePositions[0];
			float slope = (endP.y - startP.y)/(endP.x - startP.x);
			float yIntercept = startP.y - startP.x*slope;
			float newY = slope*9.0f + yIntercept;
			linePositions[1] = new Vector3(9.0f, newY, 0.0f);
		}
	}


	//Called when player clicks on rotation buttons to rotate light beam with shooter
	void RotateLightBeam()
	{
		PointRotator ();
		PointChecker ();
		SetLightBeam ();
	}


	//Detects collision with
	void detector()
	{
		if (!gameOver) {
			RaycastHit hit;
			if (Physics.Linecast (linePositions [0], linePositions [1], out hit)) {
				if (hit.collider.tag == "Target") {
					linePositions [1] = hit.point;
					SetLightBeam ();
					gameOver = true;
					FinishTime = Time.realtimeSinceStartup;
					timeInLevel = (int)FinishTime - (int)StartTime;
					calculateScore(); //score now has its right value
					//Mariam, at this point -> Clicks, score, timeInLevel & level are all ready. Good luck
					//Insert Post records method here.
					StartCoroutine(save_record());

				}
				if (hit.collider.tag == "Obstacle") {
					linePositions [1] = hit.point;
					SetLightBeam ();
				}
			} 
			else 
			{
				PointChecker ();
			}
		
		}
	}

	IEnumerator save_record() {
		string urlMessage = "https://k12-mariammohamed.c9.io/api/records/save_record";
		WWWForm form = new WWWForm ();
		// pass the email and passsword for authentication
		string user_email = ButtonLogin.user_email;
		form.AddField ("email", user_email);
		form.AddField ("level", level);
		form.AddField ("score", score);
		form.AddField ("time", timeInLevel);
		form.AddField ("clicks", clicks);
		WWW w = new WWW(urlMessage, form);
		yield return w;
		if (!string.IsNullOrEmpty (w.error)) {
			// this is done if the authentication is rejected or the response has
			// value >= 400 which means error in authentication or connection or server is down
			Debug.Log("The record is not saved");
		} else {
			// if the response has OK status
		}
		
	}
	
}
