using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerScriptL3 : MonoBehaviour {

	public static bool RRight;
	public static bool RLeft;

	public LineRenderer lightBeam;               //Lightbeam gameobject to edit positions and end points
	private List <Vector3> linePositions;        //array containing lightbeam points for setting and editing
	private float angle ;                        //degree of rotation of light beam

	float NI = 1.000293f;
	float NR = 1.3330f;
	// Use this for initialization
	void Start () {
		linePositions = new List<Vector3> ();
		Vector3 start = transform.position;
		Vector3 mid = start;
		mid.y = 0;
		Vector3 end = start;
		end.y = -4; //Changing the x-value of end point to 9 (limit of the scene)
		linePositions.Add (start); //adding shooter's position as start point to light beam
		linePositions.Add (mid);
		linePositions.Add (end); //adding the end point to light beams points array
	
	}
	
	// Update is called once per frame
	void Update () {
		if (RLeft) {
			RotateLeft();
		}
		if (RRight) {
			RotateRight();
		}
	
	}

	void RotateRight(){
		float zz = transform.eulerAngles.z;
		if ((zz < 60 || zz >=300)) 
		{
			transform.Rotate (new Vector3(0,0,0.5f));
			angle+= 0.5f;
			RotateLightBeam();
		}
	}
	void RotateLeft(){
		float zz = transform.eulerAngles.z;
		if ((zz<=61 || zz > 301)) 
		{
			transform.Rotate (new Vector3(0,0,-0.5f));
			angle-= 0.5f;
			RotateLightBeam();
		}
	}

	public static void RRightTrue(){
		RRight = true;
	}
	public static void RRightFalse(){
		RRight = false;
	}
	public static void RLeftTrue(){
		RLeft = true;
	}
	public static void RLeftFalse(){
		RLeft = false;
	}

	//updates light renderer parameters with those stored in linePositions array
	void SetLightBeam()
	{
		for (int i = 0; i < linePositions.Count; i++) {
			lightBeam.SetPosition(i, linePositions[i]);
		}
	}

	//Rotates light beam with specified degree indicate dy variable degree around its starting point (Center of shooter)
	void PointRotator()
	{
		Vector3 pivotPoint = linePositions [0];
		Vector3 pointToRotate = new Vector3 (linePositions [0].x, 0, 0);
		float Nx = (pointToRotate.x - pivotPoint.x);
		float Ny = (pointToRotate.y - pivotPoint.y);
		float angle1 = angle * Mathf.PI / 180.0f;
		linePositions[1] = new Vector3((float)(Mathf.Cos(angle1) * Nx - Mathf.Sin(angle1) * Ny + pivotPoint.x), (float)(Mathf.Sin(angle1) * Nx + Mathf.Cos(angle1) * Ny + pivotPoint.y), 0);

		PointChecker ();

		//rotating the 2nd half of the lightbeam 
		float AI = angle1; //incidense angle
		float AR = ((float)Math.Asin(Math.Sin (AI) * NI / NR)); //angle of refraction
		Vector3 pivotPoint2 = linePositions [1];
		Vector3 pointToRotate2 = new Vector3 (linePositions[1].x, linePositions [1].y-4, linePositions [1].z);
		float Nx2 = (pointToRotate2.x - pivotPoint2.x);
		float Ny2 = (pointToRotate2.y - pivotPoint2.y);
		linePositions[2] = new Vector3((float)(Mathf.Cos(AR) * Nx2 - Mathf.Sin(AR) * Ny2 + pivotPoint2.x), (float)(Mathf.Sin(AR) * Nx2 + Mathf.Cos(AR) * Ny2 + pivotPoint2.y), 0);

	}
	 
	void PointChecker()
	{
		Vector3 endP = linePositions [1];
		if (endP.y >0) {
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


}
