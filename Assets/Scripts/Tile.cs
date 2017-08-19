using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
	public bool mergedThisTurn = false;
	public int indRow;
	public int indCol;

	private Animator anim;
	public int Number {
		get {
			return number;
		}
		set {
			number = value;
			if (number == 0)
				SetEmpty ();
			else {
				ApplyStyle (number);
				SetVisible ();
			}
		}
	}

	private int number;

	private Image tileImage;

	void Awake ()
	{
		anim = GetComponent<Animator> ();
		tileImage = transform.Find ("ColoredCell").GetComponent<Image> ();
	}

	public void PlayMergeAnimation(int i){
		anim.SetTrigger ("Merged");
	}

	public void PlayExplodedAnim(int tileNumber){
		if(tileNumber == 4){
			transform.Find("Image").GetComponent<Image>().color = TileStyleHolder.Instance.tileStyles[3].color;
		}
		else if(tileNumber == 8){
			transform.Find("Image").GetComponent<Image>().color = TileStyleHolder.Instance.tileStyles[7].color;
		}
		else if(tileNumber == 12){
			transform.Find("Image").GetComponent<Image>().color = TileStyleHolder.Instance.tileStyles[11].color;
		}
		anim.SetTrigger ("Exploded");
	}

	public void PlayAppearAnimation(){
		anim.SetTrigger ("Appear");
	}

	void ApplyStyleFromHolder (int index)
	{
		tileImage.sprite = TileStyleHolder.Instance.tileStyles [index].sprite;
		tileImage.color = TileStyleHolder.Instance.tileStyles[index].color;
	}

	void ApplyStyle (int num)
	{
		switch (num) {
		case 1:
			ApplyStyleFromHolder (0);
			break;
		case 2:
			ApplyStyleFromHolder (1);
			break;
		case 3:
			ApplyStyleFromHolder (2);
			break;
		case 4:
			ApplyStyleFromHolder (3);
			break;
		case 5:
			ApplyStyleFromHolder (4);
			break;
		case 6:
			ApplyStyleFromHolder (5);
			break;
		case 7:
			ApplyStyleFromHolder (6);
			break;
		case 8:
			ApplyStyleFromHolder (7);
			break;
		case 9:
			ApplyStyleFromHolder (8);
			break;
		case 10:
			ApplyStyleFromHolder (9);
			break;
		case 11:
			ApplyStyleFromHolder (10);
			break;
		case 12:
			ApplyStyleFromHolder (11);
			break;
		default:
			Debug.Log ("Please Select Correct index");
			break;
		}
	}

	private void SetVisible ()
	{
		tileImage.enabled = true;
	}

	private void SetEmpty ()
	{
		tileImage.sprite = TileStyleHolder.Instance.empty.sprite;
		tileImage.color = TileStyleHolder.Instance.empty.color;
	}
}
