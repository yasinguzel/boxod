using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScreenManager : MonoBehaviour {

	public Text moneyText;
	public Text exitInfoText;
	public Text highScoreText;
	public GameObject shopPanel;
	public GameObject opacityPanel;
	public int buttonPressedCount;

	void Awake(){
		buttonPressedCount = 0;
		if (!PlayerPrefs.HasKey ("HighScore"))
			PlayerPrefs.SetInt ("HighScore", 0);

		highScoreText.text = PlayerPrefs.GetInt ("HighScore").ToString ();

		if (!PlayerPrefs.HasKey ("Money")) {
			PlayerPrefs.SetInt ("Money", 0);
		}

		moneyText.text = PlayerPrefs.GetInt ("Money").ToString ();
	}

	public void PlayGameButtonHandler(){
		SceneManager.LoadScene (2);
	}

	public void ShopButtonHandler(){
		shopPanel.SetActive (true);
		opacityPanel.SetActive (true);
		shopPanel.GetComponent<Animator>().SetTrigger ("open");
	}

	public void CloseShopButtonHandler(){
		shopPanel.GetComponent<Animator>().SetTrigger ("close");
		//shopPanel.SetActive (false);
		opacityPanel.SetActive (false);

	}

	void Update(){
		moneyText.text = PlayerPrefs.GetInt ("Money").ToString ();
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKeyUp (KeyCode.Escape)) {
				buttonPressedCount++;
			}
		}
		if (buttonPressedCount == 1) {
			exitInfoText.text = "Press again to exit";
			Invoke ("timeOutExit", 2);
		} else if (buttonPressedCount == 2) {
			Application.Quit ();
		}
	}

	void timeOutExit()
	{
		exitInfoText.text = "";
		buttonPressedCount = 0;
	}
}
