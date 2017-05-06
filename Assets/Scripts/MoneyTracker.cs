using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTracker : MonoBehaviour
{
	private int money;
	private bool isX2;
	public static MoneyTracker Instance;
	public Text MoneyText;
	public Text isX2Text;

	public int Money {
		get { 
			return money;
		}
		set { 
			money = value;
			MoneyText.text = money.ToString ();
			PlayerPrefs.SetInt ("Money", money);
		}

	}

	public bool IsX2 {
		get { 
			return isX2;
		}
		set { 
			isX2 = value;

			if (isX2) {
				PlayerPrefs.SetInt ("isX2", 1);
				isX2Text.text = "x2 True";
			} else {
				PlayerPrefs.SetInt ("isX2", 0);
				isX2Text.text = "x2 False";
			}
				
		}

	}


	void Awake ()
	{
		Instance = this;

		if (!PlayerPrefs.HasKey ("Money")) {
			PlayerPrefs.SetInt ("Money", 0);
			isX2 = false;
		}

		//for debug
		if (PlayerPrefs.GetInt("isX2") == 1) {
			isX2Text.text = "x2 True";
		} else {
			isX2Text.text = "x2 False";
		}

		MoneyText.text = PlayerPrefs.GetInt ("Money").ToString ();
		money = PlayerPrefs.GetInt ("Money");
		if (PlayerPrefs.GetInt ("isX2") == 1)
			isX2 = true;
		else
			isX2 = false;

	}

	void SetMoneyText (int money)
	{
		MoneyText.text = money.ToString ();
	}
}
