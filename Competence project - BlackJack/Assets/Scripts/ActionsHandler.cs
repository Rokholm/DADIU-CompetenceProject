using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary:
// Class that defines the actions that are available for
//for the player and the dealer.

public class ActionsHandler : MonoBehaviour
{
	DrawCard drawCard;
	private ListsAndScoreHandler listsAndScores;
	public int timesCardHasBeenGiven = 4;
	public bool splitEnabled = false;

	private void Awake()
	{
		drawCard = GetComponent<DrawCard>();
		listsAndScores = GetComponent<ListsAndScoreHandler>();
	}

	public void Hit(bool player)
	{
		if (player == true && GameManager.Instance.currentState == 
			GameManager.States.playerTurn)
		{
			listsAndScores.PlayerAddCardToList(drawCard.SpawnCard("player",
											  timesCardHasBeenGiven));
			listsAndScores.PlayerScore(true);
			listsAndScores.CheckWinOrLose();
		}
		else if (player == true && GameManager.Instance.currentState == 
				 GameManager.States.playerSplit)
		{
			listsAndScores.PlayerAddCardToList(drawCard.SpawnCard("player",
											 timesCardHasBeenGiven));

			listsAndScores.PlayerScore(false);
			listsAndScores.CheckWinOrLose();
		}
		else
		{
			listsAndScores.DealerAddCardToList(drawCard.SpawnCard("dealer",
											  timesCardHasBeenGiven));
			listsAndScores.DealerScore();
			listsAndScores.CheckWinOrLose();
		}
		timesCardHasBeenGiven += 1;
	}

	public void PlayerStay()
	{
		if (GameManager.Instance.currentState == GameManager.States.playerTurn && splitEnabled == true)
		{
			GameManager.Instance.GoToState(GameManager.States.playerSplit);
		}
		else if (GameManager.Instance.currentState == GameManager.States.playerTurn)
		{
			GameManager.Instance.GoToState(GameManager.States.dealerTurn);
		}
		else if (GameManager.Instance.currentState == GameManager.States.playerSplit)
		{
			GameManager.Instance.GoToState(GameManager.States.dealerTurn);
		}
	}

	public void EndDealerTurn()
	{
		GameManager.Instance.GoToState(GameManager.States.endOfHand);
	}

	public void PlayerSplitHand()
	{
		splitEnabled = true; 
	}
}
