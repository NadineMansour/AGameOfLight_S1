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
	public LineRenderer lightBeam3;
	private List <Vector3> linpositions3;
	public LineRenderer lightBeam4;
	private List <Vector3> linpositions4;
	public LineRenderer lightBeam5;
	List<Vector3> linpositions5;
	public GameObject tip1;
	public GameObject tip2;
	public GameObject nexttip;
	public static int state;
	private float angle;
	public static bool gameOver;
	public static bool saveRec;
	public static bool target1Dead;
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
		linpositions3 = new List<Vector3> ();
		Vector3 line3Start = line1Start;
		Vector3 line3End = line1Start;
		linpositions3.Add (line3Start);
		linpositions3.Add (line3End);
		SetLightBeam (linpositions3,lightBeam3);
		linpositions4 = new List<Vector3> ();
		Vector3 line4Start = line1Start;
		Vector3 line4End = line1Start;
		linpositions4.Add (line4Start);
		linpositions4.Add (line4End);
		SetLightBeam (linpositions4,lightBeam4);
		linpositions5 = new List<Vector3> ();
		Vector3 line5Start = line1Start;
		Vector3 line5End = line1Start;
		linpositions5.Add (line5Start);
		linpositions5.Add (line5End);
		SetLightBeam (linpositions5, lightBeam5);
		extendLightX (linePositions);
		tip1.SetActive (true);
		tip2.SetActive (false);
		nexttip.SetActive (true);
		state = 0;
		gameOver = false;
		saveRec = true;
		target1Dead = false;
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


	void OnMouseDown()
	{
		if (tag == "next tip") 
		{
			if(state == 1)
			{
				tip2.SetActive(false);
				state++;
				nexttip.SetActive(false);
			}
			if(state == 0)
			{
				tip1.SetActive(false);
				state++;
				tip2.SetActive(true);
			}
		}
	}
	
	
	void calculateScore()
	{
		score = (120 - timeInLevel) * 100 + (5 - clicks) * 50;
		if (score <= 50) //to exclude negative scores
			score = 50;
	}
	
	
	void EndGame()
	{
		if (saveRec) 
		{
			StartCoroutine (save_record ()); // save the record when the game ends
			target.SetActive (false); //enables target halo, indicatingt that light reached it
			bam.SetActive (true); //enables target halo, indicatingt that light reached it
			to_next_level.SetActive (true);
			saveRec = false;
		}
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		TextMesh textObject = numOfClicks.GetComponent<TextMesh>();
		textObject.text = "Moves: " + clicks;
		if (state == 2) 
		{
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
				SetLightBeam (linePositions, lightBeam);
				SetLightBeam (linpositions2, lightBeam2);
				SetLightBeam (linpositions3, lightBeam3);
				SetLightBeam (linpositions4, lightBeam4);
				SetLightBeam (linpositions5, lightBeam5);
			} 
			else 
			{
				EndGame ();
			}
		}
	}


	void detector()
	{
		if (!gameOver) 
		{
			RaycastHit hit;
			if(Physics.Linecast(linePositions[0],linePositions[1],out hit))
			{
				Vector3 collision = hit.point;
				linePositions[1] = collision;
				linpositions2[0] = collision;
				
				if(hit.collider.tag == "horizontal mirror")
				{
					
					float x = 2*(collision.x - linePositions [0].x)+linePositions[0].x;
					linpositions2 [1] = new Vector3 (x, linePositions[0].y, 0);
					extendLightX(linpositions2);
				}
				else
				{
					if(hit.collider.tag == "vertical mirror")
					{
						float y = 2*(linePositions[0].y - collision.y) ;
						linpositions2[1] = new Vector3(linePositions[0].x,-y + linePositions[0].y,0);
						extendLightY(linpositions2);
						extendLightX(linpositions2);
					}
				}
				
				if(hit.collider.tag == "Obstacle")
				{
					linpositions2[1] = hit.point;
				}
			}
			else 
			{
				linpositions2[0] = linePositions[0];
				linpositions2[1] = linePositions[0];
				extendLightX(linePositions);
				
			}
			
			
			if(Physics.Linecast(linpositions2[0],linpositions2[1],out hit))
			{
				Vector3 collision = hit.point;
				linpositions2[1] = collision;
				linpositions3[0] = collision;
				
				if(hit.collider.tag == "horizontal mirror")
				{
					float x = 2*(collision.x - linpositions2[0].x) + linpositions2[0].x;
					linpositions3[1] = new Vector3(x,linpositions2[0].y,0);
					extendLightX(linpositions3);
				}
				else 
				{
					if(hit.collider.tag == "vertical mirror")
					{
						float y = 2*(linpositions2[0].y - collision.y) ;
						linpositions3[1] = new Vector3(linpositions2[0].x,-y + linpositions2[0].y,0);
						extendLightY(linpositions3);
						extendLightX(linpositions3);
					} 
					else
					{
						if (hit.collider.tag == "fixed mirror")
						{
							linpositions3[0] = collision;
							linpositions3[1] = new Vector3(3,4,0);
							extendLightY(linpositions3);
						}
					}
				}
				
				if(hit.collider.tag == "Obstacle")
				{
					linpositions3[1] = collision;
				}
				if(hit.collider.tag == "Target")
				{
					if(target1Dead)
					{
						gameOver = true;
					}
					saveRec = true;
					linpositions3[1] = collision;
					
				}
				if(hit.collider.tag == "target 1")
				{
					target1Dead = true;
					target1.SetActive(false);
				}
				
			}
			else 
			{
				linpositions3[0] = linePositions[0];
				linpositions3[1] = linePositions[0];
				extendLightX(linpositions3);
			}
			
			
			if(Physics.Linecast(linpositions3[0],linpositions3[1],out hit))
			{
				Vector3 collision = hit.point;
				linpositions3[1] = collision;
				linpositions4[0] = collision;
				if(hit.collider.tag == "horizontal mirror")
				{
					
					float x = 2*(collision.x - linpositions3[0].x) + linpositions3[0].x;
					linpositions4[1] = new Vector3(x,linpositions3[0].y,0);
					extendLightX(linpositions4);
				}
				else
				{
					if(hit.collider.tag == "vertical mirror")
					{
						float y = 2*(linpositions3[0].y - collision.y) ;
						linpositions4[1] = new Vector3(linpositions3[0].x,-y + linpositions3[0].y	,0);
						extendLightY(linpositions4);
					}
				}
				
				if(hit.collider.tag == "Obstacle")
				{
					linpositions4[1] = hit.point;
				}
				if(hit.collider.tag == "Target")
				{
					if(target1Dead)
					{
						gameOver = true;
					}
					saveRec = true;
					linpositions4[1] = collision;
				}
				if(hit.collider.tag == "target 1")
				{
					target1Dead = true;
					target1.SetActive(false);
				}
			}
			else
			{
				linpositions4[0] = linePositions[0];
				linpositions4[1] = linePositions[0];
			}
			
			
			if(Physics.Linecast(linpositions4[0] , linpositions4[1] ,out hit))
			{
				Vector3 collision = hit.point;
				linpositions4[1] = collision;
				linpositions5[0] = collision;
				
				if(hit.collider.tag == "horizontal mirror")
				{
					float x = 2*(collision.x - linpositions4[0].x) + linpositions4[0].x;
					linpositions5[1] =  new Vector3(x,linpositions4[0].y,0);
					extendLightX(linpositions5);
				}
				else 
				{
					if(hit.collider.tag == "vertical mirror")
					{
						float y = 2*(linpositions4[0].y - collision.y);
						linpositions5[1] = new Vector3(linpositions4[0].x,-y + linpositions4[0].y,0);
						extendLightY(linpositions5);
						extendLightX(linpositions5);
					}
				}
				if (hit.collider.tag == "Target")
				{
					if(target1Dead)
					{
						gameOver = true;
					}
					saveRec = true;
					linpositions5[1] = hit.point;
				}
				
				if(hit.collider.tag == "Obstacle")
				{
					linpositions5[1] = hit.point;
				}
			}
			else
			{
				linpositions5[0] = linePositions[0];
				linpositions5[1] = linePositions[0];
				extendLightX(linpositions4);
			}
			
			if(Physics.Linecast(linpositions5[0] , linpositions5[1],out hit))
			{
				Vector3 collision = hit.point;
				
				if(hit.collider.tag == "Obstacle")
				{
					linpositions5[1] = collision;
					
				}
				if(hit.collider.tag == "Target")
				{
					if(target1Dead)
					{
						gameOver = true;
					}
					saveRec = true;
					linpositions5[1] = collision;
				}
			}
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
		linpositions3 [0] = linePositions [0];
		linpositions3 [1] = linePositions [0];
		linpositions4 [0] = linePositions [0];
		linpositions4 [1] = linePositions [0];
		linpositions5 [0] = linePositions [0];
		linpositions5 [1] = linePositions [0];
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