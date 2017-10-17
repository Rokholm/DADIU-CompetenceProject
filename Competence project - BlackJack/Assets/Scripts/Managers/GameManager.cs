using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private enum States{mainMenu, dealingCards, playerTurn, dealerTurn, playerSplit}
	private States currentState; 

	void Start ()
	{
		//currentState = States.mainMenu
	}

	void Update()
	{
		/*if(currentState == States.mainMenu) { **********;}
		else if (currentState == States.dealingCards) { **********;}
		else if(currentState == States.playerTurn) { **********;}
		else if(currentState == States.dealerTurn) { **********;}
		else if(currentState == States.playerSplit) { **********;}
		else if(currentState == States.mainMenu) { **********;}*/
	}

	/*void MainMenu()
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

	}*/
}
