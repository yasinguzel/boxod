using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MoneyButtonsHandler : MonoBehaviour
{

	public int moneyQuantity;

	public void moneyButtonHandler ()
	{
		MoneyTracker.Instance.Money += moneyQuantity;
	}
}
