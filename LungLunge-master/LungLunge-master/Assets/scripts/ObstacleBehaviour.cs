using UnityEngine;
using System.Collections;

/// <summary>
/// Defines default behaviour of an obstacle in the game world. 
/// 
/// This default obstacle behaviour does damage on contact and attempts to give chase to the player. 
/// 
/// Defines thisngs such as obstacle health, vulnerability to puffer charge, and damage given to player. 
/// 
/// The modification of player health is managed by scripts on the player itself. 
/// </summary>
public  class ObstacleBehaviour : MonoBehaviour {

    /// <summary>
    /// Enum representing the current aggro state of the obstacle, if present
    /// </summary>
    public enum AggroState
    {
        Idle, //Doing nothing
        Aggro, //chasign the player
        Reset //resetting to default position
    }
    public bool hasAggro = false;
    public bool canChase = false;

    public float maxHealth = 100;
    public float damageDone = 10f;
    public float damageTimeout = 0.75f;

    private float playerCollideEnterTime = -100.0f;

    public float aggroRangeEngage = 5.0f;
    public float aggroRangeFalloff = 10.0f;

    public float runSpeed = 5.0f;

    private AggroState aggroState = AggroState.Idle;
    private float currentHealth;
    GameObject player = null; //reference to the player object in the scene
    Rigidbody rb = null; //rigidbody of the obstacle, if one exists. 


    /// <summary>
    /// Initialization
    /// </summary>
    protected void Start()
    {
        currentHealth = maxHealth;
        //grab reference to player 
        player = GameObject.Find("Player");
        //grab reference to rigidbody, if one exists. 
        rb = GetComponent<Rigidbody>();

    }

    /// <summary>
    /// update method called once per frame. 
    /// </summary>
    void Update()
    {
        this.updateAggro();
        this.updateMovement();
    }

    /// <summary>
    /// Update the aggro state of the obstacle. This method is usually called in 
    /// Update(). Default behaviour is to aggro when player gets in AggroRangeEngage, 
    /// and fall back to 
    /// </summary>
    protected virtual void updateAggro()
    {
        if (!hasAggro) return;
        if (this.isDead()) return;
        switch (this.aggroState)
        {
            case AggroState.Idle:
                if(Vector3.Distance(transform.position, player.transform.position) < this.aggroRangeEngage)
                {
                    this.aggroState = AggroState.Aggro;
                }
                break;
            case AggroState.Aggro:
                if (Vector3.Distance(transform.position, player.transform.position) > this.aggroRangeFalloff)
                {
                    this.aggroState = AggroState.Idle;
                }
                break;
        }
    }

    /// <summary>
    /// Update the movement of the obstacle. 
    /// Generally, an obstacle will not move until aggro is attained. At that point, the 
    /// obstacle will attempt to move toward the player in an attempt to cause damage. 
    /// </summary>
    protected virtual void updateMovement()
    {
        if (!canChase) return; //only if we can chase
        if (this.rb == null) return; //if we don't have a rigidbody then we can't move. 
        if (this.aggroState != AggroState.Aggro) return; //only if we're aggroing
        if (this.isDead()) return; 

        Vector3 displacement = player.transform.position - transform.position;
        float direction = Mathf.Sign(displacement.x);
        //apply velocity
        this.rb.velocity = new Vector3(direction * this.runSpeed, rb.velocity.y, rb.velocity.z);
    }

    public float getHealth()
    {
        return this.currentHealth;
    }

    public bool isDead()
    {
        return currentHealth <= 0;
    }

    //Do damage to the obstacle. 
    public void takeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
    }
    
    public void OnCollisionStay(Collision collision)
    {
        float elapsedTimeSinceLastDmg = Time.time - this.playerCollideEnterTime;
        
        if (collision.gameObject.name.Equals("Player") && elapsedTimeSinceLastDmg >= this.damageTimeout)
        {
            print(string.Format("Elapsed time is {0}", elapsedTimeSinceLastDmg));
            print(string.Format("doing some damage, set player time to {0}", Time.time));
            //do some damage and reset the countdown timer
            this.playerCollideEnterTime = Time.time;
            player.GetComponent<PlayerManager>().DepleteBreath(this.damageDone);
        }

    }
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Cloud"))
			Destroy (gameObject);
	}
   
}
