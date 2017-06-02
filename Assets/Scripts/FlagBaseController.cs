using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBaseController : MonoBehaviour {
	public string teamColor;

	public int flagsInBase;

	private List<GameObject> holsters = new List<GameObject>();

	// Use this for initialization
	void Start () {
		var holsterCount = 0;
		for (int i = 0; i < transform.childCount; i++) {
			Transform child = transform.GetChild (i);

			if (child.tag == "FlagHolster") {
				holsters.Add (child.gameObject);
			}
		}

		foreach (GameObject holster in holsters) 
		{
			if (holster.gameObject.GetComponent<FlagHolsterController> ().hasFlag) {
				flagsInBase++;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<CharacterController> ().getTeam () != teamColor && flagsInBase > 0) {
			other.gameObject.GetComponent<CharacterController> ().beginFlagPickup (3);
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Player") 
		{
			if (other.gameObject.GetComponent<CharacterController> ().getTeam () == teamColor) {
				// same team.
			} else {
				// other team.
				// Check here if they are ready to recieve the flag, or maybe that character controller should retrieve it.
				// if (other.gameObject.GetComponent<CharacterController> ())
			}
		}

	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<CharacterController> ().stopFlagPickup ();
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
