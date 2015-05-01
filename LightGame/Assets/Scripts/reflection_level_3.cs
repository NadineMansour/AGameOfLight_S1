using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class reflection_level_3 : MonoBehaviour 
{
	public static bool rotateRight;
	public static bool rotateLeft;
	public LineRenderer lightBeam;//Lightbeam gameobject to edit positions and end points
	private List <Vector3> linePositions;//array containing lightbeam points for setting and editing
	public LineRenderer lightBeam2;
	private List <Vector3> linpoistions2;
	public GameObject tip1;
	public GameObject tip2;
	public GameObject tip3;
	public GameObject nexttip;

	private float angle;//degree of rotation of light beam
	public static bool gameOver;//check whether target is reached or not
	public static bool saveRec;
	public GameObject target;
	public GameObject bam;
	public GameObject toLevelsButton; //button that redirect to levels menu after winning
	public GameObject numOfClicks;
	//Variables for records table
	public float StartTime;
	public static int state;
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
		Vector3 end = start;
		end.y = -3;
		linePositions.Add (start);//Adding shooter's position
		linePositions.Add (end);//End of the lightbeam
		SetLightBeam (linePositions , lightBeam);//Setting the values of the lightbeam
		
		linpoistions2 = new List<Vector3> ();
		linpoistions2.Add (start);
		linpoistions2.Add (start);
		SetLightBeam (linpoistions2 ,  lightBeam2);

		tip1.SetActive (true);
		tip2.SetActive (false);
		tip3.SetActive (false);
		nexttip.SetActive (true);
		toLevelsButton.SetActive(false);//Disabling the tolevels button initially
		gameOver = false;
		saveRec = false;
		target.SetActive (true);
		bam.SetActive (false);
		level = 3;
		state = 0;
		StartTime = Time.realtimeSinceStartup;
		clicks = 0;
		RotateLightBeam ();
	}
	
	
	void calculateScore()
	{
		score = (120 - timeInLevel) * 100 + (5 - clicks) * 50;
		if (score <= 50) //to exclude negative scores
			score = 50;
	}


	void OnMouseDown()
	{
		if (tag == "next tip") 
		{
			if(state == 2)
			{
				tip3.SetActive(false);
				state++;
				nexttip.SetActive(false);
			}
			if(state == 1)
			{
				tip2.SetActive(false);
				state++;
				tip3.SetActive(true);
			}
			if(state == 0)
			{
				tip1.SetActive(false);
				state++;
				tip2.SetActive(true);
			}

		}
	}
	
	
	void EndGame()
	{
		if (state == 3) 
		{
			if (saveRec) {
				StartCoroutine (save_record ()); // save the record when the game ends
				calculateScore();
				target.SetActive (false); //enables target halo, indicatingt that light reached it
				toLevelsButton.SetActive (true); //enables the button that's used to redirect to other scene
				bam.SetActive (true); //enables target halo, indicatingt that light reached it
				saveRec = false;
			}
		}
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		TextMesh textObject = numOfClicks.GetComponent<TextMesh>();
		textObject.text = "Moves: " + clicks;
		if (state == 3) 
		{
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
				SetLightBeam (linePositions, lightBeam);
				SetLightBeam (linpoistions2, lightBeam2);
			} 
			else 
			{
				EndGame ();
			}
		}
	}
	
	
	public void detector()
	{
		if (!gameOver) 
		{
			RaycastHit hit ;
			if(Physics.Linecast(linePositions[0],linePositions[1],out hit))
			{
				Vector3 collision = hit.point;
				linePositions[1] = collision;
				linpoistions2[0] = collision;
				
				if(hit.collider.tag == "Mirror")
				{
					float y = 2*(linePositions[0].y - collision.y) ;
					linpoistions2[1] = new Vector3(linePositions[0].x,-y + linePositions[0].y,0);
					extendLightY(linpoistions2);
				}
				
				if(hit.collider.tag == "Obstacle")
				{
					linePositions[1] = collision;
				}
			}
			else
			{
				linpoistions2[0] = linePositions[0];
				linpoistions2[1] = linePositions[0];
				extendLightY(linePositions);
			}
			
			if(Physics.Linecast(linpoistions2[0],linpoistions2[1],out hit))
			{
				Vector3 collision = hit.point;
				
				if(hit.collider.tag == "Target")
				{
					gameOver = true;
					saveRec = true;
					linpoistions2[1] = collision;
				}
				
				if(hit.collider.tag == "Obstacle")
				{
					linpoistions2[1] = collision;
				}
				
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
			//l[1] = new Vector3((-5+(slope*l[0].x)-l[0].y)/slope,-5,0);
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
	
	
	//updates light renderer parameters with those stored in linePositions array
	void SetLightBeam(List <Vector3> l , LineRenderer light)
	{
		for (int i = 0; i < l.Count; i++) 
		{
			light.SetPosition(i, l[i]);
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
		float Nx = (pointToRotate.x - pivotPoint.x);
		float Ny = (pointToRotate.y - pivotPoint.y);
		float angle1 = angle * Mathf.PI / 180.0f;
		linePositions[1] = new Vector3((float)(Mathf.Cos(angle1) * Nx - Mathf.Sin(angle1) * Ny + pivotPoint.x), (float)(Mathf.Sin(angle1) * Nx + Mathf.Cos(angle1) * Ny + pivotPoint.y), 0);
		linpoistions2 [0] = linePositions [0];
		linpoistions2 [1] = linePositions [0];
	}
	
	
	//Called when player clicks on rotation buttons to rotate light beam with shooter
	void RotateLightBeam()
	{
		PointRotator ();
		SetLightBeam (linePositions ,  lightBeam);
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