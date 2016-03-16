using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

/// <summary>
/// Manages actions of the player and also handles player resources including
/// breath and puffer charge. 
/// </summary>
public class PlayerManager: MonoBehaviour {
    private const float ROTATION_ANGLE_RIGHT = 90.0f;
    private const float ROTATION_ANGLE_LEFT = 270.0f;
    private const float WINTER_PENALTY = 25.0f;
    public float MoveSpeed;
    public float JumpSpeed;

	public int currenttime;
	public int currentscore;

    public float JumpDepletionAmount;
    public float RunDepletionRate;
    public int   BreathRecoveryTimeout;
    public float BreathRecoveryRate;
    public float PufferBreathRecovered;

    public float MaxBreath;
    public float MaxPufferCharge;

    public float PufferCostSelf;
    public float PufferCostSpray;

    private float currentBreath;
    private float currentPufferCharge;

    public Transform PufferCloud = null;

	private ConfigManager cm;
    private Rigidbody rb;
    private int direction = 1; //1 means looking left, -1 means looking right. 
    private bool grounded = false;

    private Stopwatch recoveryTimer = new Stopwatch();

	private ActionsNew actionController;
    //check weather
    public bool isWinter = false;
    public float recoverModifier = 1.0f;
	// Use this for initialization
	void Start () {
		cm = GetComponent<ConfigManager> ();
        rb = GetComponent<Rigidbody>();
		actionController = GetComponentInChildren<ActionsNew> ();
        // load value from config file if they exist. Otherwise use the defaults from the unity editor. 
		cm = GameObject.Find ("Configuration").GetComponent <ConfigManager>();// GetComponent<ConfigManager> ();

		MoveSpeed =  (float)Double.Parse(cm.Load ("MoveSpeed"));
		JumpSpeed =  (float)Double.Parse(cm.Load ("JumpSpeed"));
		JumpDepletionAmount =  (float)Double.Parse(cm.Load ("JumpDepletionAmount"));
		RunDepletionRate =  (float)Double.Parse(cm.Load ("RunDepletionRate"));
		BreathRecoveryTimeout =  Int32.Parse(cm.Load ("BreathRecoveryTimeout"));
		BreathRecoveryRate =  (float)Double.Parse(cm.Load ("BreathRecoveryRate"));
        if (isWinter) recoverModifier = WINTER_PENALTY/100; //if its winter then recovery is 25% less than normal
        PufferBreathRecovered =  (float)Double.Parse(cm.Load ("PufferBreathRecovered"))*recoverModifier;
		MaxBreath =  (float)Double.Parse(cm.Load ("MaxBreath"));
		MaxPufferCharge =  (float)Double.Parse(cm.Load ("MaxPufferCharge"));
		PufferCostSelf =  (float)Double.Parse(cm.Load ("PufferCostSelf"));
		PufferCostSpray =  (float)Double.Parse(cm.Load ("PufferCostSpray"));

		this.transform.position = GameObject.Find ("LevelStart").transform.position + new Vector3 (0.0f, 0.5f, 0.0f);
		currentBreath = MaxBreath;
		currentPufferCharge = MaxPufferCharge;

       
	}

    /// <summary>
    /// Manually delete the specified amount of breath from the player.
   ///  Does not allow the breath to become negative. 
    /// </summary>
    /// <param name="amt"> amount to reduce breath by. </param>
    public void DepleteBreath(float amt)
    {
        currentBreath = Mathf.Max(0f, currentBreath - amt);
    }

    public float getBreath()
    {
        return this.currentBreath;
    }

    public float getPufferCharge()
    {
        return this.currentPufferCharge;
    }
    /// <summary>
    /// Determine whether the player is running. 
    /// </summary>
    /// <returns>True if the player is currently running. False otherwise. </returns>
    public bool isRunning()
    {
        if (rb == null || !grounded) return false;
        return Mathf.Abs(rb.velocity.x) > 0.1;
    }

    /// <summary>
    /// Use puffer on self. Fails if puffer charge is too low.  
    /// </summary>
    public void PufferUseSelf()
    {
        if (this.currentPufferCharge < PufferCostSelf) return;
        currentPufferCharge -= PufferCostSelf;
        currentBreath = Mathf.Min(MaxBreath, currentBreath + PufferBreathRecovered);
        //TODO: play animation here. 
    }

    /// <summary>
    /// Spray a cloud of puffer spray in front of the player. 
    /// Does nothing if charge is too low. 
    /// </summary>
    public void PufferSpray()
    {
        if (this.currentPufferCharge < PufferCostSpray) return;
        currentPufferCharge -= PufferCostSpray;
        //create puffer ball object
        float xoffset = this.direction * 1.0f;

        Instantiate(PufferCloud,
            new Vector3(transform.position.x + xoffset, transform.position.y + 1.0f, transform.position.z),
            Quaternion.identity);
    }
	
    /// <summary>
    /// Run in a direction.
    /// </summary>
    /// <param name="direction">Direction to run. Positive values are to the left. </param>
    public void run(float direction)
    {
        direction = Mathf.Clamp(direction, -1.0f, 1.0f);
        Transform tmesh = transform.Find("SportyGirl").transform;
        //rotate model so he is facing in direction of movement. 
        if (direction < 0.0f)
        {
            this.direction = -1;
            tmesh.eulerAngles = Vector3.Lerp(tmesh.eulerAngles, new Vector3(tmesh.eulerAngles.x, ROTATION_ANGLE_LEFT, tmesh.eulerAngles.z), 0.5f);
        }
        else if (direction > 0.0f)
        {
            this.direction = 1;
			tmesh.eulerAngles = Vector3.Lerp(tmesh.eulerAngles, new Vector3(tmesh.eulerAngles.x, ROTATION_ANGLE_RIGHT, tmesh.eulerAngles.z), 0.5f);
        }

        //now apply the velocity to the rigidbody.
		if (getBreath () > 5) {
			rb.velocity = new Vector3 (direction * MoveSpeed, rb.velocity.y, rb.velocity.z);
		}
    }

    /// <summary>
    /// Command the player to jump. Player must have sufficient breath to jump. 
    /// We must also determine if the player is touching the ground and is therefore able to jump. 
    /// </summary>
    public void jump()
    {
        if (currentBreath < JumpDepletionAmount) return;
        if (!grounded) return;
		actionController.Jump ();
        currentBreath -= JumpDepletionAmount;
        rb.velocity += new Vector3(0.0f, JumpSpeed, 0.0f);
    }

    void Update()
    {
        //Start/reset the recovery timer based on if we're running or not. Also deplete breath
        if (this.isRunning())
        {
			actionController.Run ();
            recoveryTimer.Reset();
            this.DepleteBreath(RunDepletionRate * Time.smoothDeltaTime);

        }
        else 
		{
			recoveryTimer.Start();
			actionController.Stay ();
		}

        //if we've passed the recovery time then we can start recovering breath
        if(recoveryTimer.ElapsedMilliseconds > BreathRecoveryTimeout)
        {
            currentBreath = Math.Min(MaxBreath, currentBreath + BreathRecoveryRate * Time.smoothDeltaTime);
            
        }
    }

    /// <summary>
    /// Used for keeping track of if the player is touching the ground or not. 
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
            grounded = true;

    }

    /// <summary>
    /// Used to track if the player is touching the ground for jumping purposes.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
            grounded = false;
    }

    public void addPufferCharge(float amount)
    {
        amount = Math.Max(amount, 0.0f);
        this.currentPufferCharge = Math.Min(MaxPufferCharge, amount + currentPufferCharge);
    }
	public void addscore(int amount){
		currentscore += amount;
	}
	public int getscore(){
		return currentscore;
	}
	public int gettime(){
		currenttime = (int)Time.timeSinceLevelLoad;
		return currenttime;
	}
}
