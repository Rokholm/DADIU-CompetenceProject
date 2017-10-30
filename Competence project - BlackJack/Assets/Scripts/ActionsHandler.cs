using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsHandler : MonoBehaviour {
	DrawCard drawCard;
	public ListsAndScoreHandler listsAndScores;
	public int timesCardHasBeenGiven = 4;
																							
	private void Awake()
	{
		drawCard = GetComponent<DrawCard>();
		listsAndScores = GetComponent<ListsAndScoreHandler>();
	}

	public void Hit(bool player)
	{
		if (player == true)
		{
			listsAndScores.PlayerAddCardToList(drawCard.SpawnCard("player", 
											  timesCardHasBeenGiven));
			listsAndScores.PlayerScore();
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
		GameManager.Instance.GoToState(GameManager.States.dealerTurn);
	}

	public void EndDealerTurn()
	{
		GameManager.Instance.GoToState(GameManager.States.endGame);
	}
}
