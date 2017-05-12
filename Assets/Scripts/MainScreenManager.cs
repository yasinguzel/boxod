using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScreenManager : MonoBehaviour {

	public Text moneyText;
	public Text highScoreText;
	public GameObject shopPanel;
	public GameObject opacityPanel;

	void Awake(){
		
		if (!PlayerPrefs.HasKey ("HighScore"))
			PlayerPrefs.SetInt ("HighScore", 0);

		highScoreText.text = PlayerPrefs.GetInt ("HighScore").ToString ();

		if (!PlayerPrefs.HasKey ("Money")) {
			PlayerPrefs.SetInt ("Money", 0);
		}

		moneyText.text = PlayerPrefs.GetInt ("Money").ToString ();
	}

	public void PlayGameButtonHandler(){
		SceneManager.LoadScene (1);
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
	}
}
