using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameManager : Singleton <GameManager> {
		
	public enum States
	{
		bettingPhase,
		dealingCards,
		playerTurn,
		dealerTurn,
		endGame,
		resetTable,
		playerSplit
	}

	[SerializeField]
	GameObject actionsCanvas, endGameCanvas;
	public States currentState;
	DrawCard drawCard;
	BettingController bettingController;
	ActionsHandler actionsHandler;
	ListsAndScoreHandler listsAndScores;
	int numberOfCardsDealt;

	void Awake()
	{
		drawCard = GetComponent<DrawCard>();
		bettingController = GetComponent<BettingController>();
		listsAndScores = GetComponent<ListsAndScoreHandler>();
		actionsHandler = GetComponent<ActionsHandler>();
	}
																					
	void Start ()
	{
		currentState = States.bettingPhase;
		GoToState(currentState);
	}

	void BettingPhase()
	{
		bettingController.Update();
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
		GoToState(States.bettingPhase);
	}

	public void GoToState(States StateToGoTo)
	{
		currentState = StateToGoTo;

		switch (StateToGoTo)
		{
			case States.bettingPhase:
				BettingPhase();
				break;

			case States.dealingCards:
				DealingCards();
				break;

			case States.playerTurn:
				PlayerTurn();
				break;

			case States.dealerTurn:
				DealerTurn();
				break;

			case States.endGame:
				EndGame();
				break;

			case States.resetTable:
				ResetTable();
				break;

			case States.playerSplit:
				//PlayerSplit();
				break;

			default:
				Debug.Assert(false, "A non-valid State was called");
				break;
		}
	}

	public void PlayAgain()
	{
		GoToState(States.resetTable);
	}
}
