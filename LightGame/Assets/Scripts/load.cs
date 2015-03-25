using UnityEngine;
using System.Collections;
using SimpleJSON;


public class load : MonoBehaviour {

	public string email;
	public ArrayList levels = new ArrayList();
	public ArrayList clicks = new ArrayList();
	public ArrayList times = new ArrayList();
	public ArrayList scores = new ArrayList();

	//3Dtext for levels in World1 scene
	public bool level1 = false;
	public bool level2 = false;

	//3Dtext for scores in scores scene
	public bool score1;
	public bool score2;
	public bool score3;
	public bool score4;
	public bool score5;
	public bool levelNo1;
	public bool levelNo2;
	public bool levelNo3;
	public bool levelNo4;
	public bool levelNo5;

	// Use this for initialization
	void Start () {
		email = ButtonLogin.user_email;
		StartCoroutine (get_records_by_email(email));
	}

	
	// Update is called once per frame
	void Update () {

	}



	void OnMouseUp()
	{
		// no restrictions on level 1
		//level1 is loaded
		if (level1 == true)
		{
			Application.LoadLevel("Level1");
		}
		//checks if the level is unlocked and finished
		// if true level2 is loaded
		if (level2 == true && (levels.Contains(2) == true || levels.Contains(1)))
		{

			Application.LoadLevel("Level2");
		}
	}


	
	IEnumerator get_records_by_email(string user_email) {
		levels.Clear();
		clicks.Clear ();
		times.Clear ();
		scores.Clear ();
		string urlMessage = "https://k12-mariammohamed.c9.io/api/records/get_records_by_email";
		WWWForm form = new WWWForm();
		form.AddField("email", user_email);
		WWW w = new WWW(urlMessage, form);
		yield return w;


		if (!string.IsNullOrEmpty(w.error)) {
			Debug.Log(w.error);
		}
		else {
			var N = JSON.Parse(w.text);
			int i = 0;
			while (true) {
				if (N[i] == null) {
					break;
				}
				levels.Add ( N[i]["record"]["level"].AsInt);
				times.Add ( N[i]["record"]["time"].AsInt);
				scores.Add ( N[i]["record"]["score"].AsInt);
				clicks.Add ( N[i]["record"]["clicks"].AsInt);

				if(i < scores.Count)
				{
					//change the String in the 3Dtext componant to the values in the arrays after getting the scores of the user.
					if (score1 == true)
					{
						
						GetComponent<TextMesh>().text =(string) (N[0]["record"]["score"]);
					}
					else if (levelNo1 == true)
					{
						GetComponent<TextMesh>().text =(string) (N[0]["record"]["level"]);
					}
					else if(score2 == true)
					{
						GetComponent<TextMesh>().text =(string) (N[1]["record"]["score"]);
					}
					else if (levelNo2 == true)
					{
						GetComponent<TextMesh>().text =(string) (N[1]["record"]["level"]);
					}
					else if (score3 == true)
					{
						GetComponent<TextMesh>().text =(string) (N[2]["record"]["score"]);
					}
					else if (levelNo3 == true)
					{
						GetComponent<TextMesh>().text =(string) (N[2]["record"]["level"]);
					}
					else if (score4 == true)
					{
						GetComponent<TextMesh>().text =(string) (N[3]["record"]["score"]);
					}
					else if (levelNo4 == true)
					{
						GetComponent<TextMesh>().text =(string) (N[3]["record"]["level"]);
					}
					else if (score5 == true)
					{
						GetComponent<TextMesh>().text =(string) (N[4]["record"]["score"]);
					}
					else if (levelNo5 == true)
					{
						GetComponent<TextMesh>().text =(string) (N[5]["record"]["level"]);
					}
				}

				i = i + 1;
			}
			//Checks if level 2 is finished
			if (level2 == true && levels.Contains(2)==false && levels.Contains(1)==false){
				GetComponent<TextMesh>().text = "LOCKED";
			}
		}
	}
}
