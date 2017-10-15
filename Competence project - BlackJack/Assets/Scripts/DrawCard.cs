using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DrawCard : MonoBehaviour {

	public Transform card;
	
	
	void Update () {
		if (Input.GetKeyUp(KeyCode.Space))
		{
			Instantiate(card, new Vector3(0, 0, -1), Quaternion.identity);
		}

	
	}
}
