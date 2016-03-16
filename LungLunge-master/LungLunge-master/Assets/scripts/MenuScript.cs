using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour
{

	// initialization of what is 'usable' on the menu
	public Canvas quitMenu; //The submenu to confirm if the user wants to leave the game
	public Button StartText; //The Play button to start a new game, will become a submenu eventually
	public Button ExitText; //The actual exit button which opens the submenu mentionned above

	void Start ()
	{
		//Initialization of the buttons on the menu
		quitMenu = quitMenu.GetComponent<Canvas> ();
		StartText = StartText.GetComponent<Button> ();
		ExitText = ExitText.GetComponent<Button> ();
		quitMenu.enabled = false; //wont quit by itself
	}

	public void ExitPress() //To exit the game, menu, disables other buttons
	{
		quitMenu.enabled = true; //The submenu appears on screen and ready to be used
		StartText.enabled = false; //Disables the new game button while exit submenu is open
		ExitText.enabled = false; //Disables the exit button so there won't be many isntances of the submenu
	}

	public void NoPress() //enables other options when not in exit menu
	{
		quitMenu.enabled = false; //submenu disappears
		StartText.enabled = true; //allows the user to start a new game
		ExitText.enabled = true; //allows the user to reopen the submenu if desired
	}

	public void StartLevel()
	{
		Application.LoadLevel (1); //Insert level number here in the brackets, please note the number will vary based on builds
	}

	public void ExitGame()
	{
		Application.Quit (); //Close the application
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
