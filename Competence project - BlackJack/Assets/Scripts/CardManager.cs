using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

	HandManager playerManager = new HandManager();
	HandManager dealerManager = new HandManager();
	string activeHandManager;
	int timesCardHasBeenGiven = 4;

	public void Hit()
	{
																				//
		switch (activeHandManager)
		{
			case "player":
				playerManager.AddCardToList(DeckHandler.Instance.cardData[DeckHandler.Instance.shuffledDeck[timesCardHasBeenGiven]]);
				playerManager.Score();
				break;
			case "dealer":
				dealerManager.AddCardToList(DeckHandler.Instance.cardData[DeckHandler.Instance.shuffledDeck[timesCardHasBeenGiven]]);
				dealerManager.Score();
				break;
		}

		timesCardHasBeenGiven = +1;
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

	/*public HandManager GetHandManager(string playerName)
	{
		switch (playerName)
		{
			case "player":
				return playerManager;
			case "dealer":
				return dealerManager;
		}
	}*/
}
