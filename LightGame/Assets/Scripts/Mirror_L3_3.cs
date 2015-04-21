using UnityEngine;
using System.Collections;


public class Mirror_L3_3 : MonoBehaviour {


	public static bool MirrorL;
	public static bool MirrorR;


	void Start () {
		MirrorL = false;
		MirrorR = false;
	}

	
	void Update () {
		//if(ShooterScript....
		//{
			if (MirrorR && transform.position.x < 1) 
			{
				transform.position = transform.position + new Vector3(0.05f, 0, 0); 
			}
			if (MirrorL && transform.position.x > -5) 
			{
				transform.position = transform.position - new Vector3(0.05f, 0, 0); 
			}

		//}
	}


	public static void moveMirror(int x)
	{
		//Go Right
		if (x == 0) {
			MirrorR = true;
		}
		//Go Left
		if (x == 1) {
			MirrorL = true;
		}
		//Stop Right
		if (x == 2) {
			MirrorR = false;
		}
		//Stop Left
		if (x == 3) {
			MirrorL = false;
		}
	}
}
