using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBaseController : MonoBehaviour {
	public string teamColor;

	public int flagsInBase;

	private List<GameObject> holsters = new List<GameObject>();

	public void playerRetrieveFlag (GameObject player) 
	{
		if (flagsInBase == 0)
		{
			return;
		}
		var flagRetrieved = false;
		foreach (GameObject holster in holsters) 
		{
			if (!flagRetrieved && holster.gameObject.GetComponent<FlagHolsterController> ().hasFlag) 
			{
				holster.gameObject.GetComponent<FlagHolsterController> ().giveFlagToPlayer (player);
				flagRetrieved = true;
				flagsInBase--;
			}
		}
	}

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
		if (other.gameObject.tag == "Player") {
			Debug.Log (flagsInBase);
			var characterController = other.gameObject.GetComponent<CharacterController> ();
			if (characterController.getTeam () != teamColor) {
				if (flagsInBase == 0) {
					return;
				}
				characterController.beginFlagPickup (3);
				characterController.flagBase = transform.gameObject;
			} else if (characterController.flagEquipped) {
				placeFlagInOpenHolster (characterController.equippedFlag);
				characterController.flagEquipped = false;
				characterController.equippedFlag = null;
			}
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
			var characterController = other.gameObject.GetComponent<CharacterController> ();
			characterController.stopFlagPickup ();
			characterController.flagBase = null;
		}
	}

	// Update is called once per frame
	void Update () {

	}

	private void placeFlagInOpenHolster (GameObject flag)
	{
		var placed = false;
		foreach (GameObject holster in holsters) 
		{
			if (placed) {
				return;
			}
			var holsterController = holster.gameObject.GetComponent <FlagHolsterController> ();
			if (!holsterController.hasFlag) 
			{
				var flagController = flag.GetComponent <FlagController> ();
				flagController.returnFlagToHolster (holster);
				flagsInBase++;
				holsterController.hasFlag = true;
				holsterController.flag = flag;
				placed = true;
			}
		}
	}
}
