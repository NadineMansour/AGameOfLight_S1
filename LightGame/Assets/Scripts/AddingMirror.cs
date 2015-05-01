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
			screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}


	void OnMouseUp()
	{
		reflection_level_4.clicks++;
		reflection_level_5.clicks++;
	}


	void OnMouseDrag() 
	{ 
		Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
		transform.position = curPosition;
	}
}
