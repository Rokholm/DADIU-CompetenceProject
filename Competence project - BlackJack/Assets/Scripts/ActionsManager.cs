using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoBehaviour {

	/*GameObject newGo, newGo2;
	public CardHandManager playerManager = new CardHandManager();
	public CardHandManager dealerManager = new CardHandManager();
	DrawCard drawCard;
	string activeHandManager;
	int timesCardHasBeenGiven = 4;
	private object newGO;

	private void Awake()
	{
		drawCard = GetComponent<DrawCard>();
	}

	private void Start()
	{
		CardHandManager playerManager = newGo.getComponent(CardHandManager);
		CardHandManager dealerManager = newGo2.getComponent(CardHandManager);
	}

	public void Hit(string playerName)
	{
		if (playerName == "player")
		{
			playerManager.AddCardToList(DeckHandler.Instance.cardData[DeckHandler.Instance.shuffledDeck[0]]);
			playerManager.AddCardToList(DeckHandler.Instance.cardData[DeckHandler.Instance.shuffledDeck[2]]);
			playerManager.AddCardToList(DeckHandler.Instance.cardData[DeckHandler.Instance.shuffledDeck[timesCardHasBeenGiven]]);
			Debug.Log("card should have been added to list");
			//playerManager.Score();
		}
		else
		{
			dealerManager.AddCardToList(DeckHandler.Instance.cardData[DeckHandler.Instance.shuffledDeck[1]]);
			dealerManager.AddCardToList(DeckHandler.Instance.cardData[DeckHandler.Instance.shuffledDeck[3]]);
			dealerManager.AddCardToList(DeckHandler.Instance.cardData[DeckHandler.Instance.shuffledDeck[timesCardHasBeenGiven]]);
			Debug.Log("card should have been added to list");
			//dealerManager.Score();
		}
		timesCardHasBeenGiven =+ 1;
	}

	public void Stay()
	{
		GameManager.Instance.GoToNextState();
	}

	bool SetActiveHandManager(string playerName)
	{
		if (playerName == "player")		{ return true;}
		else							{ return false;}
	}

	public CardHandManager GetCardHandManager(string playerName)
	{
		if (playerName == "player")		{return playerManager;}
		else							{return dealerManager;}	
	}

	public void DealerStateBehaviour()
	{
		if (dealerManager.Score() < 17)		{Hit("dealer");}
		else								{Stay();}
	}*/
}
