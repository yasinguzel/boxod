using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	private Tile[,] AllTiles = new Tile[5, 5];
	private List<Tile[]> columns = new List<Tile[]> ();
	private List<Tile[]> rows = new List<Tile[]> ();
	private List<Tile> EmptyTiles = new List<Tile> ();
	private List<int[]> savedTiles = new List<int[]> ();
	private List<int> savedScores = new List<int> ();

	private int destroyedRedBox;
	private int destroyedPurpleBox;
	private int destroyedGreenBox;
	private int totalDestroyedBox;

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
			if (LineOfTiles [i].Number != 0 && LineOfTiles [i].Number == LineOfTiles [i + 1].Number && LineOfTiles [i].mergedThisTurn == false && LineOfTiles [i + 1].mergedThisTurn == false && FindColorGroup (LineOfTiles, i) == FindColorGroup (LineOfTiles, i + 1)) {
				LineOfTiles [i].Number += 1;
				LineOfTiles [i + 1].Number = 0;
				LineOfTiles [i].mergedThisTurn = true;

				if (LineOfTiles [i].Number == 2 || LineOfTiles [i].Number == 6 || LineOfTiles [i].Number == 10) {
					ScoreTracker.Instance.Score += 3;
				} else if (LineOfTiles [i].Number == 3 || LineOfTiles [i].Number == 7 || LineOfTiles [i].Number == 11) {
					ScoreTracker.Instance.Score += 6;
				} else if (LineOfTiles [i].Number == 4 || LineOfTiles [i].Number == 8 || LineOfTiles [i].Number == 12) {
					ScoreTracker.Instance.Score += 12;
				}

				if (LineOfTiles [i].Number == 4) {
					LineOfTiles [i].Number = 0;
					GameObject.Find ("Canvas/Panel/DestroyedInfo").GetComponent<Text> ().text = "Destroyed Red Box";
					destroyedRedBox++;
				}
				if (LineOfTiles [i].Number == 8) {
					LineOfTiles [i].Number = 0;
					GameObject.Find ("Canvas/Panel/DestroyedInfo").GetComponent<Text> ().text = "Destroyed Purple Box";
					destroyedPurpleBox++;
				}
				if (LineOfTiles [i].Number == 12) {
					LineOfTiles [i].Number = 0;
					GameObject.Find ("Canvas/Panel/DestroyedInfo").GetComponent<Text> ().text = "Destroyed Green Box";
					destroyedGreenBox++;
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

				if (LineOfTiles [i].Number == 2 || LineOfTiles [i].Number == 6 || LineOfTiles [i].Number == 10) {
					ScoreTracker.Instance.Score += 3;
				} else if (LineOfTiles [i].Number == 3 || LineOfTiles [i].Number == 7 || LineOfTiles [i].Number == 11) {
					ScoreTracker.Instance.Score += 6;
				} else if (LineOfTiles [i].Number == 4 || LineOfTiles [i].Number == 8 || LineOfTiles [i].Number == 12) {
					ScoreTracker.Instance.Score += 12;
				}

				if (LineOfTiles [i].Number == 4) {
					LineOfTiles [i].Number = 0;
					GameObject.Find ("Canvas/Panel/DestroyedInfo").GetComponent<Text> ().text = "Destroyed Red Box";
					destroyedRedBox++;
				}
				if (LineOfTiles [i].Number == 8) {
					LineOfTiles [i].Number = 0;
					GameObject.Find ("Canvas/Panel/DestroyedInfo").GetComponent<Text> ().text = "Destroyed Purple Box";
					destroyedPurpleBox++;
				}
				if (LineOfTiles [i].Number == 12) {
					LineOfTiles [i].Number = 0;
					GameObject.Find ("Canvas/Panel/DestroyedInfo").GetComponent<Text> ().text = "Destroyed Green Box";
					destroyedGreenBox++;
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
				EmptyTiles [indexForNewNumber].Number = 5;
			else
				EmptyTiles [indexForNewNumber].Number = 9;
			
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
		if (LineOfTiles [index].Number >= 1 && LineOfTiles [index].Number <= 4)
			return "Yellow";
		else if (LineOfTiles [index].Number >= 5 && LineOfTiles [index].Number <= 8)
			return "Blue";
		else
			return "Green";
	}

	public void ShuffleButtonHandler ()
	{
		SaveGamePosition ();
		Tile[] AllTilesOneDim = GameObject.FindObjectsOfType<Tile> ();
		int[] tilesNumbers = new int [25];
		int k = 0;

		for (int i = 0; i < AllTiles.GetLength (0); i++) {
			for (int j = 0; j < AllTiles.GetLength (1); j++) {
				tilesNumbers [k] = AllTiles [i, j].Number;
				k++;
			}
		}

		k = 0;

		foreach (Tile t in AllTilesOneDim) {
			t.Number = tilesNumbers [k];
			AllTiles [t.indRow, t.indCol] = t;
			k++;
		}


	}

	public void UndoButtonHandler ()
	{
		UndoLastGamePosition ();
	}

	private void SaveGamePosition ()
	{
		int[] savedTilesNumbers = new int[25];
		int savedScore;

		int k = 0;
		for (int i = 0; i < AllTiles.GetLength (0); i++) {
			for (int j = 0; j < AllTiles.GetLength (1); j++) {
				savedTilesNumbers [k] = AllTiles [i, j].Number;
				k++;
			}
		}

		savedTiles.Add (savedTilesNumbers);

		savedScore = ScoreTracker.Instance.Score;
		savedScores.Add (savedScore);


	}

	private void UndoLastGamePosition ()
	{
		if (savedTiles.Count > 0) {
			int savedScore;
			savedScore = savedScores [savedScores.Count - 1];

			int[] savedTilesNumbers = new int[25];
			savedTilesNumbers = savedTiles [savedTiles.Count - 1];

			int k = 0;
			for (int i = 0; i < AllTiles.GetLength (0); i++) {
				for (int j = 0; j < AllTiles.GetLength (1); j++) {
					AllTiles [i, j].Number = savedTilesNumbers [k];
					k++;
				}
			}

			savedTiles.RemoveAt (savedTiles.Count - 1);

			ScoreTracker.Instance.Score = savedScore;

			savedScores.RemoveAt (savedScores.Count - 1);
		}
	}

	private void DestroyBoxToMoney ()
	{
		totalDestroyedBox = destroyedRedBox + destroyedPurpleBox + destroyedGreenBox;

		if (totalDestroyedBox != 0)
			MoneyTracker.Instance.Money += Square (totalDestroyedBox);

		if (totalDestroyedBox != 0 || destroyedRedBox != 0 || destroyedPurpleBox != 0 || destroyedGreenBox != 0) {
			Debug.Log ("Total Destroyed Box: " + totalDestroyedBox);
			Debug.Log ("Destroyed Red Box: " + destroyedRedBox);
			Debug.Log ("Destroyed Puprle Box: " + destroyedPurpleBox);
			Debug.Log ("Destroyed Green Box: " + destroyedGreenBox);
		}


		destroyedRedBox = 0;
		destroyedPurpleBox = 0;
		destroyedGreenBox = 0;
		totalDestroyedBox = 0;
	}

	private int Square (int repetition)
	{
		int number = 3;

		for (int i = 1; i < repetition; i++) {
			number = number * number;
		}

		return number;

	}

	public void NewGameButtonHandler ()
	{
		SceneManager.LoadScene (0);
	}

	public void Move (MoveDirection md, bool generate)
	{
		SaveGamePosition ();
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

		DestroyBoxToMoney ();

		if (moveMade) {
			UpdateEmptyTiles ();
			if (generate)
				Generate ();
		}
			
			
	}
}
