using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class reflection_level_4 : MonoBehaviour {


	//booleans to rotate left and right
	public static bool rotateRight;
	public static bool rotateLeft;
	//variables for state recognition and calculations
	public LineRenderer lightBeam; 
	private List <Vector3> linePositions;
	private float angle;
	private static bool gameOver;
	public GameObject target;
	public GameObject bam;
	public GameObject to_next_level;
	//Variables for records table
	public float StartTime;
	public float FinishTime;
	public int timeInLevel;
	public static int clicks;
	public static string log;
	public int score;
	public int level;

	
	// Use this for initialization
	void Start () {
		//setting light points  
		linePositions = new List<Vector3> ();
		Vector3 start = transform.position;
		Vector3 mid1 = start;
		Vector3 mid2 = start;
		Vector3 mid3 = start;
		Vector3 end = new Vector3(0,0,0);
		linePositions.Add (start);
		linePositions.Add (mid1);
		linePositions.Add (mid2);
		linePositions.Add (mid3);
		linePositions.Add (end);
		SetLightBeam ();
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
	void Update () {
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
			detector ();
			SetLightBeam ();
		} 
		else 
		{
			EndGame();
		}
	}


	public void detector()
	{
		if (!gameOver) 
		{
			RaycastHit hit;
			if (Physics.Linecast (linePositions [3], linePositions [4], out hit)) 
			{
				// check if the light hitted the target
				if (hit.collider.tag == "Target") 
				{
					gameOver = true;
					linePositions [4] = hit.point;
					FinishTime = Time.realtimeSinceStartup;
					timeInLevel = (int)FinishTime - (int)StartTime;
					calculateScore (); //score now has its right value
					//Mariam, at this point -> Clicks, score, timeInLevel, log & level are all ready. Good luck
					//Insert Post records method here.
					StartCoroutine (save_record ());
				}
				//check if the light hitted an obstacle
				if (hit.collider.tag == "Obstacle") 
				{
					linePositions [4] = hit.point;
				}
				// check if the light hitted a horizontal mirror the reflection is done on the x-axis
				if (hit.collider.tag == "horizontal mirror")
				{
					Vector3 collision = hit.point;
					float x = 0;
					// this condition is for checking if there is no collisons at all. the three mid point are still at the start point of the line
					//if no collison occurs point 4 is = to the point of collison
					if (linePositions [3] == linePositions [0]) 
					{
						linePositions [3] = collision;
						x = 2*(collision.x - linePositions [0].x)+linePositions[0].x;
						linePositions [4] = new Vector3 (x, linePositions[0].y, 0);

					}
					else
						//this condition is checking if there is only one collision. two midpoints are at the start point
						//if there is one collision only and another mirror is added point 3 is = to point 4 and point 4 is = to point of collision
						if (linePositions [2] == linePositions [0]) 
						{
							linePositions [2] = linePositions [3];
							linePositions [3] = collision;
							x = 2*(collision.x - linePositions [2].x)+linePositions[2].x;
							linePositions [4] = new Vector3 (x, linePositions[2].y, 0);
						} else

							//this condition if there is two collision points. one point is at the start point 
							//if there is two collision points and a third mirror is add point 2 is = to point 3 and point 3 is = to point 4 and point 4 is = to collision point
							if (linePositions [1] == linePositions [0]) {
							linePositions [1] = linePositions [2];
							linePositions [2] = linePositions [3];
							linePositions [3] = collision;
							x = 2*(collision.x - linePositions [2].x) + linePositions[2].x;
							linePositions [4] = new Vector3 (x, linePositions[2].y, 0);
						}
					extendLightX();
				}


				//check if the light hitted a vertical mirror the reflection is done on y-axis
				//this condition have the same idea of the horizontal mirror
				if (hit.collider.tag == "vertical mirror") 
				{
					Vector3 collision = hit.point;
					float y = 0;
					if (linePositions [3] == linePositions [0]) 
					{
						linePositions [3] = collision;
						y = 2*(linePositions[0].y - collision.y) + linePositions[0].y;
						linePositions [4] = new Vector3 (linePositions[0].x, -y, 0);
					}
					else 

						if (linePositions [2] == linePositions [0]) 
					{
						linePositions [2] = linePositions [3];
						linePositions [3] = collision;
						y = 2*(linePositions[2].y - collision.y)+linePositions[2].y;
						linePositions [4] = new Vector3 (linePositions[2].x, -y, 0);
					}
					else 

						if (linePositions [1] == linePositions [0]) 
					{
						linePositions [1] = linePositions [2];
						linePositions [2] = linePositions [3];
						linePositions [3] = collision;
						y = 2*(linePositions[2].y - collision.y) + linePositions[2].y;
						linePositions [4] = new Vector3 (linePositions[2].x, -y, 0);
					}
					extendLightY();
				}
			}

			//to be continued***************************** 
			/*else 
				if (Physics.Linecast(linePositions[2],linePositions[3],out hit))
			{
				if(hit.collider.tag == "horizontal mirror")
				{
					Vector3 collision = hit.point;
					float x = 0;

					if(linePositions[2] == linePositions[0])
					{
						linePositions[3] = collision;
						x = 2*(collision.x - linePositions [0].x)+linePositions[0].x;
						linePositions [4] = new Vector3 (x, linePositions[0].y, 0);
					} 
					else if (linePositions[1] ==  linePositions[0])
					{
						linePositions[3] = collision;
						x = 2*(collision.x - linePositions [0].x)+linePositions[0].x;
						linePositions [4] = new Vector3 (x, linePositions[0].y, 0);
					}

				}
			}*/
		}
	}


	void extendLightX()
	{
			if (linePositions [4].y < 6) 
		{
				Vector3 point1 = linePositions [3];
				Vector3 point2 = linePositions [4];
				float slope = (point2.y - point1.y) / (point2.x - point1.x);
				float yIntercept = point1.y - slope * point1.x;
			if(linePositions[3].x < linePositions[4].x)
			{
				linePositions [4] = new Vector3 (15, 15 * slope + yIntercept, 0);
			}
			else
			{
				linePositions[4] = new Vector3 (-15,-15*slope + yIntercept,0);
			}

			}
	}


	void extendLightY()
	{
		if (linePositions [4].y < 3) 
		{
			Vector3 point1 = linePositions[3];
			Vector3 point2 = linePositions[4];
			float slope = (point2.y - point1.y)/(point2.x-point1.x);
			float yIntercept = point1.y - slope * point1.x;
			linePositions[4] = new Vector3((-5-yIntercept)/slope,-5,0);
		}
	}


	void SetLightBeam()
	{
		for (int i = 0; i < linePositions.Count; i++)
		{
			lightBeam.SetPosition(i, linePositions[i]);
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
		linePositions[4] = new Vector3((float)(Mathf.Cos(angle1) * Nx - Mathf.Sin(angle1) * Ny + pivotPoint.x), (float)(Mathf.Sin(angle1) * Nx + Mathf.Cos(angle1) * Ny + pivotPoint.y), 0);


		Vector3 midpoint1 = new Vector3 (linePositions [0].x, 0, 0);
		float m1x = (midpoint1.x - pivotPoint.x);
		float m1y = (midpoint1.x - pivotPoint.x);
		linePositions[1] = new Vector3((float)(Mathf.Cos(angle1) * m1x - Mathf.Sin(angle1) * m1y + pivotPoint.x), (float)(Mathf.Sin(angle1) * m1x + Mathf.Cos(angle1) * m1y + pivotPoint.y), 0);


		Vector3 midpoint2 = new Vector3 (linePositions [1].x, 0, 0);
		float m2x = (midpoint2.x - pivotPoint.x);
		float m2y = (midpoint2.y - pivotPoint.y);
		linePositions[2] = new Vector3((float)(Mathf.Cos(angle1) * m2x - Mathf.Sin(angle1) * m2y + pivotPoint.x), (float)(Mathf.Sin(angle1) * m2x + Mathf.Cos(angle1) * m2y + pivotPoint.y), 0);


		Vector3 midpoint3 = new Vector3(linePositions[2].x,0,0);
		float m3x = (midpoint3.x - pivotPoint.x);
		float m3y = (midpoint3.y - pivotPoint.y);
		linePositions[3] = new Vector3((float)(Mathf.Cos(angle1) * m3x - Mathf.Sin(angle1) * m3y + pivotPoint.x), (float)(Mathf.Sin(angle1) * m3x + Mathf.Cos(angle1) * m3y + pivotPoint.y), 0);
	

		extendLightX ();
	}

	
	void RotateLightBeam() //method that apply rotation
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


