using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton <GameManager> {

	private enum States{mainMenu, dealingCards, playerTurn, dealerTurn, playerSplit}
	private States currentState; 

	void Start ()
	{
		//currentState = States.mainMenu
	}

	void Update()
	{
		/*if(currentState == States.mainMenu) { **********;}
		else if (currentState == States.dealingCards) {DealingCards();}
		else if(currentState == States.playerTurn) { PlayerTurn();}
		else if(currentState == States.dealerTurn) { DealerTurn();}
		else if(currentState == States.playerSplit) { PlayerSplit;}*/
	}

	void MainMenu()
	{
		
	}

	void DealingCards()
	{
		
	}

	void PlayerTurn()
	{

	}

	void DealerTurn()
	{

	}

	void PlayerSplit()
	{

	}

	public void GoToNextState()
	{
		currentState += 1;
	}
}
