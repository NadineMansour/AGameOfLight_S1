using UnityEngine;
using System.Collections;


public class Solution_Quiz3 : MonoBehaviour {
	
	
	public GameObject correct;
	public GameObject incorrect;
	
	
	void OnMouseDown()
	{
		print (Quiz3.clickable);
		if(Quiz3.clickable){
			if (tag == "right") 
			{
				Quiz3.chosenSolution = "Refraction";
				Quiz3.rightAnswer = true;
				correct.SetActive(true);
			}
			
			
			if(tag == "wrong")
			{
				Quiz3.rightAnswer = false;
				incorrect.SetActive(true);
				if(name == "S1")
				{
					Quiz3.chosenSolution = "Reflection";
				}
				if(name == "S3")
				{
					Quiz3.chosenSolution = "Dispersion";
				}
				if(name == "S2")
				{
					Quiz3.chosenSolution = "Deviation";
				}
			}
			
			
			Quiz3.state++;
		}
	}
}
