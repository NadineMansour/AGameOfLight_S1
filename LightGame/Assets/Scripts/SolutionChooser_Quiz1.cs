using UnityEngine;
using System.Collections;


public class SolutionChooser_Quiz1 : MonoBehaviour {


	public GameObject correct;
	public GameObject incorrect;


	void OnMouseDown()
	{
		if(Quiz1.clickable){
			if (tag == "Right") 
			{
				Quiz1.chosenSolution = "Rays";
				Quiz1.rightAnswer = true;
				correct.SetActive(true);
			}


			if(tag == "Wrong")
			{
				Quiz1.rightAnswer = false;
				incorrect.SetActive(true);
				if(name == "Cube2")
				{
					Quiz1.chosenSolution = "Circles";
				}
				if(name == "Cube3")
				{
					Quiz1.chosenSolution = "Cubes";
				}
				if(name == "Cube4")
				{
					Quiz1.chosenSolution = "Waves";
				}
			}


			Quiz1.state++;
		}
	}
}
