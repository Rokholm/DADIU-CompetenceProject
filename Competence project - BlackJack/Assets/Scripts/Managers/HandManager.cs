using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour {

	List<Card> cardHand = new List<Card>();

	public void AddCardToList(Card newCard)
	{
		cardHand.Add(newCard);
	}

	public int Score()
	{
		int valueSum = 0;
		for (int i = 0; i <= cardHand.Count; i++)
		{
			valueSum += cardHand[i].GetCardValue();
		}
		return valueSum;
	}

	public struct Card
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
}
