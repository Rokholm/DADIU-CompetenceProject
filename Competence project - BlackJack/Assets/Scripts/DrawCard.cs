using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawCard : MonoBehaviour {
	
	[SerializeField]
	GameObject[] cardModels;
	[SerializeField, Tooltip("Defines the card prefab that should be spawned")]
	private Renderer rend;
	[SerializeField]
	Transform playerTransformPosition, dealerTransformPosition;
	Vector3 playerCardOffsetPosition, dealerCardOffsetPosition;
	int timesCalled = 0;

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			DealingStateSpawnCard();
			timesCalled++;
		}
	}

	public GameObject[] GetModels()
	{
		return cardModels;
	}

	void DealingStateSpawnCard()
	{
		GameObject card = Instantiate(cardModels[(DeckHandler.shuffledDeck[timesCalled])],
												  DealingPositionPlayer(), transform.rotation);
		card.transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
		card.transform.Rotate(new Vector3(-90.0f, 0, 0));
	}

	void SpawnCard()
	{
		GameObject card = Instantiate(cardModels[(DeckHandler.shuffledDeck[timesCalled])], 
												  new Vector3((0.0f), 0.0f, 0.0f), transform.rotation);
		card.transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
		card.transform.Rotate(new Vector3(-90.0f, 0, 0));
	}

	Vector3 DealingPositionPlayer()
	{

		if (timesCalled == 1 || timesCalled == 3)
		{
			playerCardOffsetPosition = playerTransformPosition.position + 
									   new Vector3(0.3f * timesCalled, 0.02f * timesCalled, 0.2f * timesCalled);
			Debug.Log(playerCardOffsetPosition);
			return playerCardOffsetPosition;
		}

		else
		{
			dealerCardOffsetPosition = dealerTransformPosition.position + 
									   new Vector3(0.3f * timesCalled, 0.02f * timesCalled, 0.2f * timesCalled);
			Debug.Log(dealerCardOffsetPosition);
			return dealerCardOffsetPosition;
		}
		 
	}

}
