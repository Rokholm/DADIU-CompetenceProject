using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandler : MonoBehaviour {

	int[] emptyDeck = new int[52];
	int[] cardNumbers = new int[52];

	void Awake()
	{
		InitCardNumbers();
		ShuffleDeck();
		List <Card> cardData = CardAttributeList();
		//Debug.Log(cardData[4].getCardID());
	}

	void InitCardNumbers()
	{
		int j = 1;
		for (int i = 0; i < 52; i++)
		{
			//Debug.Log(cardNumbers[i]);
			cardNumbers[i] = i + 1;
			//Debug.Log(cardNumbers[i]);
			j++;
		}
		return;
	}

	void ShuffleDeck()
	{
		for (int i = 0; i < cardNumbers.Length; i++)
		{
			int randNum = Random.Range(0, 51);
			//Debug.Log(emptyDeck[randNum]);
			if (emptyDeck[randNum] == 0)
			{
				emptyDeck[randNum] = cardNumbers[i];
			}

			else
			{
				int OriginalRandNum = randNum;
				while (emptyDeck[randNum] > 0)
				{
					randNum += 1;
					if (randNum == emptyDeck.Length)
					{
						randNum = 0;
					}

					else if (randNum == OriginalRandNum)
					{
						//Debug.Log("breaks");
						break;
					}
				}
				emptyDeck[randNum] = cardNumbers[i];
			}

			return;
		}
	}

	struct Card
	{
		int cardID;
		int cardValue;
		Suits cardSuit;
		//Prefab cardPrefab
		

		public Card(int cardID, int cardValue, Suits cardSuit)
		{
			this.cardID = cardID;
			this.cardValue = cardValue;
			this.cardSuit = cardSuit;
			//this.cardTexture = cardTexture;
		}

		public int getCardID()
		{
			return cardID;
		}
	}

	enum Suits {hearts, clubs, diamonds, spades}

	List<Card> CardAttributeList()
	{
		Suits suits = new Suits();
		List<Card> cardData = new List<Card>();

		for (int i = 1; i < 4; i++)
		{
			suits = suits + 1;

			for (int j = 2; j < 14; j++)
			{
				if (j > 10)
				{
					Card newCard = new Card(i * j, 10, suits);
					cardData.Add(newCard);
				}

				else if (j > 13)
				{
					Card newCard = new Card(i * j, 11, suits);
					cardData.Add(newCard);
				}

				else
				{
					Card newCard = new Card(i * j, j, suits);
					cardData.Add(newCard);
				}
			}
		}
		return cardData;
	}
}
