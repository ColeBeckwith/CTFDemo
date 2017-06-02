using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagHolsterController : MonoBehaviour {
	public bool hasFlag;

	void Awake () {
		checkIfHasFlag ();
	}

	// Use this for initialization
	void Start () {
		// checkIfHasFlag ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool checkIfHasFlag() {
		for (int i = 0; i < transform.childCount; i++) {
			Transform child = transform.GetChild (i);

			if (child.tag == "Flag") {
				hasFlag = true;
				return hasFlag;
			}
		}
		hasFlag = false;
		return hasFlag;
	}
}
