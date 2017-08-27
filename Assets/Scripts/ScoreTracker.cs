using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class ScoreTracker : MonoBehaviour
{
	private int score;
	public static ScoreTracker Instance;
	public Text ScoreText;
	public Text HighScoreText;
	public GameObject HighScoreIcon;

	public int Score {
		get { 
			return score;
		}
		set { 
			//Take score Animation
			ScoreText.GetComponent<Animator>().SetTrigger ("Taked Score");
			score = value;
			ScoreText.text = score.ToString ();


			if (PlayerPrefs.GetInt ("HighScore") < score) {
				//High Score Animation
				HighScoreIcon.GetComponent<Animator>().SetTrigger ("High Score");
				HighScoreText.GetComponent<Animator>().SetTrigger ("High Score");

				PlayerPrefs.SetInt ("HighScore", score);
				HighScoreText.text = score.ToString ();
			}

		}

	}

	void Awake ()
	{
		Instance = this;

		if (!PlayerPrefs.HasKey ("HighScore"))
			PlayerPrefs.SetInt ("HighScore", 0);

		HighScoreText.text = PlayerPrefs.GetInt ("HighScore").ToString ();
	}
		
}
