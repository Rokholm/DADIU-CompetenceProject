using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour {

	List<Card> cardsInHand = new List<Card>();

	public void AddCardToList(Card newCard)
	{
		cardsInHand.Add(newCard);
	}

	public int Score()
	{
		int valueSum = 0;
		for (int i = 0; i <= cardsInHand.Count; i++)
		{
			valueSum += cardsInHand[i].GetCardValue();
		}
		return valueSum;
	}

	bool CheckForLose()
	{
		if (Score() >  21)
		{
			return true;
		}

		else
		{
			return false;
		}
	}

}
