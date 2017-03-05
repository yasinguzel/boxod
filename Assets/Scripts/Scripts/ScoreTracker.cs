using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{

	private int score;
	public static ScoreTracker Instance;
	public Text ScoreText;
	public Text HighScoreText;

	public int Score {
		get { 
			return score;
		}
		set { 
			score = value;
			ScoreText.text = "Score: "+score.ToString ();

			if (PlayerPrefs.GetInt ("HighScore") < score) {
				PlayerPrefs.SetInt ("HighScore",score);
				HighScoreText.text = "HighScore: "+score.ToString ();
			}

		}

	}


	void Awake ()
	{
		Instance = this;

		if (!PlayerPrefs.HasKey ("HighScore"))
			PlayerPrefs.SetInt ("HighScore", 0);

		ScoreText.text = "Score: 000";
		HighScoreText.text = "HighScore: "+PlayerPrefs.GetInt ("HighScore").ToString ();
	}

}
