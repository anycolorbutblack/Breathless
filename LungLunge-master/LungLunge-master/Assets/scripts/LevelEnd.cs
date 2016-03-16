using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {

	PlayerManager pm;
    // Use this for initialization

   //if exit condition is met
    void OnTriggerEnter(Collider other)
    {
        //check name of collider
        if (other.gameObject.name == "Player")
			pm = GameObject.Find("Player").GetComponent<PlayerManager>();
			PlayerPrefs.SetInt ("LevelScore", pm.getscore());
			PlayerPrefs.SetInt ("LevelTime", pm.gettime());
			PlayerPrefs.SetInt ("PufferCharge", (int)pm.getPufferCharge());
			PlayerPrefs.Save();
            print("Got level end");
            //dummy scene
            Application.LoadLevel("endscene");
    }
	
}
