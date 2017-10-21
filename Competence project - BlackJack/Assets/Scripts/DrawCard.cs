using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawCard : MonoBehaviour {

	[SerializeField]
	GameObject[] cardModels;
	[SerializeField]
	Transform playerTransformPosition, dealerTransformPosition;
	Vector3 playerCardOffset, dealerCardOffset;
	int timesCalled = 0;

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space) && timesCalled < 4)
		{
			DealingStateSpawnCard();
			timesCalled++;
		}
	}

	public GameObject[] GetModels()
	{
		return cardModels;
	}
																						//																		
	public void DealingStateSpawnCard()
	{
		GameObject card = Instantiate(DeckHandler.Instance.cardData[DeckHandler.
									  Instance.shuffledDeck[timesCalled]].model,
									  DealingCardPositions(), transform.rotation);
		card.transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
		card.transform.Rotate(new Vector3(CardFaceUpOrFaceDown(), 0, 0));
	}

	void SpawnCard()
	{
		
		GameObject card = Instantiate(DeckHandler.Instance.cardData[DeckHandler.
									  Instance.shuffledDeck[timesCalled]].model,
									  new Vector3((0.0f), 0.0f, 0.0f), transform.rotation);
		card.transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
		card.transform.Rotate(new Vector3(-90.0f, 0, 0));
	}

	Vector3 DealingCardPositions()
	{
		if (timesCalled == 0 || timesCalled == 2)
		{
			playerCardOffset = playerTransformPosition.position +
							   new Vector3(0.3f * timesCalled, 0.02f, 0.2f);
			Debug.Log(playerCardOffset);
			return playerCardOffset;
		}
		else
		{
			dealerCardOffset = dealerTransformPosition.position +
							   new Vector3(0.3f * timesCalled, 0.02f, 0.2f);
			Debug.Log(dealerCardOffset);
			return dealerCardOffset;
		}
	}

	float CardFaceUpOrFaceDown()
	{
		if (timesCalled == 3)		{return 90.0f;}
		else						{return -90.0f;}
	}

}
