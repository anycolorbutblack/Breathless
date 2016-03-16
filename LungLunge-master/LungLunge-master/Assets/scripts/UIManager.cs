using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GameObject PausePanel; //using the game panel/pause menu

	public bool isPaused; //To know when game is paused or not

	void Start () {
		isPaused = false; //initializing as not paused on first frame
	}

	void Update () { //This is the manual control for pausing the game or not
		if (isPaused) { //This will open or close the pause menu
			PauseGame (true); //show pause menu
		} else {
			PauseGame (false);
		}

		if (Input.GetButtonDown("Cancel")) { //This here detects the escape key to open/close the pause menu
			switchPause();
		}
	}

	void PauseGame(bool state){ //pause the game or unpause it
        //print("Pause Game");
		if (state) {
			PausePanel.SetActive (true);
			Time.timeScale = 0.0f;
		} else {
			Time.timeScale = 1.0f;
			PausePanel.SetActive (false);
		}
	}

	public void switchPause(){ //Switch between pause or not
        print("SwitchPause");
		if (isPaused) {
			isPaused = false;
		} else {
			isPaused = true;
		}
	}

	public void ReturnMenu()
	{
        print("ReturnMenu");
		//SceneManager.LoadScene (Menu_Scene);
		Application.LoadLevel (0); //Return to main menu, level 0
	}
}
