using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelpMenuScene : MonoBehaviour
{
    public Button Controls;
    public Button Back;
    public Button BreathMeter;
    public Button PufferCharge;
    public Button UserManual;

    public Canvas ControlsScreen;
    public Canvas BreathMeterScreen;
    public Canvas PufferChargeScreen;
    public Canvas UserManualScreen;
    void Start()
    {
        ControlsScreen = ControlsScreen.GetComponent<Canvas>();
        BreathMeterScreen = BreathMeterScreen.GetComponent<Canvas>();
        PufferChargeScreen = PufferChargeScreen.GetComponent<Canvas>();
        UserManualScreen = UserManualScreen.GetComponent<Canvas>();

        Controls = Controls.GetComponent<Button>();
        BreathMeter = BreathMeter.GetComponent<Button>();
        PufferCharge = PufferCharge.GetComponent<Button>();
        UserManual = UserManual.GetComponent<Button>();

        Back.enabled = false;
    }
    public void MainHelp()
    {
        Back.enabled = true;
        BreathMeter.enabled = true;
        Controls.enabled = true;
        PufferCharge.enabled = true;
        UserManual.enabled = true;
    }
    public void ControlPress()
    {
        Controls.enabled = true;
        Back.enabled = true;
        BreathMeter.enabled = false;
        PufferCharge.enabled = false;
        UserManual.enabled = false;
    }
    public void BreathMeterPress()
    {
        Back.enabled = true;
        BreathMeter.enabled = true;
        Controls.enabled = false;
        PufferCharge.enabled = false;
        UserManual.enabled = false;
    }
    public void PufferChargePress()
    {
        Back.enabled = true;
        PufferCharge.enabled = true;
        BreathMeter.enabled = false;
        Controls.enabled = false;
        UserManual.enabled = false;
    }
    public void UserManualPress()
    {
        Back.enabled = true;
        BreathMeter.enabled = false;
        Controls.enabled = false;
        PufferCharge.enabled = false;
        UserManual.enabled = true;
    }



    public void loadScene(int scene)
    {
        Application.LoadLevel(scene); //Insert level number here in the brackets, please note the number will vary based on builds
    }


    // Update is called once per frame
    void Update()
    {

    }
}
//http://img11.deviantart.net/245f/i/2012/339/c/5/cartoon_woman_running_by_digitalalter-d5lnrbb.png this was for the girl running in control menu