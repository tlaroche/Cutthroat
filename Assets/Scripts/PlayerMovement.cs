﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

    public GameObject attack;
    public Sprite dead;
    public int speed;
    public int X_Boundary;
    public int Y_Boundary;

    private int playerIndex;
    private string className;
    private string attackName; // The name of the current player's attack; used to make sure you can't hit yourself with an attack
    private bool isDead;

    // How often a player can use basic attack
    private float basicAttackCooldown;


    public void SetPlayer(string name, int index)
    {
        className = name;
        playerIndex = index;
        attackName = "Attack" + index;
        //attack.name = attackName;
    }


    // Use this for initialization
    void Start () {
        basicAttackCooldown = 0;
        isDead = false;
    }

    void Update()
    {
        Move();

        CheckForBasicAttack();
    }

    void OnGUI()
    {
        //GUI.Label(new Rect(0, 0, 200, 100), stack[stack.Count - 1] + "");
        //GUI.Label(new Rect(0, 0, 200, 100), "hor:" + horizontal + " vert:" + vertical);
        //GUI.Label(new Rect(0, 0, 200, 100), "Direction: " + direction + "Pos:" + transform.position);
    }

    // Moves the player based on the input from the controller joystick
    void Move()
    {
        //Debug.Log(className + playerIndex);
        float horizontal;
        float vertical;

        try
        {
            // If player is dead, don't move
            if (isDead)
            {
                horizontal = 0;
                vertical = 0;
            }
            else // If player is alive, get the input axis from the player
            {
                horizontal = Input.GetAxis("Horizontal" + playerIndex);
                vertical = Input.GetAxis("Vertical" + playerIndex);
            }
        }
        catch (System.ArgumentException e)
        {
            horizontal = 0;
            vertical = 0;
        }
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        if (horizontal > 0)
        {
            if (transform.position.x + (speed * Time.deltaTime) > X_Boundary)
            {
                transform.position.Set(X_Boundary, transform.position.y, transform.position.z);
            }
            else
            {
                transform.Translate(1 * speed * Time.deltaTime, 0, 0);
            }
        }
        else if (horizontal < 0)
        {
            if (transform.position.x - (speed * Time.deltaTime) < X_Boundary * -1)
            {
                transform.position.Set(X_Boundary * -1, transform.position.y, transform.position.z);
            }
            else
            {
                transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
            }
        }
        else if (vertical > 0)
        {
            if (transform.position.y + (speed * Time.deltaTime) > Y_Boundary)
            {
                transform.position.Set(transform.position.x, Y_Boundary, transform.position.z);
            }
            else
            {
                transform.Translate(0, 1 * speed * Time.deltaTime, 0);
            }
        }
        else if (vertical < 0)
        {
            if (transform.position.y - (speed * Time.deltaTime) < Y_Boundary * -1)
            {
                transform.position.Set(transform.position.x, Y_Boundary * -1, transform.position.z);
            }
            else
            {
                transform.Translate(0, -1 * speed * Time.deltaTime, 0);
            }
        }
    }

    // Checks if the player wants to basic attack, and commences the attack if the basic attack is not on cooldown
    void CheckForBasicAttack()
    {
        bool basicAttackPressed = false;

        try
        {
            basicAttackPressed = Input.GetButtonDown("A" + playerIndex);
        }
        catch(System.ArgumentException ae)
        {
            basicAttackPressed = false;
        }

        // Check to see if player presses attack
        if (basicAttackCooldown <= 0)
        {   
            if (basicAttackPressed && !isDead)
            {
                // Temporarily create a sprite for the attack animation
                GameObject temp = (GameObject)Instantiate(attack, transform.position, transform.rotation);
                temp.name = attackName;
                temp.transform.parent = gameObject.transform;
                Destroy(temp, .25f);
                basicAttackCooldown = .25f;
            }
        }
        else
        {
            basicAttackCooldown -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDead)
        {
            // When the player attacks, an attack animation with a collider spawns for a short duration. The attack collider is 
            // on top of the player and will trigger this method, so to prevent the player from killing itself, we need to 
            // add a check to make sure we are not killing ourselves.
            if (other.gameObject.tag == "Basic Attack" && other.name != attackName)
            {
                isDead = true;
                GetComponent<SpriteRenderer>().sprite = dead;
            }
        }
    }
}
