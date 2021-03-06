﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript2 : MonoBehaviour {


	public static bool left;
	public static bool right;
	public float StartTime;
	public float FinishTime;
	public int timeInLevel;
	private static bool gameOver;
	public static int clicks;
	public int level;
	public int score;
	public static string log;


	// Use this for initialization
	void Start () 
	{
		//Initializing default values
		left = false;
		right = false;
		StartTime = Time.realtimeSinceStartup;
		clicks = 0;
		score = 0;
		log = "";
		if (Application.loadedLevelName == "Level5")
			level = 5;
		else
			level = 6;	
	}
	

	// Update is called once per frame
	void Update () 
	{
		if (!gameOver) 
		{
			if (right) moveRight ();
			else 
			{
				if (left) moveLeft ();
			}
			detector ();
		} 
		else 
		{
			EndGame ();
		}
	}


	void EndGame()
	{
		StartCoroutine (save_record ()); // save the record when the game ends
	}


	void calculateScore()
	{
		score = (120 - timeInLevel) * 100 + (5 - clicks) * 50;
		//to exclude negative scores
		if (score <= 50) score = 50;
	}


	public static bool isGameOver()
	{
		return gameOver;
	}


	void detector()
	{
		if (!gameOver) 
		{
			RaycastHit hit;
			List<Vector3> linePositions = Player_Level5_nadine.getLinePositions();
			if (Physics.Linecast (linePositions [1], linePositions [2], out hit)) 
			{
				if (hit.collider.tag == "Target") 
				{
					gameOver = true;
					FinishTime = Time.realtimeSinceStartup;
					timeInLevel = (int)FinishTime - (int)StartTime;
					calculateScore(); //score now has its right value
					//timeInLevel, score, clicks, log & level ready	
				}
				if (hit.collider.tag == "Obstacle") 
				{
					//linePositions [2] = hit.point;
				}
			} 
		}
	}


	public void moveRight()
	{
		if (transform.position.x < 5 && !gameOver) 
		{

			transform.position = transform.position + (new Vector3 (0.05f, 0 ,0));
			//Change line positions here
			//Tip: Just add "new Vector3 (0.05f, 0 ,0)" to every point
			List<Vector3> linePositions = Player_Level5_nadine.getLinePositions();
			for(int i = 0; i < linePositions.Count; i++)
			{
				linePositions[i] = linePositions[i] + new Vector3 (0.05f, 0 ,0);
			}
			Player_Level5_nadine.setLinePositions(linePositions);
		}
	}


	public void moveLeft()
	{
		if (transform.position.x > -5 && !gameOver) 
		{
			transform.position = transform.position - (new Vector3 (0.05f, 0 ,0));
			//Change line positions here
			//Tip: Just subtract "new Vector3 (0.05f, 0 ,0)" to every point
			List<Vector3> linePositions = Player_Level5_nadine.getLinePositions();
			for(int i = 0; i < linePositions.Count; i++)
			{
				linePositions[i] = linePositions[i] - new Vector3 (0.05f, 0 ,0);
			}
			Player_Level5_nadine.setLinePositions(linePositions);
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
				log += "Right-";
			}
			right = true;
			break;
		case 2:
			right = false;
			break;
		case 3:
			if(!left)
			{
				clicks++;
				log+= "Left-";
			}
			left = true;
			break;
		case 4:
			left = false;
			break;
		}
	}


	IEnumerator save_record() 
	{
		string urlMessage = "https://k12-mariammohamed.c9.io/api/records/save_record";
		WWWForm form = new WWWForm ();
		// pass the email for authentication
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
