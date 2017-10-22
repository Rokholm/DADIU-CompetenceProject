using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour {
	[SerializeField]
	List<Card> playerCardsInHand = new List<Card>();
	[SerializeField]
	List<Card> dealerCardsInHand = new List<Card>();
	[SerializeField]
	private Text scoreTextPlayer, scoreTextDealer;

	DrawCard drawCard; 
	int timesCardHasBeenGiven = 4;

	private void Awake()
	{
		drawCard = GetComponent<DrawCard>();
	}

	public void DealingStateCardsToList()
	{
		PlayerAddCardToList(DeckHandler.Instance.cardData
						   [DeckHandler.Instance.shuffledDeck[0]]);
		DealerAddCardToList(DeckHandler.Instance.cardData
						   [DeckHandler.Instance.shuffledDeck[1]]);
		PlayerAddCardToList(DeckHandler.Instance.cardData
						   [DeckHandler.Instance.shuffledDeck[2]]);
		DealerAddCardToList(DeckHandler.Instance.cardData
						   [DeckHandler.Instance.shuffledDeck[3]]);
	}

	public void PlayerAddCardToList(Card newCard)
	{
		playerCardsInHand.Add(newCard);
	}

	public void DealerAddCardToList(Card newCard)
	{
		dealerCardsInHand.Add(newCard);
	}
																				//																																											
	public void Hit(bool player)
	{
		if (player == true)
		{
			drawCard.SpawnCard("player",timesCardHasBeenGiven);
			PlayerAddCardToList(DeckHandler.Instance.cardData
							   [DeckHandler.Instance.shuffledDeck
							   [timesCardHasBeenGiven]]);
			Debug.Log("card should have been added to list");
			PlayerScore();
		}
		else
		{
			drawCard.SpawnCard("dealer", timesCardHasBeenGiven);
			DealerAddCardToList(DeckHandler.Instance.cardData
							   [DeckHandler.Instance.shuffledDeck
							   [timesCardHasBeenGiven]]);
			Debug.Log("card should have been added to list");
			DealerScore();
		}
		timesCardHasBeenGiven = +1;
	}

	public void Stay()
	{
		GameManager.Instance.GoToNextState();
	}

	public int PlayerScore()
	{
		int valueSum = 0;
		for (int i = 0; i < playerCardsInHand.Count; i++)
		{
			if (playerCardsInHand[i].GetCardValue() == 11 && (valueSum + 11) > 21)
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
		int valueSum = 0;
		for (int i = 0; i < dealerCardsInHand.Count; i++)
		{
			if (dealerCardsInHand[i].GetCardValue() == 11 && (valueSum + 11) > 21)
			{
				valueSum += 1;
			}
			else
			{
				valueSum += dealerCardsInHand[i].GetCardValue();
			}
			scoreTextDealer.text = valueSum.ToString();
		}
		return valueSum;
	}

	public void DealerStateBehaviour()
	{
		while (DealerScore() < 17)
		{
			Hit(false);
		}
		Stay();
	}

	public int CheckWinOrLoseCondition()
	{
		if (PlayerScore() >= DealerScore() && 22 != PlayerScore())
		{
			return 1;
		}
		else if (PlayerScore() > 22)
		{
			return 2;
		}
		else
		{
			return 0;
		}
	}
}
