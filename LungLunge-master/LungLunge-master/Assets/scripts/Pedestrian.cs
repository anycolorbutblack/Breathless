using UnityEngine;
using System.Collections;

public class Pedestrian : MonoBehaviour {
	private ActionsNew actionController;
	private float Detectionrange;
	private float Runspeed = 3.0f;
	private Vector3 SpawnPosition = new Vector3(1,0,1);
	public Vector3 Destination = new Vector3(50,0,50);
	private float Heading;
	private Vector3 CurrentLocation;
	Rigidbody rb = null; 
	PlayerManager pm = null;
	public Transform Ped= null;
	public void Avoid(Vector3 otherped){
		// should use grid probably
		//needs animations
		Vector3 Heading = otherped - transform.position;
		Vector3 changer;
		if (Mathf.Abs(transform.position.x - Destination.x) > 1.0f)  {
			 float dir = Mathf.Sign (Heading.z);
			changer = new Vector3 (transform.position.x, transform.position.y, transform.position.z- dir * 1.0f);
		} else {
			float dir = Mathf.Sign (Heading.x);
			changer = new Vector3 (transform.position.x - dir * 1.0f, transform.position.y, transform.position.z);
		}
		transform.position = changer;
		//do animation
	}
	public Vector2 GetLocation(){
		return CurrentLocation;
	}
	public bool DestReached(){
		if (Mathf.Abs(transform.position.x - Destination.x) < 1.0f && Mathf.Abs(transform.position.z - Destination.z) < 1.0){
			return true;
		}
		return false;
	}
	public void UpdateMovement(){
		Vector3 Heading = Destination - transform.position;
		float Xdir = Mathf.Sign(Heading.x);
		float Zdir = Mathf.Sign(Heading.z);
		//needs to follow pathing (which doesn't exist at the moment.)
		if (Mathf.Abs(transform.position.x - Destination.x) > 1.0f) {
			transform.forward = Vector3.Normalize (new Vector3 (Xdir, 0f, 0f));
			this.rb.velocity = new Vector3 (Xdir * Runspeed, rb.velocity.y, rb.velocity.z);
		} else {
			transform.forward = Vector3.Normalize (new Vector3 (0f, 0f, Zdir));
			this.rb.velocity = new Vector3 ( rb.velocity.x, rb.velocity.y, Zdir * Runspeed);
		}

		//calc path
		//move toward dest

	}
	// Use this for initialization
	void Start () {
		actionController = GetComponentInChildren<ActionsNew> ();
		rb = GetComponent<Rigidbody>();
		pm = GetComponent<PlayerManager>();
		//set dest
		//set spawn pos
		SpawnPosition = new Vector3 (Random.Range (0f, 50f), 0f, Random.Range (0f, 50f));
		Destination = new Vector3 (Random.Range (0f, 50f), 0f, Random.Range (0f, 50f));

		if (Destination == SpawnPosition) {
			Start();
		}
		transform.position = SpawnPosition;	
	}

	// Update is called once per frame
	void Update () {
		actionController.Walk ();
		UpdateMovement();
		if (DestReached() ==true) {
			Start();
		}
		//call despawn

	}
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Floor")) {
		} else {
			Avoid(other.transform.position);
		}

	}
}
