using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTracker : MonoBehaviour
{
	private int money;
	public static MoneyTracker Instance;
	public Text MoneyText;
	//public Text HighScoreText;

	public int Money {
		get { 
			return money;
		}
		set { 
			money = value;
			MoneyText.text = money.ToString ();
			PlayerPrefs.SetInt ("Money", money);
			//HighScoreText.text = money.ToString ();

			/*if (PlayerPrefs.GetInt ("Money") < money) {
				PlayerPrefs.SetInt ("Money",money);
				HighScoreText.text = money.ToString ();
			}*/

		}

	}


	void Awake ()
	{
		Instance = this;

		if (!PlayerPrefs.HasKey ("Money"))
			PlayerPrefs.SetInt ("Money", 0);

		//ScoreText.text = "000";
		MoneyText.text = PlayerPrefs.GetInt ("Money").ToString ();
		money = PlayerPrefs.GetInt ("Money");
	}
}
