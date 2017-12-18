using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Summary:
//The Gamemanager controls the flow of the game through state changing
//and calling functions that relate to the individual states in the game. 

public class GameManager : Singleton<GameManager>
{

	public enum States
	{
		bettingPhase,
		dealingCards,
		playerTurn,
		dealerTurn,
		endOfHand,
		resetTable,
		playerSplit,
		gameOver
	}

	[SerializeField]
	GameObject actionsCanvas, endOfHandCanvas, gameOverCanvas;
	[SerializeField]
	Text gameOverText;
	Button uiSplitHand;
	DrawCard drawCard;
	BettingController bettingController;
	ActionsHandler actionsHandler;
	ListsAndScoreHandler listsAndScores;
	int numberOfCardsDealt;
	[HideInInspector]
	public string winType;
	public States currentState;
	bool splitEnabled, hasSplit = false;

	void Awake()
	{
		drawCard = GetComponent<DrawCard>();
		bettingController = GetComponent<BettingController>();
		listsAndScores = GetComponent<ListsAndScoreHandler>();
		actionsHandler = GetComponent<ActionsHandler>();
	}

	void Start()
	{
		currentState = States.bettingPhase;
		GoToState(currentState);
	}

	private void Update()
	{
		if (currentState == States.playerTurn)
		{
			//listsAndScores.CheckForSplit();
			splitEnabled = actionsHandler.splitEnabled;
			if (splitEnabled == true)
			{
				if (hasSplit == true)
				{
					drawCard.SplitMoveCardsApart();
					Debug.Log("Move cards apart called");
				}
				else
				{
					listsAndScores.TransferCardIntoSecondHand();
					uiSplitHand = listsAndScores.uiSplitHand;
					uiSplitHand.enabled = true;
					Debug.Log("Transfer Cards into list called");
					hasSplit = true;
					
				}
			}
		}
	}

	void DealingCards()
	{
		endOfHandCanvas.SetActive(false);
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
		endOfHandCanvas.SetActive(true);
		winType = listsAndScores.winType;
		print(winType);
		bettingController.AddReturnsToFunds(winType);
	}

	void ResetTable()
	{
		drawCard.ResetCardSpawns();
		listsAndScores.ResetLists(true);
		listsAndScores.ResetLists(false);
		listsAndScores.SplitCardsInSecondHand.Clear();
		endOfHandCanvas.SetActive(false);
		listsAndScores.TestIfListsEmpty();
		actionsHandler.timesCardHasBeenGiven = 4;
		actionsHandler.splitEnabled = false;
		hasSplit = false;
		GoToState(States.bettingPhase);

	}

	void GameOver()
	{
		gameOverCanvas.SetActive(true);
		gameOverText.text = "Looks like you ran out of money";
	}

	public void GoToState(States StateToGoTo)
	{
		currentState = StateToGoTo;

		switch (StateToGoTo)
		{
			case States.bettingPhase:
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

			case States.endOfHand:
				EndGame();
				break;

			case States.resetTable:
				ResetTable();
				break;

			case States.playerSplit:
				break;

			case States.gameOver:
				GameOver();
				break;

			default:
				Debug.Assert(false, "A non-valid State was called");
				break;
		}
	}

	private void CheckIfSplitCorrect()
	{

	}

	public void PlayNextHand()
	{
		GoToState(States.resetTable);
	}
}
