using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Summary:
//Manages the player's available money, together with the player's
//bet for the current hand. It also returns the winning based on the
//type of win that has been achieved.

public class BettingController : MonoBehaviour
{
	[SerializeField]
	int funds = 10;
	int tempBet = 1;
	[SerializeField]
	Text initialBet, totalMoney;

	public int Funds
	{
		get
		{
			return funds;
		}
		set
		{
			funds = value;
		}
	}

	void UpdateTotalMoney()
	{
		totalMoney.text = Funds.ToString();
	}

	void UpdateBetText()
	{
		initialBet.text = tempBet.ToString();
	}

	public void Update()
	{
		if (GameManager.Instance.currentState ==
			GameManager.States.bettingPhase)
		{
			UpdateTotalMoney();
			PlaceBet(Funds);

			if (Input.GetKeyUp(KeyCode.Return) && tempBet > 0)
			{
				LockDownBet();
				UpdateTotalMoney();
			}
		}
		if (Funds <= 0)
		{
			OutOfMoney();
		}
	}

	void PlaceBet(int totalMoney)
	{
		if (Input.GetKeyUp(KeyCode.W) && tempBet < totalMoney)
		{
			tempBet += 1;
			UpdateBetText();
		}
		else if (Input.GetKeyUp(KeyCode.S) && tempBet > 1)
		{
			tempBet -= 1;
			UpdateBetText();
		}
	}

	void LockDownBet()
	{
		Funds = Funds - tempBet;
		GameManager.Instance.GoToState(GameManager.States.dealingCards);
	}

	public void AddReturnsToFunds(string winType)
	{
		funds += CalculateReturns(winType, tempBet);
		UpdateTotalMoney();
	}

	int CalculateReturns(string winType, int sizeOfBet)
	{
		int betReturns = 0;
		switch (winType)
		{
			case "Regular":
				betReturns = sizeOfBet * 2;
				return betReturns;

			case "BlackJack":
				betReturns = Mathf.FloorToInt(sizeOfBet * 1.5f);
				return betReturns;

			case "1HandSplit":
				return betReturns;

			case "2HandSplit":
				betReturns = betReturns * 2;
				return betReturns;
				
			case "Equal":
				betReturns = sizeOfBet;
				return betReturns;

			case "PlayerLost":
				return betReturns;

			default:
				Debug.Assert(false, "No valid win type was achieved");
				break;
		}
		return 0;
	}

	public void SplitBet()
	{
		Funds = Funds - tempBet;
		UpdateTotalMoney();
		tempBet = tempBet * 2;
		UpdateBetText();
	}

	void OutOfMoney()
	{
		GameManager.Instance.GoToState(GameManager.States.gameOver);
	}
}


