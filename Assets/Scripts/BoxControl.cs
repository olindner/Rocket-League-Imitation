using UnityEngine;
using System.Collections;

public class BoxControl : MonoBehaviour {
	public Rigidbody rb;
	public float speedMult;
	public float rotMult;
	public Camera ballCam;
	public Camera mainCam;
	private float minBoost;
	public float boost;
	private float maxBoost;
	private float acceleration;
	public float maxSpeed;
	public float jumpMult;
	private Vector3 velocity;
	private float magnitude;
	private Vector3 f;
	private bool grounded;
	public float torqueMult;

	void Start () {
		ballCam.enabled = true;
		mainCam.enabled = false;
		minBoost = 0;
		maxBoost = 100;
		acceleration = 1;
		grounded = false;
	}

	void Update () {
		//Sense height
		if (rb.position.y <= 1.5) {
			grounded = true;
		}
		else {
			grounded = false;
		}

		//User Control
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.Rotate(Vector3.down * Time.deltaTime * rotMult);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.Rotate(Vector3.up * Time.deltaTime * rotMult);
		}
		if (Input.GetKey(KeyCode.UpArrow) && (grounded == true)) {
			rb.AddForce(transform.forward * speedMult * acceleration);
		}
		if (Input.GetKey(KeyCode.DownArrow) && (grounded == true)) {
			rb.AddForce(-transform.forward * speedMult);
		}

		//Camera Toggle
		if (Input.GetKeyDown (KeyCode.C)) {
			ballCam.enabled = !ballCam.enabled;
			mainCam.enabled = !mainCam.enabled;
		}

		//Boost
		if (((Input.GetKey (KeyCode.LeftControl)) || (Input.GetKey (KeyCode.RightControl))) && (Input.GetKey(KeyCode.UpArrow)) && (boost > minBoost)) {
			acceleration += 1;
			boost -= 1;
		} 
		else {
			if (acceleration > 2) {
				acceleration -= 1;
			}
			else {
				acceleration = 1;
			}
		}

		//Jump
		if (Input.GetKeyDown (KeyCode.Space) && (grounded == true)) {
			rb.AddForce(0,jumpMult, 0);
			grounded = false;
		}

		//Speed Check
		//velocity = rb.velocity;
		//magnitude = rb.velocity.magnitude;
		//if (magnitude > maxSpeed)
		//{
		//	rb.velocity = maxSpeed;
		//}

		//Breaking
		if ((Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) && (grounded == true)) {
			f = -(rb.mass * rb.velocity);
			rb.AddForce(f/15, ForceMode.Impulse);
		}

		//Frontflip
		if(Input.GetKeyDown(KeyCode.F)) {
			//mainCam.transform
			rb.AddForce(0,jumpMult,0);
			rb.AddTorque(transform.right * torqueMult);
		}
	}


	public void CollectedAPickup(GameObject pickup) {
		if (boost > 80) {
			boost = maxBoost;
		} 
		else {
			boost += 20;
		}
	}

	//void OnCollisionEnter(Collision col) {
	//	if (col.gameObject.tag == "Ground") {
	//		grounded = true;
	//	}
	//}

}
