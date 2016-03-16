using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PufferPickup : MonoBehaviour {

	public float pufferRecharge;
	public int pufferscore;
	private ConfigManager cm;
	private float yCenter;
	private float yMove;
	private float bobRange = 0.4f;
	private float bobSpeed = 0.7f;
    PlayerManager pm;
	// Use this for initialization
	void Start () {
		cm = GetComponent<ConfigManager>();
		yCenter = transform.position.y;
		yMove = 0f;
        pm = GameObject.Find("Player").GetComponent<PlayerManager>();

		pufferRecharge = (float)Double.Parse(cm.Load ("pufferRecharge"));
		pufferscore   = Int32.Parse(cm.Load ("pufferscore"));

	}
	
	// Update is called once per frame
	void Update () {
		if (Math.Abs (yMove) > bobRange) {
			bobSpeed = -bobSpeed;
			yMove = Math.Sign (yMove) * bobRange;
		}
		yMove += Time.deltaTime * bobSpeed;
		transform.position = new Vector3 (transform.position.x, yCenter + yMove, transform.position.z);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag ("Player"))
		{
			pm.addscore(pufferscore);
			pm.addPufferCharge(pufferRecharge);
			Destroy(gameObject); //destroy this capsule
		}
	}
}
