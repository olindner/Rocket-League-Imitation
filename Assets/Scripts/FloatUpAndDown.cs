using UnityEngine;
using System.Collections;

public class FloatUpAndDown : MonoBehaviour {
	private float origY;
	public float speed;
	public float distance;
	public bool spin;
	public float spinSpeed;
	private float timer;
	public int timeUntilRespawn;
	public GameObject go;

	void Start () {
		origY = transform.position.y;
	}

	void Update ()
	{
		if (!GetComponent<Renderer>().enabled) {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				timer = 0;
				GetComponent<Renderer>().enabled = true;
				GetComponent<Collider>().enabled = true;
			}
		}
		//Hover up and down
		if(Mathf.Abs(transform.position.y - origY) > distance) {
			speed = -speed; //flip direction
		}

		transform.Translate(0,speed*Time.deltaTime,0);

		//Rotation element
		if (spin) {
			transform.Rotate(Vector3.up * Time.deltaTime * spinSpeed);
		}
	}

	void OnTriggerEnter (Collider coll) {
		if (coll.tag == "Cube") {
			go.GetComponent<BoxControl> ().CollectedAPickup (gameObject);
			GetComponent<Renderer> ().enabled = false;
			GetComponent<Collider> ().enabled = false;
			timer = timeUntilRespawn;
		}
	}
}
