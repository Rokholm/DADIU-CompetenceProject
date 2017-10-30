using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameManager : Singleton <GameManager> {
		
	public enum States
	{
		dealingCards,
		playerTurn,
		dealerTurn,
		endGame,
		resetTable,
		playerSplit
	}

	[SerializeField]
	GameObject actionsCanvas, endGameCanvas;
	[SerializeField]
	public States currentState;
	DrawCard drawCard;
	BettingController bettingController;
	ActionsHandler actionsHandler;
	ListsAndScoreHandler listsAndScores;
	int numberOfCardsDealt;

	void Awake()
	{
		drawCard = GetComponent<DrawCard>();
		listsAndScores = GetComponent<ListsAndScoreHandler>();
		actionsHandler = GetComponent<ActionsHandler>();
	}
																					
	void Start ()
	{
		currentState = States.dealingCards;
		GoToState(currentState);
	}

	void DealingCards()
	{
		endGameCanvas.SetActive(false);
		numberOfCardsDealt = 0;
		DeckHandler.Instance.ShuffleDeck();
		StartCoroutine(drawCard.DealingStateSpawnCard(numberOfCardsDealt));
		listsAndScores.DealingStateCardsToList();
		GoToState(States.playerTurn);
	}

	void PlayerTurn()
	{
		Debug.Log("you are now in the PlayerTurn State");
		actionsCanvas.SetActive(true);
	}

	void DealerTurn()
	{
		Debug.Log("you are now in the DealerTurn State");
		actionsCanvas.SetActive(false);
		drawCard.RevealDealerCard();
		StartCoroutine(listsAndScores.DealerStateBehaviour());
	}

	void EndGame()
	{
		Debug.Log("you are now in the EndGame State");
		endGameCanvas.SetActive(true);
	}

	void ResetTable()
	{
		drawCard.ResetCardSpawns();
		listsAndScores.ResetLists(true);
		listsAndScores.ResetLists(false);
		endGameCanvas.SetActive(false);
		listsAndScores.TestIfListsEmpty();
		actionsHandler.timesCardHasBeenGiven = 4;
		GoToState(States.dealingCards);
	}

	public void GoToState(States StateToGoTo)
	{
		//this could have been made into a Switch
		currentState = StateToGoTo;
		if (currentState == States.dealingCards)
		{
			DealingCards();
		}
		else if (currentState == States.playerTurn)
		{
			PlayerTurn();
		}
		else if (currentState == States.dealerTurn)
		{
			DealerTurn();
		}
		else if (currentState == States.endGame)
		{
			EndGame();
		}
		else if (currentState == States.resetTable)
		{
			ResetTable();
		}
	}

	public void PlayAgain()
	{
		GoToState(States.resetTable);
	}
}
