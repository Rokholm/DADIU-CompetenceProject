using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

	HandManager playerManager = new HandManager();
	HandManager dealerManager = new HandManager();
	string activeHandManager;

	void Hit()
	{
		switch (activeHandManager)
		{
			case "player":
				playerManager.AddCardToList();
				playerManager.Score();
				break;
			case "dealer":
				dealerManager.AddCardToList();
				dealerManager.Score();
				break;
		}

	}

	void Stay()
	{
		//go to next state.
	}

	void SetActiveHandManager(string playerName)
	{
		activeHandManager = playerName;
	}

	public HandManager GetHandManager(string playerName)
	{
		switch (playerName)
		{
			case "player":
				return playerManager;
			case "dealer":
				return dealerManager;
		}
	}
}
