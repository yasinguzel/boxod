using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TileStyle{

	public Sprite sprite;
}

public class TileStyleHolder : MonoBehaviour {

	public static TileStyleHolder Instance;

	public TileStyle[] tileStyles;

	void Awake(){
		Instance = this;
	}
}
