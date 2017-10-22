using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton <GameManager> {
	//				
	public enum States	{mainMenu, dealingCards, playerTurn,
						dealerTurn, endGame, playerSplit}
	
	[SerializeField]
	public States currentState;
	DrawCard drawCard;
	OptionsManager optionsManager; 
	//ActionsManager actionsManager;
	//ActionsManager getCardHandmanager;
	[SerializeField]
	GameObject actionsCanvas;
	int numberCardDealt;

	void Awake()
	{
		drawCard = GetComponent<DrawCard>();
		optionsManager = GetComponent<OptionsManager>();
		//actionsManager = GetComponent<ActionsManager>();
		//getCardHandmanager = GetComponent<ActionsManager>();
	}

	void Start ()
	{
		//currentState = States.mainMenu
	}

	void Update()
	{
		if(currentState == States.mainMenu) {}
		else if(currentState == States.dealingCards) {DealingCards();}
		else if(currentState == States.playerTurn) {PlayerTurn();}
		else if(currentState == States.dealerTurn) {DealerTurn();}
		else if(currentState == States.playerSplit) {PlayerSplit();}
		else if(currentState == States.endGame) {EndGame();}
	}

	void MainMenu(string menuAction)
	{
		Debug.Log("you are now in the main menu");
		if (menuAction == "play")
		{
			currentState = States.dealingCards;
		}
	}

	void DealingCards()
	{
		numberCardDealt = 0;
		Debug.Log("you are now in the DealingCards State");
		DeckHandler.Instance.ShuffleDeck();
		drawCard.DealingStateSpawnCard(numberCardDealt);
		optionsManager.DealingStateCardsToList();
		currentState = States.playerTurn;
	}

	void PlayerTurn()
	{
		numberCardDealt = 4;
		Debug.Log("you are now in the PlayerTurn State");
		actionsCanvas.SetActive(true);
		if (optionsManager.CheckWinOrLoseCondition() == 2)
		{
			currentState = States.endGame;
		}
	}

	void DealerTurn()
	{
		Debug.Log("you are now in the DealerTurn State");
		actionsCanvas.SetActive(false);
		optionsManager.DealerStateBehaviour();
	}

	void PlayerSplit()
	{
		Debug.Log("you are now in the PlayerSplit State");
		currentState = States.dealerTurn;
	}

	void EndGame()
	{
		Debug.Log("you are now in the EndGame State");
		
	}

	public void GoToNextState()
	{
		currentState = currentState + 1;
	}
}
