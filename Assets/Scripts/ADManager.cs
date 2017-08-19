using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Heyzap;

public class ADManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		HeyzapAds.Start("9a5b94190642abf2b815c547052624d3", HeyzapAds.FLAG_NO_OPTIONS);
		HZBannerShowOptions showOptions = new HZBannerShowOptions();
		showOptions.Position = HZBannerShowOptions.POSITION_TOP;
		HZBannerAd.ShowWithOptions(showOptions);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void testAD(){
		
	}
}
