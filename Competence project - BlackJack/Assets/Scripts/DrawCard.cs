using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawCard : MonoBehaviour {

	[SerializeField]
	GameObject[] cardModels;
	[SerializeField]
	private Transform playerTransformPosition, dealerTransformPosition;
	public GameObject cardParent;
	private GameObject cardFaceDown;
	private Vector3 playerCardOffset, dealerCardOffset;

	IEnumerator SpawnDelay()
	{
		yield return new WaitForSeconds(2);
	}
																																								
	public GameObject[] GetModels()
	{
		return cardModels;
	}
																		
	public IEnumerator DealingStateSpawnCard(int cardsDealt)
	{
		while (cardsDealt < 4)
		{
			GameObject card = Instantiate(DeckHandler.Instance.cardData[DeckHandler.
										  Instance.shuffledDeck[cardsDealt]-1].model,
									      DealingCardPositions(cardsDealt), 
									      transform.rotation);
			card.transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
			card.transform.Rotate(new Vector3(CardFaceUpOrFaceDown(cardsDealt),
								  0, 0));
			card.transform.parent = cardParent.transform;
			card.AddComponent<Rigidbody>();
			BoxCollider tempCollider = card.AddComponent<BoxCollider>();
			tempCollider.size = new Vector3(tempCollider.size.x, 
								tempCollider.size.y, 0.01f);
			cardsDealt++;

			if (cardsDealt == 4)
			{
				cardFaceDown = card;
			}
			yield return new WaitForSeconds(1);
		}
	}
	public Card SpawnCard(string playerName, int cardsDealt)
	{
		if (playerName == "player")
		{
			GameObject card = Instantiate(DeckHandler.Instance.cardData[DeckHandler.
										  Instance.shuffledDeck[cardsDealt]-1].model,
										  playerCardOffset, transform.rotation);
			card.transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
			card.transform.Rotate(new Vector3(-90.0f, 0, 90.0f));
			card.transform.parent = cardParent.transform;
			card.AddComponent<Rigidbody>();
			BoxCollider tempCollider = card.AddComponent<BoxCollider>();
			tempCollider.size = new Vector3(tempCollider.size.x, 
								tempCollider.size.y, 0.01f);
			Card cardToReturn = DeckHandler.Instance.cardData
								[DeckHandler.Instance.shuffledDeck[cardsDealt] - 1];
			cardsDealt++;
			return cardToReturn;
		}
		else
		{
			GameObject card = Instantiate(DeckHandler.Instance.cardData[DeckHandler.
										  Instance.shuffledDeck[cardsDealt]-1].model,
										  dealerCardOffset, transform.rotation);
			card.transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
			card.transform.Rotate(new Vector3(-90.0f, 0, 90.0f));
			card.transform.parent = cardParent.transform;
			card.AddComponent<Rigidbody>();
			BoxCollider tempCollider = card.AddComponent<BoxCollider>();
			tempCollider.size = new Vector3(tempCollider.size.x, 
								tempCollider.size.y, 0.01f);
			Card cardToReturn = DeckHandler.Instance.cardData
								[DeckHandler.Instance.shuffledDeck[cardsDealt] - 1];
			cardsDealt++;
			return cardToReturn;
		}
	}

	Vector3 DealingCardPositions(int cardsDealt)
	{
		if (cardsDealt == 0 || cardsDealt == 2)
		{
			playerCardOffset = playerTransformPosition.position +
							   new Vector3(0.3f * cardsDealt, 
							   0.02f * cardsDealt, 0.2f);
			return playerCardOffset;
		}
		else
		{
			dealerCardOffset = dealerTransformPosition.position +
							   new Vector3(0.3f * (cardsDealt - 1),
							   0.02f * cardsDealt, 0.2f);
			return dealerCardOffset;
		}
	}

	float CardFaceUpOrFaceDown(int cardsDealt)
	{
		if (cardsDealt == 3)
		{
			return 90.0f;
		}
		else
		{
			return -90.0f;
		}
	}

	public void RevealDealerCard()
	{
		cardFaceDown.transform.Rotate(new Vector3(180.0f, 0.0f, 0.0f));
	}

	public void ResetCardSpawns()
	{
		foreach (Transform child in cardParent.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
	}
}
