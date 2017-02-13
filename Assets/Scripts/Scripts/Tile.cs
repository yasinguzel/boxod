using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

	public int Number{
		get{ 
			return number;
		}
		set{ 
			number = value;

			if (number == 0) {
				SetEmpty ();
			} else {
				ApplyStyle (number);
				SetVisible ();
			}
		}
	}


	private int number;

	private Image tileImage;

	void Awake(){
		tileImage = transform.Find ("ColoredCell").GetComponent<Image> ();
	}

	void ApplyStyleFromHolder(int index){
		tileImage.sprite = TileStyleHolder.Instance.tileStyles [index].sprite;
	
	}

	void ApplyStyle(int num){
		switch (num) {
			case 1:
				ApplyStyleFromHolder (0);
				break;
			case 2:
				ApplyStyleFromHolder (1);
				break;
		default:
				Debug.Log ("Please Select Correct index");
				break;
		}
	}

	private void SetVisible(){
		
		tileImage.enabled = true;
	}

	private void SetEmpty(){

		tileImage.enabled = false;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
