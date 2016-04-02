using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform player;
	public Vector3 startpos;
	// Use this for initialization
	void Start () {
		startpos = new Vector3 (0,15,0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position =  startpos + player.position ;
	}
}
