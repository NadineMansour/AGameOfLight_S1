// boolean values to detetct on which button the mouse is 
var records=false;
var levels=false;
var to_main_menu = false;
var quit = false;


function OnMouseEnter()
{
	GetComponent.<Renderer>().material.color=Color.red;
}



function OnMouseExit()
{
	GetComponent.<Renderer>().material.color=Color.white;
}



function OnMouseUp()
{
	if(records == true)
	{
		Application.LoadLevel("score");
	}
	else if (levels)
	{
		Application.LoadLevel("World1");
	}
	else if (to_main_menu == true)
	{
		Application.LoadLevel("scene");
	}
	else if (quit == true)
	{
		Application.Quit();
	}
}