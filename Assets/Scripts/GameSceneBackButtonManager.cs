using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneBackButtonManager : MonoBehaviour {
	
	GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = gameObject.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKeyUp (KeyCode.Escape)) {
				if (gameManager.areYouSurePanelIsActive)
				{
					gameManager.NoButtonHandler();
				}
				else if(gameManager.gameOverPanelIsActive){
					gameManager.CloseGameOverPanelButtonHandler();
				}
				else if(gameManager.shopPanelIsActive){
					gameManager.CloseShopPanelButtonHandler();
				}
				else if(gameManager.pausePanelIsActive){
					gameManager.ClosePausePanelButtonHandler();
				}
				else{
					gameManager.ExitToMainButtonHandler();
				}
			}
		}
	}
}
