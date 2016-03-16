using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/// <summary>
/// Manages input into the player. 
/// </summary>
public class InputManager: MonoBehaviour
{
    PlayerManager pm;
    

    // Use this for initialization
    void Start()
    {
        pm = GetComponent<PlayerManager>();
    }

    // FixedUpdate is called once per physics frame
    void Update()
    {
        //running and jumping
        pm.run(Input.GetAxisRaw("Horizontal"));
        if (Input.GetButtonDown("Jump"))
        {
            pm.jump();
        }
        if (Input.GetButtonDown("PufferSpray")) pm.PufferSpray();
        if (Input.GetButtonDown("PufferSelf")) pm.PufferUseSelf();

    }

    
}
