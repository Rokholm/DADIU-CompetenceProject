using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawCard : MonoBehaviour {

	
	[SerializeField]
	GameObject[] cardModels;
	[SerializeField, Tooltip("Defines the card prefab that should be spawned")]
	private Renderer rend;
	int i = 0;

	DeckHandler nextCardID;

	void Start()
	{
		nextCardID = GetComponent<DeckHandler>();
		
	}

	void Update()
	{
		
		if (Input.GetKeyUp(KeyCode.Space))
		{
			SpawnCard(i);
			i++;
		}
		//Debug.Log(i);
	}

	void SpawnCard(int timesCalled)
	{
		GameObject card = Instantiate(cardModels[(DeckHandler.shuffledDeck[timesCalled]) - 1], 
												  new Vector3(timesCalled, 0.0f, 0.0f), transform.rotation);
		card.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
		card.transform.Rotate(new Vector3(-90.0f, 0, 0));
		//Debug.Log(DeckHandler.emptyDeck[timesCalled]);
	}

	//float DisplaceCards()
	//{
	//	//Vector3 center = rend.bounds.center;
	//	float radius = rend.bounds.extents.magnitude;
	//	float displaceAmount =+ radius;

	//	return displaceAmount;
	//}

	//cardModels[DeckHandler.emptyDeck[0].id]
}
