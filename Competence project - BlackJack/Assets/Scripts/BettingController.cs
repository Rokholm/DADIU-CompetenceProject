using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BettingController : MonoBehaviour
{
	//[SerializeField]
	//Button increaseBet1, decreaseBet1, increaseBet5, decreaseBet5, currentbet;
	[SerializeField]
	int betMoney = 0;
	int funds = 10;
	int tempBet = 1;
	[SerializeField]
	Text text, text2;

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
		text2.text = Funds.ToString();
	}

	void UpdateBetText()
	{
		text.text = tempBet.ToString();
	}
	
	public void Update()
	{
		if (GameManager.Instance.currentState == 
			GameManager.States.bettingPhase)
		{
			UpdateTotalMoney();
			PlaceBet(Funds);

			if (Input.GetKeyUp(KeyCode.Return))
			{
				LockDownBet();
			}
		}

		if (Funds <= 0)
		{
			OutOfMoney();
		}
		// Get a trigger for the betting state. (Check)

		// Make player able to place a bet on the hand. (Maybe)

		// Tell the gameManager to go to next state. (Check)

		// Recieve data from gameManager on the win & type of win
		// 21 or split, or regular.

		//update the funds variable based on returns / losses.
		
	}

	void PlaceBet(int totalMoney)
	{
			if (Input.GetKeyUp(KeyCode.W) && tempBet < totalMoney )
			{
				tempBet += 1;
				UpdateBetText();
			}
			else if (Input.GetKeyUp(KeyCode.S) && tempBet > 0)
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

	void OutOfMoney()
	{
		GameManager.Instance.GoToState(GameManager.States.endGame);
	}

	//void BetButtonControl(int totalMoney, int betMoney)
	//{
	//	if (betMoney > totalMoney)
	//	{
	//		increaseBet1.interactable = false;
	//		increaseBet5.interactable = false;
	//	}
	//	else if (betMoney <= 0)
	//	{
	//		decreaseBet1.interactable = false;
	//		decreaseBet5.interactable = false;
	//	}
	//	else
	//	{
	//		increaseBet1.interactable = true;
	//		increaseBet5.interactable = true;
	//		decreaseBet1.interactable = true;
	//		decreaseBet5.interactable = true;
	//	}
	//}

	/*public void AddOneToBet()
	{
		betMoney += 1;
	}

	public void SubtractOneFromBet()
	{
		betMoney -= 1;
	}

	public void SubtractfiveToBet()
	{
		betMoney -= 1;
	}

	public void AddFiveToBet()
	{
		betMoney += 1;
	}*/
}


