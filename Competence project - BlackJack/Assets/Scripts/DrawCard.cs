using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawCard : MonoBehaviour {
	[SerializeField, Tooltip("Defines the card prefab that should be spawned")]
	public GameObject card;
	private Renderer rend;

	void Start()
	{
		rend = card.GetComponent<Renderer>();
	}

	void Update()
	{

		if (Input.GetKeyUp(KeyCode.Space))
		{
			//Debug.Log("Space key pressed");
			SpawnCard();
		}
	}

	void SpawnCard()
	{
		Instantiate(card, new Vector3(DisplaceCards(), 0.0f, 0.0f), transform.rotation);
	}

	float DisplaceCards()
	{
		//Vector3 center = rend.bounds.center;
		float radius = rend.bounds.extents.magnitude;
		float displaceAmount = +radius * 0.5f;

		return displaceAmount;

	}
}
