using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandler : MonoBehaviour {

	int[] emptyDeck = new int[52];
	int[] cardNumbers = new int[52];

	void Awake()
	{
		InitCardNumbers();
		//Debug.Log(cardNumbers[30]);
		ShuffleDeck();
	}

	int [] InitCardNumbers()
	{
		int j = 1;
		for (int i = 0; i < 52; i++)
		{
			//Debug.Log(cardNumbers[i]);
			cardNumbers[i] = i + 1;
			//Debug.Log(cardNumbers[i]);
			j++;
		}

		return cardNumbers;
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
		}

		/*for (int j = 0; j < emptyDeck.Length; j++)
		{
			Debug.Log(emptyDeck[j]);
		}*/
	}
}
