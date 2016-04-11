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
	public void Avoid(Vector3 otherped){
		// should use grid probably
		//needs animations
		Vector3 Heading = otherped - transform.position;
		float dir = Mathf.Sign (Heading.z);
		Vector3 changer = new Vector3 (transform.position.x, transform.position.y, transform.position.z - dir* 1.5f);
		transform.position = changer;
		//do animation
	}
	public Vector2 GetLocation(){
		return CurrentLocation;
	}
	public bool DestReached(){
		if (CurrentLocation == Destination){
			return true;
		}
		return false;
	}
	public void UpdateMovement(){
		Vector3 Heading = Destination - transform.position;
		float Xdir = Mathf.Sign(Heading.x);
		float Zdir = Mathf.Sign(Heading.z);
		//needs to follow pathing (which doesn't exist at the moment.)
		if (transform.position.x <= Mathf.Abs(transform.position.x - Destination.x)) {
			this.rb.velocity = new Vector3 (Xdir * Runspeed, rb.velocity.y, rb.velocity.z);
			transform.rotation = new Quaternion (0, 90*Xdir, 0,1 );
		} else {
			this.rb.velocity = new Vector3 ( rb.velocity.x, rb.velocity.y, Zdir * Runspeed);
			transform.rotation = new Quaternion (0, -180*Zdir, 0,1 );
		}
	
		//calc path
		//move toward dest
				
	}
	// Use this for initialization
	void Start () {
		actionController = GetComponentInChildren<ActionsNew> ();
		rb = GetComponent<Rigidbody>();
		//set dest
		//set spawn pos
		if (Destination == SpawnPosition) {
			Start();
		}
		CurrentLocation = SpawnPosition;	
	}
	
	// Update is called once per frame
	void Update () {
		actionController.Walk ();
		UpdateMovement();
		if (DestReached() ==true) {
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
