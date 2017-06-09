using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour {

	public Collider[] colliders;

	public void makePlayerNewParent(GameObject player)
	{
		var playerController = player.GetComponent <CharacterController> ();
		foreach (Collider collider in colliders) 
		{
			collider.enabled = false;
		}

		transform.SetParent (player.transform);
		transform.localPosition = new Vector3 (playerController.flagPosX, 0, playerController.flagPosZ);
		transform.localRotation = Quaternion.Euler (0, 0, playerController.flagRotateZ);
		playerController.equippedFlag = transform.gameObject;
	}

	public void returnFlagToHolster(GameObject holster) 
	{
		transform.SetParent (holster.transform);
		transform.localPosition = new Vector3 (0, 0, 0);
		transform.localRotation = Quaternion.Euler (0, 0, 0);
		var holsterScale = holster.transform.localScale;
		transform.localScale = new Vector3 (1 / holsterScale.x, 1 / holsterScale.y, 1 / holsterScale.z);

		foreach (Collider collider in colliders) 
		{
			collider.enabled = true;
		}
	}

	// Use this for initialization
	void Start () {
		// transform.SetParent (transform.parent.transform);
		var holsterScale = transform.parent.transform.localScale;
		transform.localScale = new Vector3 (1 / holsterScale.x, 1 / holsterScale.y, 1 / holsterScale.z);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
