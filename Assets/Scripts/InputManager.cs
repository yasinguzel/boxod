﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
	Right,
	Up,
	Left,
	Down
}

public class InputManager : MonoBehaviour
{

	private GameManager gm;

	void Awake ()
	{
		gm = GameObject.FindObjectOfType<GameManager> ();
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			gm.Move (MoveDirection.Right, true);
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			gm.Move (MoveDirection.Up, true);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			gm.Move (MoveDirection.Left, true);
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			gm.Move (MoveDirection.Down, true);
		}

	}
}
