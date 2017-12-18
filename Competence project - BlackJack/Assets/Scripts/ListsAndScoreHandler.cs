using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Summary:
//Calls and handles the lists that player and dealer cards are saved in.
//Also keeps track of the scores for player and dealer, together
//with the dealer decision making based on these. 

public class ListsAndScoreHandler : MonoBehaviour
{
	public List<Card> playerCardsInHand = new List<Card>();
	public List<Card> SplitCardsInSecondHand = new List<Card>();
	public List<Card> dealerCardsInHand = new List<Card>();
	[SerializeField]
	private Text scoreTextPlayer, scoreTextDealer, endGameText, scoreSplitHand;
	[SerializeField]
	private Button splitButton;
	public Button uiSplitHand;
	ActionsHandler actionsHandler;
	public string winType;

	private void Awake()
	{
		actionsHandler = GetComponent<ActionsHandler>();
	}

	public enum WinOrLose
	{
		noWinner,
		playerWon,
		blackJack,
		equal,
		dealerBust,
		playerBust,
		dealerWin,
		playerWinSplitHand,
		playerWinSplitHands
	}

	public WinOrLose typeOfWin;

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
		if (GameManager.Instance.currentState == GameManager.States.playerSplit)
		{
			SplitCardsInSecondHand.Add(newCard);
		}
		else
		{
			playerCardsInHand.Add(newCard);
		}
	}

	public void DealerAddCardToList(Card newCard)
	{
		dealerCardsInHand.Add(newCard);
	}

	public int PlayerScore(bool primaryHand)
	{
		List<Card> currentList = new List<Card>();
		int valueSum = 0;
		Text scoreText;

		if (primaryHand == true)
		{
			currentList = playerCardsInHand;
			scoreText = scoreTextPlayer;
		}
		else
		{
			currentList = SplitCardsInSecondHand;
			scoreText = scoreSplitHand;
		}
		for (int i = 0; i < currentList.Count; i++)
		{
			if (currentList[i].GetCardValue() == 11
				&& (valueSum + 11) > 21)
			{
				valueSum += 1;
			}
			else
			{
				valueSum += currentList[i].GetCardValue();
			}
			//checks if a previous card was an Ace, and corrects to one on playerbust.
			if (valueSum + currentList[i].GetCardValue() > 21)
			{
				for (int j = 0; j < currentList.Count; j++)
				{
					if (currentList[j].GetCardValue() == 11)
					{
						valueSum -= 10;
					}
				}
			}
		}
		scoreText.text = valueSum.ToString();
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
			GameManager.Instance.currentState == GameManager.States.endOfHand)
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

	public void CheckWinOrLose()
	{
		//Checks for Player Bust
		if (PlayerScore(true) > 21)
		{
			scoreTextPlayer.text = "Bust!";
			if (SplitCardsInSecondHand.Count != 0)
			{
				// Checks for Player Bust while split
				if (PlayerScore(false) > 21)
				{
					scoreSplitHand.text = "Bust!";
					typeOfWin = WinOrLose.playerBust;
				}
				else if (SplitCardsInSecondHand.Count != 0)
				{
					GameManager.Instance.GoToState(GameManager.States.playerSplit);
				}
			}
			else
			{
				typeOfWin = WinOrLose.playerBust;
			}
		}
		//Checks for dealer bust
		else if (DealerScore() > 21)
		{
			typeOfWin = WinOrLose.dealerBust;
		}
		//Checks for player win 
		else if (PlayerScore(true) > DealerScore() && PlayerScore(true) <= 21
			&& GameManager.Instance.currentState == GameManager.States.dealerTurn)
		{
			if (PlayerScore(true) == 21 && SplitCardsInSecondHand.Count != 0)
			{
				typeOfWin = WinOrLose.blackJack;
			}
			else
			{
				typeOfWin = WinOrLose.playerWinSplitHand;
			}
		}
		else if (PlayerScore(true) == DealerScore() &&
				GameManager.Instance.currentState == GameManager.States.endOfHand)
		{
			typeOfWin = WinOrLose.equal;
		}
		else if (PlayerScore(true) < DealerScore() &&
				GameManager.Instance.currentState == GameManager.States.endOfHand)
		{
			if (PlayerScore(false) < DealerScore() || SplitCardsInSecondHand.Count == 0)
			{
				typeOfWin = WinOrLose.dealerWin;
			}
		}

		switch (typeOfWin)
		{
			case WinOrLose.playerWon:
				endGameText.text = "Congratulations, You won the hand!";
				winType = "Regular";
				break;

			case WinOrLose.equal:
				endGameText.text = "Your score and the Dealer's are equal, neither wins";
				winType = "Equal";
				break;

			case WinOrLose.playerBust:
				endGameText.text = "Sorry the dealer wins, You went over 21! ";
				winType = "PlayerLost";
				GameManager.Instance.GoToState(GameManager.States.endOfHand);
				break;

			case WinOrLose.dealerBust:
				endGameText.text = "Dealer above 21, You won the hand";
				scoreTextDealer.text = "Bust";
				winType = "Regular";
				GameManager.Instance.GoToState(GameManager.States.endOfHand);
				break;

			case WinOrLose.dealerWin:
				endGameText.text = "The dealer won the hand";
				winType = "PlayerLost";
				break;

			case WinOrLose.blackJack:
				endGameText.text = "Hurray, you won the hand with Black Jack!";
				winType = "BlackJack";
				break;

			case WinOrLose.noWinner:
				Debug.Log("no winner found yet");
				break;

			default:
				Debug.Assert(false, "A non-existing Win/Lose condition" +
							"was achieved");
				break;
		}
	}

	public IEnumerator DealerStateBehaviour()
	{
		if (PlayerScore(true) > 21)
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
			scoreTextPlayer.text = PlayerScore(true).ToString();
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

	public void CheckForSplit()
	{
		if (playerCardsInHand[0].cardValue == playerCardsInHand[1].cardValue &&
			playerCardsInHand.Count < 3)
		{
			splitButton.interactable = true;
		}
	}

	public void TransferCardIntoSecondHand()
	{
		SplitCardsInSecondHand.Insert(0, playerCardsInHand[1]);
		playerCardsInHand.Remove(playerCardsInHand[1]);
		PlayerScore(true);
		PlayerScore(false);
	}

	// check if playerListInHand had the same value in (1), as InSecondHand has in (0)
}