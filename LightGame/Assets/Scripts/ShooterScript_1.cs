using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShooterScript_1 : MonoBehaviour {


	//for movement
	public static bool up;
	public static bool down;
	public static bool RUp;
	public static bool RDown;
	//Gameobjects
	public GameObject spotLight;
	public LineRenderer lightBeam;
	public GameObject nextButton;
	public GameObject Tip3;
	public GameObject numOfClicks;


	public static List<Vector3> linePositions; //for holding the line renderer points
	public static float angle; //for rotating the line renderer
	public static bool gameover;
	public static int state;
	public static int startTime;


	//variables for score
	public static int clicks;
	public static string log;
	public int score;
	public static int time;
	int level;


	// Use this for initialization
	void Start () 
	{
		state = 0;
		linePositions = new List<Vector3> ();
		linePositions.Add(spotLight.transform.position);
		linePositions.Add(spotLight.transform.position + new Vector3 (15, 0, 0));
		gameover = false;
		beamSetter ();
		log = "";
		clicks = 0;
		score = 0;
		time = 0;
		level = 1;
	}


	void calculateScore()
	{
		score = (120 - time) * 100 + (5 - clicks) * 50;
		//to exclude negative scores
		if (score <= 50) score = 50;
	}


	// Update is called once per frame
	void Update () 
	{
		if (state == 2) 
		{
			nextButton.SetActive(false);
			float factor = 2.844702467f;
			angle = Mathf.Rad2Deg * spotLight.transform.rotation.x * factor;
			if (!gameover) 
			{
				if (up && transform.position.y < 4)
					MoveUp ();
				if (down && transform.position.y > -2)
					MoveDown ();
				if (RUp && angle > -20)
					RotateCCW ();
				if (RDown && angle < 20)
					RotateCW ();
			}
			detector ();
			beamSetter ();
		}
		//display the number of moves/clicks
		TextMesh textObject = numOfClicks.GetComponent<TextMesh>();
		textObject.text = "Moves: "+clicks;
	}


	void MoveDown()
	{
		transform.position = transform.position - (new Vector3 (0, 0.05f, 0));
		linePositions[0] = linePositions[0] - (new Vector3 (0, 0.05f, 0));
		linePositions[1] = linePositions[1] - (new Vector3 (0, 0.05f, 0));
	}


	void MoveUp()
	{
		transform.position = transform.position + (new Vector3 (0, 0.05f, 0));
		linePositions [0] = linePositions [0] + (new Vector3 (0, 0.05f, 0));
		linePositions [1] = linePositions [1] + (new Vector3 (0, 0.05f, 0));
	}


	void RotateCW()
	{
		spotLight.transform.Rotate (new Vector3 (0.3f, 0, 0));
		Vector3 pivot = linePositions[0];
		Vector3 pointToRotate = new Vector3 (9, pivot.y, 0);
		float Nx = (pointToRotate.x - pivot.x);
		float rotationAngle = Mathf.Deg2Rad * angle;
		Vector3 rotatedPoint = new Vector3((float)(Mathf.Cos(rotationAngle*-1.0f) * Nx + pivot.x), (float)(Mathf.Sin(rotationAngle*-1.0f) * Nx+ pivot.y), 0);
		/*
			if (rotatedPoint.x < 10){
				Vector3 point0 = linePositions[0];
				Vector3 point1 = rotatedPoint;
				float slope = (float)(point1.y - point0.y)/(point1.x - point0.x);
				float yIntercept = point0.y - slope*point0.x;
				Vector3 point2 = new Vector3 (10,0,0);
				point2.y = 10.0f*slope + yIntercept;
				rotatedPoint = point2;
			}
			*/
		linePositions[1] = rotatedPoint;
	}


	void RotateCCW()
	{
		spotLight.transform.Rotate (new Vector3 (-0.3f, 0, 0));
		Vector3 pivot = linePositions[0];
		Vector3 pointToRotate = new Vector3 (9, pivot.y, 0);
		float Nx = (pointToRotate.x - pivot.x);
		float rotationAngle = Mathf.Deg2Rad * angle;
		Vector3 rotatedPoint = new Vector3((float)(Mathf.Cos(rotationAngle*-1.0f) * Nx + pivot.x), (float)(Mathf.Sin(rotationAngle*-1.0f) * Nx+ pivot.y), 0);
		/*
			if (rotatedPoint.x < 10){
				Vector3 point0 = linePositions[0];
				Vector3 point1 = rotatedPoint;
				float slope = (float)(point1.y - point0.y)/(point1.x - point0.x);
				float yIntercept = point0.y - slope*point0.x;
				Vector3 point2 = new Vector3 (10,0,0);
				point2.y = 10.0f*slope + yIntercept;
				rotatedPoint = point2;
			}
			*/
		linePositions[1] = rotatedPoint;
	}


	void detector()
	{
		RaycastHit hit;
		if (Physics.Linecast (linePositions [0], linePositions [1], out hit)) {
			if (hit.collider.tag == "Target") 
			{
				linePositions [1] = hit.point;
				gameover = true;
				time = (int)Time.timeSinceLevelLoad - startTime;
				calculateScore();
				//clicks, log, time and level are ready here.
				EndGame();
			}
		} 
		else 
		{
			extender();
		}
	}


	void EndGame()
	{
		Tip3.SetActive (true);
		StartCoroutine(save_record());
		state = 3;
		nextButton.SetActive (true);
	}


	void extender()
	{
		if (linePositions[1].x < 10){
			Vector3 point0 = linePositions[0];
			Vector3 point1 = linePositions[1];
			float slope = (float)(point1.y - point0.y)/(point1.x - point0.x);
			float yIntercept = point0.y - slope*point0.x;
			Vector3 point2 = new Vector3 (10,0,0);
			point2.y = 10*slope + yIntercept;
			linePositions[1] = point2;
		}
	}


	void beamSetter()
	{
		lightBeam.SetPosition(0, linePositions[0]);
		lightBeam.SetPosition(1, linePositions[1]);
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
		form.AddField ("time", time);
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
