using UnityEngine;
																				
[System.Serializable]
public class Card
{                                                                                   
	public int cardID;
	public int cardValue;
	public DeckHandler.Suits cardSuit;
	public GameObject model;
	public Card(int cardID, int cardValue,
				DeckHandler.Suits cardSuit, GameObject model)
	{
		this.cardID = cardID;
		this.cardValue = cardValue;
		this.cardSuit = cardSuit;
		this.model = model;
	}

	public int GetCardID()			{return cardID;}

	public int GetCardValue()		{return cardValue;}
}