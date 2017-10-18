using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandler : MonoBehaviour {

	static public int[] shuffledDeck = new int[52];
	int[] cardNumbers = new int[52];

	void Awake()
	{
		InitCardNumbers();
		ShuffleDeck();
		List <Card> cardData = CardAttributeList();
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
					if (randNum == shuffledDeck.Length)
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
		//for (int j = 0; j < emptyDeck.Length; j++)
		//{
		//	Debug.Log(emptyDeck[j]);
		//}
	}

	struct Card
	{
		int cardID;
		int cardValue;
		Suits cardSuit;

		public Card(int cardID, int cardValue, Suits cardSuit)
		{
			this.cardID = cardID;
			this.cardValue = cardValue;
			this.cardSuit = cardSuit;
		}

		public int GetCardID()
		{
			return cardID;
		}

		public int GetCardValue()
		{
			return cardValue;
		}
	}

	enum Suits {hearts, clubs, diamonds, spades}

	List<Card> CardAttributeList()
	{
		Suits suits = new Suits();
		List<Card> cardData = new List<Card>();

		for (int i = 1; i < 4; i++)
		{
			for (int j = 1; j < 14; j++)
			{
				if (j > 12)
				{
					Card newCard = new Card(i * j, 11, suits);
					cardData.Add(newCard);
				}

				else if (j > 9)
				{
					Card newCard = new Card(i * j, 10, suits);
					cardData.Add(newCard);
				}

				else
				{
					Card newCard = new Card(i * j, j+1, suits);
					cardData.Add(newCard);
				}
			}
			suits = suits + 1;
		}
		return cardData;
	}
}
