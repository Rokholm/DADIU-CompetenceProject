using System.Collections;
using System.Collections.Generic;
using UnityEngine;
																				
//Summary:
//The class responsible for spawning/destroying and manipulating
//the 3D models of the cards.

public class DrawCard : MonoBehaviour
{																				
	[SerializeField]
	private GameObject[] cardModels;
	[SerializeField]
	private Transform playerTransformPosition, playerSplitTransformPos;
	[SerializeField]
	private Transform dealerTransformPosition;
	public GameObject cardParent;
	private GameObject cardFaceDown, splitCardObject;
	private Transform splitCardTransform;
	private Vector3 playerCardOffset, dealerCardOffset;
	private Vector3 splitCardOriginPos;
	private float lerpTime = 0.0f;

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
										  Instance.shuffledDeck[cardsDealt] - 1].model,
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
			if (GameManager.Instance.currentState == GameManager.States.playerSplit)
			{
				playerCardOffset = playerSplitTransformPos.position + new Vector3(0.3f,
							   0.02f, 0.2f);
			}
			GameObject card = Instantiate(DeckHandler.Instance.cardData[DeckHandler.
										  Instance.shuffledDeck[cardsDealt] - 1].model,
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
										  Instance.shuffledDeck[cardsDealt] - 1].model,
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

	public void SplitMoveCardsApart()
	{
		//Debug.Log("begins lerp");
		if (lerpTime >= 1)
		{
			return;
		}
		else if (lerpTime == 0)
		{
			splitCardTransform = cardParent.transform.GetChild(2);
			splitCardOriginPos = splitCardTransform.transform.position;
			splitCardObject = splitCardTransform.transform.gameObject;
			CardSplitLerp();
		}
		else
		{
			CardSplitLerp();
		}
		
	}

	private void CardSplitLerp()
	{
		//Debug.Log(lerpTime);
		lerpTime += Time.deltaTime;
		splitCardObject.transform.position = Vector3.Lerp(splitCardOriginPos,
													 playerSplitTransformPos.position, lerpTime);
	}

	private void ResetLerp()
	{
		if (GameManager.Instance.currentState == GameManager.States.resetTable)
		{
			lerpTime = 0; 
		}
	}
}
