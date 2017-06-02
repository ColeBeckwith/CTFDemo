using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

	public float speed = 10.0F;
	public float jumpSpeed = 2260.0F;
	public string teamColor;


	public Slider flagCaptureSlider;

	private Rigidbody rb;
	private float jumpCooldown = 0f;
	private float startingJumpCooldown = 1f;

	public bool flagEquipped = false;
	public bool pickingUpFlag = false;
	private float flagPickupTime;
	private float flagPickupCountup;


	public string getTeam() {
		return teamColor;
	}


	public void beginFlagPickup(float countdown)
	{
		pickingUpFlag = true;
		flagPickupTime = countdown;
		flagPickupCountup = 0f;
		flagCaptureSlider.gameObject.SetActive (true);
	}

	public void stopFlagPickup()
	{
		flagPickupCountup = 0f;
		pickingUpFlag = false;
		flagCaptureSlider.gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		flagCaptureSlider.gameObject.SetActive (false);
		Cursor.lockState = CursorLockMode.Locked;
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		float translation = Input.GetAxis ("Vertical") * speed;
		float straffe = Input.GetAxis ("Horizontal") * speed;
		translation *= Time.deltaTime;
		straffe *= Time.deltaTime;

		jumpCooldown -= Time.deltaTime;
		jumpCooldown = Mathf.Clamp (jumpCooldown, 0f, startingJumpCooldown);

		transform.Translate (straffe, 0f, translation);

		if (Input.GetKeyDown ("escape"))
			Cursor.lockState = CursorLockMode.None;


		if (Input.GetAxis ("Jump") != 0 && jumpCooldown == 0) {
			// rb.AddForce (Vector3.up * jumpSpeed);
			jumpCooldown = startingJumpCooldown;
		}

		if (pickingUpFlag) 
		{
			flagPickupCountup += Time.deltaTime;
			flagCaptureSlider.value = flagPickupCountup / flagPickupTime;
			if (flagPickupCountup >= flagPickupTime) 
			{
				flagEquipped = true;
				stopFlagPickup ();
			}
		}
			
				

	}
}
