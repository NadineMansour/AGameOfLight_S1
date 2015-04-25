using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class reflection_level_5 : MonoBehaviour {

	//booleans to rotate left and right
	public static bool rotateRight;
	public static bool rotateLeft;
	//variables for state recognition and calculations
	public LineRenderer lightBeam; 
	private List <Vector3> linePositions;
	public LineRenderer lightBeam2;
	private List <Vector3> linpositions2;
	private float angle;
	public static bool gameOver;
	public GameObject target;
	public GameObject target1;
	public GameObject bam;
	public GameObject to_next_level;
	public GameObject numOfClicks;
	//Variables for records table
	public float StartTime;
	public float FinishTime;
	public static int timeInLevel;
	public static int clicks;
	public static string log;
	public int score;
	public int level;
	
	
	// Use this for initialization
	void Start () 
	{
		//setting light points  
		linePositions = new List<Vector3> ();
		Vector3 line1Start = transform.position;
		Vector3 line1End = new Vector3(0,3,0);
		linePositions.Add (line1Start);
		linePositions.Add (line1End);
		SetLightBeam (linePositions,lightBeam);

		linpositions2 = new List<Vector3> ();
		Vector3 line2Start = line1Start;
		Vector3 line2End = line1Start;
		linpositions2.Add (line2Start);
		linpositions2.Add (line2End);
		SetLightBeam (linpositions2,lightBeam2);

		extendLightX (linePositions);
		gameOver = false;
		target.SetActive (true);
		bam.SetActive (false);
		gameOver = false;
		target.SetActive (true);
		bam.SetActive (false);
		to_next_level.SetActive (false);
		level = 4;
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
		bam.SetActive(true); //enables target halo, indicatingt that light reached it
		to_next_level.SetActive (true);
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		TextMesh textObject = numOfClicks.GetComponent<TextMesh>();
		textObject.text = "Moves: " + clicks;
		if (!gameOver) 
		{
			if (rotateRight) 
			{
				RotateRight ();
			}
			if (rotateLeft)
			{
				RotateLeft ();
			}
			SetLightBeam (linePositions,lightBeam);
			SetLightBeam(linpositions2,lightBeam2);
		} 
		else 
		{
			EndGame();
		}
	}






	void extendLightX(List<Vector3> l)
	{
		if (l [1].y < 6) 
		{
			Vector3 point1 = l [0];
			Vector3 point2 = l [1];
			float slope = (point2.y - point1.y) / (point2.x - point1.x);
			if(l[0].x < l[1].x)
			{
				l [1] = new Vector3 (8, (slope*8)-(slope*l[0].x)+l[0].y, 0);
			}
			else
			{
				l[1] = new Vector3 (-8,(-8*slope)- (slope * l[0].x) + l[0].y,0);
			}
		}
	}
	
	
	void extendLightY(List <Vector3> l)
	{
		if (l [1].y < 5) 
		{
			Vector3 point1 = l[0];
			Vector3 point2 = l[1];
			float slope = (point2.y - point1.y)/(point2.x-point1.x);
			if(l[0].y > l[1].y)
			{
				l[1] = new Vector3((-5+(slope*l[0].x)-l[0].y)/slope,-5,0);
			}
			else 
			{
				l[1] = new Vector3((5+(slope*l[0].x)-l[0].y)/slope,5,0);
			}
		}
	}
	
	
	void SetLightBeam (List <Vector3> l, LineRenderer line)
	{
		for (int i = 0; i < l.Count; i++)
		{
			line.SetPosition(i, l[i]);
		}
	}


	public static void RotateRightTrue() // the method sets the RotateRight value to true
	{
		if (!rotateRight) 
		{
			clicks++;
			log+= "RRight-";
		}
		rotateRight = true;
	}
	
	
	public static void RotateRightFalse() // the method sets the RotateRight value to false
	{
		rotateRight = false;
	}
	
	
	public static void RotateLeftTrue() // the method sets the RotateLeft value to true
	{
		if (!rotateLeft) 
		{
			clicks++;
			log+= "RLeft-";
		}
		rotateLeft = true;
	}
	
	
	public static void RotateLeftFalse()// the method sets the RotateLeft value to false
	{
		rotateLeft = false;
	}
	
	
	public void RotateRight()
	{
		float zz = transform.eulerAngles.z;
		if ((zz < 60 || zz >=300)) 
		{
			transform.Rotate (new Vector3(0,0,0.3f));
			angle+= 0.3f;
			RotateLightBeam();
		}
		
	}
	
	
	void RotateLeft()
	{
		float zz = transform.eulerAngles.z;
		if ((zz<=61 || zz > 301)) 
		{
			transform.Rotate (new Vector3(0,0,-0.3f));
			angle-= 0.3f;
			RotateLightBeam();
		}
	}
	
	
	void PointRotator() // the method rotate all point in the line in a straight line, and the reflection is done by the detector method.
	{
		Vector3 pivotPoint = linePositions [0];
		Vector3 pointToRotate = new Vector3 (0, pivotPoint.y, 0);
		float Nx = (pointToRotate.x - pivotPoint.x);
		float Ny = (pointToRotate.y - pivotPoint.y);
		float angle1 = angle * Mathf.PI / 180.0f;
		linePositions[1] = new Vector3((float)(Mathf.Cos(angle1) * Nx - Mathf.Sin(angle1) * Ny + pivotPoint.x), (float)(Mathf.Sin(angle1) * Nx + Mathf.Cos(angle1) * Ny + pivotPoint.y), 0);

		linpositions2 [0] = linePositions [0];
		linpositions2 [1] = linePositions [0];

		extendLightX (linePositions);
	}
	
	
	void RotateLightBeam() //method that apply rotation
	{
		PointRotator ();
		SetLightBeam (linePositions,lightBeam);
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
