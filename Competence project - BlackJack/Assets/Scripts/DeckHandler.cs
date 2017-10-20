using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandler : Singleton<DeckHandler> {

	static public int[] shuffledDeck = new int[52];
	int[] cardNumbers = new int[52];
	
	[SerializeField]
	List<Card> cardData;
	DrawCard drawCard;

	void Awake()
	{
		drawCard = GetComponent<DrawCard>();
		InitCardNumbers();
		ShuffleDeck();
		cardData = CardAttributeList();
		//Debug.Log(cardData[11].GetCardValue());
		for (int i = 0; i < 13; i++)
		{
			//Debug.Log(emptyDeck[i]);

		}
	}

	void InitCardNumbers()
	{
		for (int i = 0; i < 52; i++)
		{
			//Debug.Log(cardNumbers[i]);
			cardNumbers[i] = i + 1;
			//Debug.Log(cardNumbers[i]);
			
		}
	}

	void ShuffleDeck()
	{
		// initialize the shuffledeck array to zero;
		for (int i = 0; i < cardNumbers.Length; i++)
		{
			int randNum = Random.Range(0, 51);
			//Debug.Log(emptyDeck[randNum]);
			if (shuffledDeck[randNum] == 0)
			{
				shuffledDeck[randNum] = cardNumbers[i];
			}

			else
			{
				int OriginRandNum = randNum;
				while (shuffledDeck[randNum] > 0)
				{
					randNum += 1;
					if (randNum >= shuffledDeck.Length)
					{
						randNum = 0;
					}

					else if (randNum == OriginRandNum)
					{
						Debug.Log("Something went wrong");
						return;
					}
				}
				shuffledDeck[randNum] = cardNumbers[i];
			}
		}
		// check if all the cards are present in the shuffled array. 
		// See if the array is set to zero beforehand. 
		//for (int j = 0; j < emptyDeck.Length; j++)
		//{
		//	Debug.Log(emptyDeck[j]);
		//}
	}

	public enum Suits
	{
		hearts, clubs, diamonds, spades
	}

	List<Card> CardAttributeList()
	{
		Suits suit = new Suits();
		List<Card> cardsData = new List<Card>();
		GameObject[] models = drawCard.GetModels();

		for (int i = 1, cardId = 0; i <= 4; i++, suit += 1)
		{
			for (int j = 1; j < 14; j++, cardId++)
			{
				if (13 == j)
				{
					Card newCard = new Card(cardId, 11, suit,models[cardId]);
					cardsData.Add(newCard);
				}

				else if (j > 9)
				{
					Card newCard = new Card(cardId, 10, suit, models[cardId]);
					cardsData.Add(newCard);
				}

				else
				{
					Card newCard = new Card(cardId, j+1, suit, models[cardId]);
					cardsData.Add(newCard);
				}
			}
		}
		return cardsData;
	}
}
