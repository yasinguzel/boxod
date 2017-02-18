﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	private Tile[,] AllTiles = new Tile[5, 5];
	private List<Tile[]> columns = new List<Tile[]> ();
	private List<Tile[]> rows = new List<Tile[]> ();
	private List<Tile> EmptyTiles = new List<Tile> ();

	// Use this for initialization
	void Start ()
	{
		Tile[] AllTilesOneDim = GameObject.FindObjectsOfType<Tile> ();

		foreach (Tile t in AllTilesOneDim) {
			t.Number = 0;
			AllTiles [t.indRow, t.indCol] = t;
			EmptyTiles.Add (t);
		}

		columns.Add (new Tile[] {
			AllTiles [0, 0],
			AllTiles [1, 0],
			AllTiles [2, 0],
			AllTiles [3, 0],
			AllTiles [4, 0],
		});
		columns.Add (new Tile[] {
			AllTiles [0, 1],
			AllTiles [1, 1],
			AllTiles [2, 1],
			AllTiles [3, 1],
			AllTiles [4, 1],
		});
		columns.Add (new Tile[] {
			AllTiles [0, 2],
			AllTiles [1, 2],
			AllTiles [2, 2],
			AllTiles [3, 2],
			AllTiles [4, 2],
		});
		columns.Add (new Tile[] {
			AllTiles [0, 3],
			AllTiles [1, 3],
			AllTiles [2, 3],
			AllTiles [3, 3],
			AllTiles [4, 3],
		});
		columns.Add (new Tile[] {
			AllTiles [0, 4],
			AllTiles [1, 4],
			AllTiles [2, 4],
			AllTiles [3, 4],
			AllTiles [4, 4],
		});

		rows.Add (new Tile[] {
			AllTiles [0, 0],
			AllTiles [0, 1],
			AllTiles [0, 2],
			AllTiles [0, 3],
			AllTiles [0, 4],
		});
		rows.Add (new Tile[] {
			AllTiles [1, 0],
			AllTiles [1, 1],
			AllTiles [1, 2],
			AllTiles [1, 3],
			AllTiles [1, 4],
		});
		rows.Add (new Tile[] {
			AllTiles [2, 0],
			AllTiles [2, 1],
			AllTiles [2, 2],
			AllTiles [2, 3],
			AllTiles [2, 4],
		});
		rows.Add (new Tile[] {
			AllTiles [3, 0],
			AllTiles [3, 1],
			AllTiles [3, 2],
			AllTiles [3, 3],
			AllTiles [3, 4],
		});
		rows.Add (new Tile[] {
			AllTiles [4, 0],
			AllTiles [4, 1],
			AllTiles [4, 2],
			AllTiles [4, 3],
			AllTiles [4, 4],
		});



		Generate ();
		Generate ();

	}



	bool MakeOneMoveDownIndex (Tile[] LineOfTiles)
	{
		for (int i = 0; i < LineOfTiles.Length - 1; i++) {
			//MOVE BLOCK 
			if (LineOfTiles [i].Number == 0 && LineOfTiles [i + 1].Number != 0) {
				LineOfTiles [i].Number = LineOfTiles [i + 1].Number;
				LineOfTiles [i + 1].Number = 0;
				return true;
			}

			//MERGE BLOCK
			if (LineOfTiles [i].Number != 0 && LineOfTiles [i].Number == LineOfTiles [i + 1].Number && LineOfTiles [i].mergedThisTurn == false && LineOfTiles [i + 1].mergedThisTurn == false) {
				LineOfTiles [i].Number += 1;
				LineOfTiles [i + 1].Number = 0;
				LineOfTiles [i].mergedThisTurn = true;

				if (LineOfTiles [i].Number == 3) {
					LineOfTiles [i].Number = 0;
					GameObject.Find ("Canvas/Panel/Text").GetComponent<Text> ().text = "Destroyed Blue Box";
				} else if (LineOfTiles [i].Number == 6) {
					LineOfTiles [i].Number = 0;
					GameObject.Find ("Canvas/Panel/Text").GetComponent<Text> ().text = "Destroyed Yellow Box";
				} else if (LineOfTiles [i].Number == 9) {
					LineOfTiles [i].Number = 0;
					GameObject.Find ("Canvas/Panel/Text").GetComponent<Text> ().text = "Destroyed Green Box";
				}
				
				return true;
			}

		}
		return false;
	}

	bool MakeOneMoveUpIndex (Tile[] LineOfTiles)
	{
		for (int i = LineOfTiles.Length - 1; i > 0; i--) {
			
			//MOVE BLOCK 
			if (LineOfTiles [i].Number == 0 && LineOfTiles [i - 1].Number != 0) {
				LineOfTiles [i].Number = LineOfTiles [i - 1].Number;
				LineOfTiles [i - 1].Number = 0;
				return true;
			}

			//MERGE BLOCK
			if (LineOfTiles [i].Number != 0 && LineOfTiles [i].Number == LineOfTiles [i - 1].Number && LineOfTiles [i].mergedThisTurn == false && LineOfTiles [i - 1].mergedThisTurn == false && FindColorGroup (LineOfTiles, i) == FindColorGroup (LineOfTiles, i - 1)) {
				LineOfTiles [i].Number += 1;
				LineOfTiles [i - 1].Number = 0;
				LineOfTiles [i].mergedThisTurn = true;

				if (LineOfTiles [i].Number == 3) {
					LineOfTiles [i].Number = 0;
					GameObject.Find ("Canvas/Panel/Text").GetComponent<Text> ().text = "Destroyed Blue Box";
				} else if (LineOfTiles [i].Number == 6) {
					LineOfTiles [i].Number = 0;
					GameObject.Find ("Canvas/Panel/Text").GetComponent<Text> ().text = "Destroyed Yellow Box";
				} else if (LineOfTiles [i].Number == 9) {
					LineOfTiles [i].Number = 0;
					GameObject.Find ("Canvas/Panel/Text").GetComponent<Text> ().text = "Destroyed Green Box";
				}
				
				return true;
			}


		}
		return false;
	}

	void Generate ()
	{
		if (EmptyTiles.Count > 0) {
			int indexForNewNumber = Random.Range (0, EmptyTiles.Count);
			int randomNum = Random.Range (0, 3);
			if (randomNum == 0)
				EmptyTiles [indexForNewNumber].Number = 1;
			else if (randomNum == 2)
				EmptyTiles [indexForNewNumber].Number = 4;
			else
				EmptyTiles [indexForNewNumber].Number = 7;
			
			EmptyTiles.RemoveAt (indexForNewNumber);
		}
	}

	private void ResetMergedFlags ()
	{
		foreach (Tile t in AllTiles)
			t.mergedThisTurn = false;
	}

	private void UpdateEmptyTiles ()
	{
		EmptyTiles.Clear ();
		foreach (Tile t in AllTiles) {
			if (t.Number == 0) {
				EmptyTiles.Add (t);
			}
		}
	}

	private string FindColorGroup (Tile[] LineOfTiles, int index)
	{
		if (LineOfTiles [index].Number >= 1 && LineOfTiles [index].Number <= 3)
			return "Yellow";
		else if (LineOfTiles [index].Number >= 4 && LineOfTiles [index].Number <= 6)
			return "Blue";
		else
			return "Green";
	}

	public void NewGameButtonHandler ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void Move (MoveDirection md)
	{
		Debug.Log (md.ToString () + "move");

		ResetMergedFlags ();
		bool moveMade = false;

		for (int i = 0; i < rows.Count; i++) {
			switch (md) {
			case MoveDirection.Down:
				while (MakeOneMoveUpIndex (columns [i])) {
					moveMade = true;
				}
				break;
			case MoveDirection.Left:
				while (MakeOneMoveDownIndex (rows [i])) {
					moveMade = true;
				}
				break;
			case MoveDirection.Right:
				while (MakeOneMoveUpIndex (rows [i])) {
					moveMade = true;	
				}
				break;
			case MoveDirection.Up:
				while (MakeOneMoveDownIndex (columns [i])) {
					moveMade = true;
				}
				break;
			}
		}

		if (moveMade) {
			UpdateEmptyTiles ();
			Generate ();
		}
			
			
	}
}
