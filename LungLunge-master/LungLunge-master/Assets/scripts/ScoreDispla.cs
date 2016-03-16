using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDispla : MonoBehaviour {
	public Text ScoreText;
	public Text TimeText;
	public Text ScoreCalc;
	int score;
	int time;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		time = PlayerPrefs.GetInt ("LevelTime");
		score = PlayerPrefs.GetInt ("LevelScore") + (4000 - (50*time)) + (PlayerPrefs.GetInt ("PufferCharge")*10);
		ScoreCalc.text =  "Score = " + PlayerPrefs.GetInt ("LevelScore")+ "+ \nTime Bonus: 4000 - (50*"+time+" seconds) +  \nPuffer Charge Bonus: ("+ PlayerPrefs.GetInt ("PufferCharge")+"*10)" ;
		ScoreText.text = "Final Score: " + score;
		TimeText.text = "Time: " + time + " seconds";
	}
}
