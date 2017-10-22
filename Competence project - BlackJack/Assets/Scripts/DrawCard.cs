using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawCard : MonoBehaviour {

	[SerializeField]
	GameObject[] cardModels;
	[SerializeField]
	Transform playerTransformPosition, dealerTransformPosition;
	Vector3 playerCardOffset, dealerCardOffset;

	void Update()
	{
		
	}

	public GameObject[] GetModels()
	{
		return cardModels;
	}
																		
	public void DealingStateSpawnCard(int cardsDealt)
	{
		while (cardsDealt < 4)
		{
			GameObject card = Instantiate(DeckHandler.Instance.cardData[DeckHandler.
									  Instance.shuffledDeck[cardsDealt]].model,
									  DealingCardPositions(cardsDealt), transform.rotation);
			card.transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
			card.transform.Rotate(new Vector3(CardFaceUpOrFaceDown(cardsDealt), 0, 0));

			cardsDealt++;
		}
	}
	public void SpawnCard(string playerName, int cardsDealt)
	{
		if (playerName == "player")
		{
			GameObject card = Instantiate(DeckHandler.Instance.cardData[DeckHandler.
										  Instance.shuffledDeck[cardsDealt]].model,
										  playerCardOffset, transform.rotation);
			card.transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
			card.transform.Rotate(new Vector3(-90.0f, 0, 90.0f));
			cardsDealt++;
		}
		else
		{
			GameObject card = Instantiate(DeckHandler.Instance.cardData[DeckHandler.
										  Instance.shuffledDeck[cardsDealt]].model,
										  dealerCardOffset, transform.rotation);
			card.transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
			card.transform.Rotate(new Vector3(-90.0f, 0, 90.0f));
			cardsDealt++;
		}
	}

	Vector3 DealingCardPositions(int cardsDealt)
	{
		if (cardsDealt == 0 || cardsDealt == 2)
		{
			playerCardOffset = playerTransformPosition.position +
							   new Vector3(0.3f * cardsDealt, 0.02f, 0.2f);
			//Debug.Log(playerCardOffset);
			return playerCardOffset;
		}
		else
		{
			dealerCardOffset = dealerTransformPosition.position +
							   new Vector3(0.3f * (cardsDealt - 1), 0.02f, 0.2f);
			//Debug.Log(dealerCardOffset);
			return dealerCardOffset;
		}
	}

	float CardFaceUpOrFaceDown(int cardsDealt)
	{
		if (cardsDealt == 3)		{return 90.0f;}
		else						{return -90.0f;}
	}

}
