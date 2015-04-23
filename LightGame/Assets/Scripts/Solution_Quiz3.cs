using UnityEngine;
using System.Collections;


public class Solution_Quiz3 : MonoBehaviour {
	
	
	public GameObject correct;
	public GameObject incorrect;
	
	
	void OnMouseDown()
	{
		print (Quiz3.clickable);
		if(Quiz3.clickable){
			if (tag == "Right") 
			{
				Quiz3.chosenSolution = "Refraction";
				Quiz3.rightAnswer = true;
				correct.SetActive(true);
			}
			
			
			if(tag == "Wrong")
			{
				Quiz3.rightAnswer = false;
				incorrect.SetActive(true);
				if(name == "Sphere")
				{
					Quiz3.chosenSolution = "Reflection";
				}
				if(name == "Sphere 3")
				{
					Quiz3.chosenSolution = "Dispersion";
				}
				if(name == "Sphere 2")
				{
					Quiz3.chosenSolution = "Deviation";
				}
			}
			
			
			Quiz3.state++;
		}
	}
}
