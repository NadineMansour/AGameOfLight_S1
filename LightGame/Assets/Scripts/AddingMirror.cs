using UnityEngine;
using System.Collections;


public class AddingMirror : MonoBehaviour 
{
	public Rigidbody Mirror;
	public float MirrorCount;
	private Vector3 screenPoint;
	private Vector3 offset;


	void OnMouseDown() 
	{ 
		if (GameObject.FindGameObjectsWithTag ("Mirror").Length < MirrorCount+1) 
		{
			screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
		else
		{
			return;
		}
	}


	void OnMouseDrag() 
	{ 
		Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
		transform.position = curPosition;
	}
}
