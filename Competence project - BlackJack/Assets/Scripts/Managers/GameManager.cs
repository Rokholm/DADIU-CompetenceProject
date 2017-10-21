using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton <GameManager> {
																				//				
	public enum States{mainMenu, dealingCards, playerTurn,
					   dealerTurn, playerSplit, endGame,}
	public States currentState;
	DrawCard DrawCard;
	[SerializeField]
	GameObject actionsCanvas;

	void Awake()
	{
		DrawCard = GetComponent<DrawCard>();
	}

	void Start ()
	{
		//currentState = States.mainMenu
	}

	void Update()
	{
		/*if(currentState == States.mainMenu) { **********;}
		else if(currentState == States.dealingCards) {DealingCards();}
		else if(currentState == States.playerTurn) {PlayerTurn();}
		else if(currentState == States.dealerTurn) {DealerTurn();}
		else if(currentState == States.playerSplit) {PlayerSplit();}
		else if(currentState == States.endGame) {EndGame();}*/
	}

	void MainMenu()
	{
		
	}

	void DealingCards()
	{
		DeckHandler.Instance.ShuffleDeck();
		DrawCard.DealingStateSpawnCard();
		currentState = States.playerTurn;
	}

	void PlayerTurn()
	{
		actionsCanvas.SetActive(true);
		currentState = States.dealerTurn;
	}

	void DealerTurn()
	{
		actionsCanvas.SetActive(false);
		currentState = States.endGame;
	}

	void PlayerSplit()
	{
		currentState = States.dealerTurn;
	}

	void EndGame()
	{
		currentState = States.dealingCards;
	}

	public void GoToNextState()
	{
		currentState = currentState + 1;
	}
}
