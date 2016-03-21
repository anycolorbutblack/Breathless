using UnityEngine;
using System.Collections;

public class Pedestrian : MonoBehaviour {
	private float Detectionrange;
	private float Runspeed;
	private float SpawnPosition;
	private float Destination;
	private float Heading;
	private float CurrentLocation;

	public void Avoid(float otherped){
		//avoids
	}
	public void GetLocation(){
		return CurrentLocation;
	}
	public bool DestReached(){
		if (CurrentLocation == Destination){
 			//call despawn
		}
	}
	public void UpdateMovement(){
		//calc path
		//move toward dest
				
	}
	// Use this for initialization
	void Start () {
		//set dest
		//set spawn pos
		if (Destination == SpawnPosition) {
			Start();
		}
		CurrentLocation == SpawnPosition;	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMovement();
	}
}
