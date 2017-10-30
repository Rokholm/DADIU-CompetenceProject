using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BettingController : MonoBehaviour {

	[SerializeField]
	Button increaseBet1, decreaseBet1, increaseBet5, decreaseBet5, currentbet;
	[SerializeField]
	int betMoney = 0;
	//
	void BetButtonControl(int totalMoney, int betMoney)
	{
		if (betMoney > totalMoney)
		{
			increaseBet1.interactable = false;
			increaseBet5.interactable = false;
		}
		else if (betMoney <= 0)
		{
			decreaseBet1.interactable = false;
			decreaseBet5.interactable = false;
		}
		else
		{
			increaseBet1.interactable = true;
			increaseBet5.interactable = true;
			decreaseBet1.interactable = true;
			decreaseBet5.interactable = true;
		}
	}

	int CalculateReturns(bool blackJack, int sizeOfBet)
	{
		int betReturns = 0;
		if (blackJack == true)
		{
			betReturns = Mathf.FloorToInt(sizeOfBet * 1.5f);
		}
		else
		{
			betReturns = sizeOfBet;
		}
		return betReturns;
	}

	public void AddOneToBet()				{betMoney += 1;}

	public void SubtractOneFromBet()		{betMoney -= 1;}

	public void SubtractfiveToBet()			{betMoney -= 1;}

	public void AddFiveToBet()				{betMoney += 1;}
}


