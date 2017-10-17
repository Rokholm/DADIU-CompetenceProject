using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private enum States{mainMenu, dealingCards, playerTurn, dealerTurn, playersplit}
	private States currentState; 

	void Start ()
	{
		//currentState = States.mainMenu;
	}
	
	void Update ()
	{
		
	}
}
