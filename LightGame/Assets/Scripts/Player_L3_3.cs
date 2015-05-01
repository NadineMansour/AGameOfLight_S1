using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Player_L3_3 : MonoBehaviour {


	//For The Rotation 
	public static bool RRight;					
	public static bool RLeft;
	
	
	//Used in the Refraction
	private static float angle ;
	float NI = 1.000293f;						 
	float NR = 1.3330f;
	
	
	//For score
	public static int startTime;
	public static int timeInLevel;
	public static int clicks;
	public int level;
	public int score;
	public static string log;
	
	
	public static bool gameOver;
	public static int state;
	
	
	public GameObject nextButton;
	public GameObject intro;
	public GameObject start;
	public GameObject end;
	public GameObject Mirror;
	//public GameObject Stopwatch;
	//public GameObject numOfClicks;
	
	
	public LineRenderer lightBeam;                      //the main lightbeam used in the refraction 
	public LineRenderer lightBeam2;                     //simulates the reflection of the 1st lightbeam
	public static Vector3 position;
	private static List <Vector3> linePositions;        //array containing lightbeam  points for setting and editing
	private static List <Vector3> linePositions2; 		//array containing lightbeam2 points for setting and editing


	void Start () {
		float x = transform.position.x;
		float y = transform.position.y;
		linePositions  = new List<Vector3> ();
		linePositions2 = new List<Vector3> (); 
		linePositions.Add (new Vector3(x, y, 0));
		linePositions.Add (new Vector3(x, 0, 0));
		linePositions.Add (new Vector3(x, -4.0f, 0));
		linePositions2.Add (new Vector3 (0, 0, 0));
		linePositions2.Add (new Vector3 (0, 0, 0));
		gameOver = false;
		RRight = false;
		RLeft  = false;
		angle = 0.0f;
		level = 8; 
		state = -1;
		nextButton.SetActive (false);
		intro.SetActive (false);
		start.SetActive (false);
		end.SetActive (false);
	}


	void Update () {
		position = transform.position;


		if (state == -1) 
		{
			nextButton.SetActive (true);
			intro.SetActive (true);
			start.SetActive (false);
			end.SetActive (false);
		}


		if (state == 0) 
		{
			nextButton.SetActive (true);
			intro.SetActive (false);
			start.SetActive (true);
			end.SetActive (false);
		}

		if(state == 1)
		{
			nextButton.SetActive (false);
			intro.SetActive (false);
			start.SetActive (false);
			end.SetActive (false);
			linePositions2 [0] = new Vector3 (0, 0, 0);
			linePositions2 [1] = new Vector3 (0, 0, 0);
			if (RRight)
			{
				RotateRight();
			}
			if (RLeft)
			{
				RotateLeft();
			}
			endExtender();
			limitChecker2();
			limitChecker1();
			detector ();
			SetLightBeam();
		}


		if (state == 2) 
		{
			nextButton.SetActive (true);
			intro.SetActive (false);
			start.SetActive (false);
			end.SetActive (true);
		}
		
	}


	void endExtender()
	{
		if (linePositions[2].y != -4.0f)
		{
			if (linePositions[1].x == linePositions[2].x)
			{
				linePositions[2] = new Vector3(linePositions[1].x , -4.0f, 0);
			}
			else
			{
				Vector3 diff = linePositions[2] - linePositions[1];
				float slope = (float)(diff.y / diff.x);
				float yIntercept = linePositions[2].y - linePositions[2].x*slope;
				float extendedX = (-4.0f - yIntercept)/slope;
				linePositions[2] = new Vector3(extendedX, -4.0f, 0.0f);
			}
		}
	}


	public static void movement(int x)
	{
		switch(x)
		{
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
	
	void limitChecker1()
	{
		float slope, yIntercept, newY;
		Vector3 top = linePositions [0];
		Vector3 mid = linePositions [1];
		
		
		if (mid.x <= -5.8f || mid.x >= 5.8f) 
		{
			linePositions [2] = linePositions [0];
			slope = (mid.y - top.y) / (mid.x - top.x);
			yIntercept = mid.y - slope * mid.x;
			if (mid.x <= -5.8f) 
			{
				newY = -5.8f * slope + yIntercept;
				linePositions [1] = new Vector3 (-5.8f, newY, 0);
			} 
			else 
			{
				newY = 5.8f * slope + yIntercept;
				linePositions [1] = new Vector3 (5.8f, newY, 0);
			}	
		} 
	}
	
	void limitChecker2()
	{
		Vector3 mid = linePositions [1];
		Vector3 end = linePositions [2];
		float newY;
		
		
		if((end.x <= -6.1f || end.x >= 6.1f))
		{
			float slope = (end.y - mid.y) / (end.x - mid.x);
			float yIntercept = end.y - slope * end.x;
			if (end.x <= -6.1f) 
			{
				newY = -6.1f * slope + yIntercept;
				linePositions [2] = new Vector3 (-6.1f, newY, 0);
			} 
			else 
			{
				newY = 6.1f * slope + yIntercept;
				linePositions [2] = new Vector3 (6.1f, newY, 0);
			}	
		}
		
	}
	
	
	void moveRight()
	{
		if (position.x < 4) 
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
		if (position.x > -4)
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


		for (int i = 0; i < linePositions2.Count; i++) 
		{
			lightBeam2.SetPosition(i, linePositions2[i]);
		}
	}

	
	//Detects collision of light with other gameobjects
	void detector()
	{
		if (!gameOver) 
		{
			bool extend = true;
			RaycastHit hit;
			if (Physics.Linecast (linePositions [1], linePositions [2], out hit)) {
				if (hit.collider.tag == "Obstacle") 
				{
					linePositions [2] = hit.point;
					extend = false;
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
				else
				{
					if(extend)
					{
						Vector3  mid  = linePositions[2];
						Vector3 start = linePositions[1];
						if(mid.x != start.x)
						{
							Vector3 end = new Vector3();
							float slope = (mid.y - start.y)/(mid.x - start.x);
							float yIntercept = mid.y - slope*mid.x;
							end.y = -4.0f;
							end.z = 0;
							end.x = (-4.0f - yIntercept)/slope;
							linePositions[2] = end;
						}
					}
				}
			}
			
			
			RaycastHit hit1;
			if (Physics.Linecast (linePositions2 [0], linePositions2 [1], out hit1)) 
			{
				if (hit1.collider.tag == "Target") 
				{
					gameOver = true;
					if (Mirror_8.MirrorL || Mirror_8.MirrorR)
					{
						Mirror_8.MirrorL = false;
						Mirror_8.MirrorR = false;
						log += "xEnd: " + Mirror.transform.position.x + '\n';
					}
					if(RRight || RLeft)
					{
						RRight = false;
						RLeft  = false;
						log += " zEnd: " + angle + '\n'; 
					}
					state++;
					timeInLevel = (int) Time.timeSinceLevelLoad - startTime;
					calculateScore();
					print(log);
					StartCoroutine(save_record());
				}
			}


		}
	}


	void lineExtender2(){
		Vector3 testedPoint2 = linePositions2 [1];
		if (testedPoint2.y != -4.0f) 
		{
			Vector3 P1 = linePositions2[0];
			Vector3 P2 = linePositions2[1];
			if(P2.x != P1.x) //To avoid dividing by 0
			{
				float slope = (P2.y - P1.y) / (P2.x - P1.x);
				float yIntercept = P1.y - slope*P1.x;
				float newX = (-4.0f - yIntercept)/slope;
				linePositions2[1] = new Vector3(newX, -4.0f, 0);
			}
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
	
	
	void calculateScore()
	{
		score = (120 - timeInLevel) * 100 + (5 - clicks) * 50;
		//to exclude negative scores
		if (score <= 50) score = 50;
	}
}