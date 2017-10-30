using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListsAndScoreHandler : MonoBehaviour
{
	public List<Card> playerCardsInHand = new List<Card>();
	public List<Card> dealerCardsInHand = new List<Card>();
	[SerializeField]
	private Text scoreTextPlayer, scoreTextDealer, endGameText;
	ActionsHandler actionsHandler;
																				
	private void Awake()
	{
		actionsHandler = GetComponent<ActionsHandler>();
	}

	public void DealingStateCardsToList()
	{
		PlayerAddCardToList(DeckHandler.Instance.cardData
						   [DeckHandler.Instance.shuffledDeck[0] - 1]);
		DealerAddCardToList(DeckHandler.Instance.cardData
						   [DeckHandler.Instance.shuffledDeck[1] - 1]);
		PlayerAddCardToList(DeckHandler.Instance.cardData
						   [DeckHandler.Instance.shuffledDeck[2] - 1]);
		DealerAddCardToList(DeckHandler.Instance.cardData
						   [DeckHandler.Instance.shuffledDeck[3] - 1]);
		UpdateScoreText(true);
		UpdateScoreText(false);
	}

	public void PlayerAddCardToList(Card newCard)
	{
		playerCardsInHand.Add(newCard);
	}

	public void DealerAddCardToList(Card newCard)
	{
		dealerCardsInHand.Add(newCard);
	}
																																											

	public int PlayerScore()
	{
		int valueSum = 0;
		for (int i = 0; i < playerCardsInHand.Count; i++)
		{
			if (playerCardsInHand[i].GetCardValue() == 11
				&& (valueSum + 11) > 21)
			{
				valueSum += 1;
			}
			else
			{
				valueSum += playerCardsInHand[i].GetCardValue();
			}
		}
		scoreTextPlayer.text = valueSum.ToString();
		return valueSum;
	}

	public int DealerScore()
	{
		int valueSum = 0, faceDownCard = 0;
		for (int i = 0; i < dealerCardsInHand.Count; i++)
		{
			if (1 == i)
			{
				faceDownCard = dealerCardsInHand[i].GetCardValue();
			}
			else
			{
				if (dealerCardsInHand[i].GetCardValue() == 11
					&& (valueSum + 11) > 21)
				{
					valueSum += 1;
				}
				else
				{
					valueSum += dealerCardsInHand[i].GetCardValue();
				}
			}
		}
		if (GameManager.Instance.currentState == GameManager.States.dealerTurn ||
			GameManager.Instance.currentState == GameManager.States.endGame)
		{
			valueSum = valueSum + faceDownCard;
			scoreTextDealer.text = valueSum.ToString();
		}
		else
		{
			scoreTextDealer.text = valueSum.ToString();
		}
		scoreTextDealer.text = valueSum.ToString();
		return valueSum;
	}

	//make this a enum, so it becomes less likely to be confused with 
	// a meaningful number in the game.

	public void CheckWinOrLose()
	{
		int winOrLose = 0;

		if (PlayerScore() >= DealerScore() && PlayerScore() <= 21
			&& GameManager.Instance.currentState == GameManager.States.endGame)
		{
			winOrLose = 1;
		}
		else if (PlayerScore() > 21)
		{
			winOrLose = 2;
		}
		else if (DealerScore() > 21)
		{
			winOrLose = 3;
		}
		else if (PlayerScore() < DealerScore() && 
				GameManager.Instance.currentState == GameManager.States.endGame)
		{
			winOrLose = 4;
		}

		switch (winOrLose)
		{
			case 1:
				endGameText.text = "Congratulations, You won the hand!";
				break;

			case 2:
				endGameText.text = "Sorry the dealer wins, You went over 21! ";
				actionsHandler.PlayerStay();
				break;

			case 3:
				endGameText.text = "Dealer above 21, You won the hand";
				actionsHandler.EndDealerTurn();
				break;

			case 4:
				endGameText.text = "The dealer won the hand";
				break;

			default:
				Debug.Assert(false, "A non-existing Win/Lose condition" + 
							"was achieved");
				break;
		}
	}

	public IEnumerator DealerStateBehaviour()
	{
		if (PlayerScore() > 21)
		{
			yield return new WaitForSeconds(1);
			actionsHandler.EndDealerTurn();
		}
		else
		{
			while (DealerScore() < 17)
			{
				actionsHandler.Hit(false);
			}
			yield return new WaitForSeconds(1);
			actionsHandler.EndDealerTurn();
		}
	}

	public void ResetLists(bool playerList)
	{
		if (playerList == true)
		{
			playerCardsInHand.Clear();
		}
		else
		{
			dealerCardsInHand.Clear();
		}
	}

	 void UpdateScoreText(bool playerScore)
	{
		if (playerScore == true)
		{
			scoreTextPlayer.text = PlayerScore().ToString();
		}
		else
		{
			scoreTextDealer.text = DealerScore().ToString();
		}
	}

	public void TestIfListsEmpty()
	{
		bool dealerListEmpty = true;

		foreach (Card element in dealerCardsInHand)
		{
			if (element != null)
			{
				dealerListEmpty = false;
			}
		}
		bool playerListEmpty = true;
		foreach (Card element in playerCardsInHand)
		{
			if (element != null)
			{
				playerListEmpty = false;
			}
		}
		Debug.Assert(playerListEmpty, "playerCardsInHand list has not been emptied");
		Debug.Assert(dealerListEmpty, "dealerCardsInHand list has not been emptied");
	}
}
																					

