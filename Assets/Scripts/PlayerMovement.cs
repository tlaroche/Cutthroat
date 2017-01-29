using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

    public AudioClip basicAttackAudio;
    public float volume;
    AudioSource audio;

    public GameObject attack;
    public GameObject ability1;
    public GameObject ability2;
    public GameObject mark;

    public Sprite original;

    int ability1Uses;
    int ability2Uses;
    public Sprite dead;
    public int speed;
    public int X_Boundary;
    public int Y_Boundary;

    StartController startController;
    GameController gameController;
    int winner;

    public int playerIndex;
    public int teamNumber;
    private string className;

    //private string attackName; // The name of the current player's attack; used to make sure you can't hit yourself with an attack

    public bool isDead;
    public bool isFakeDead;
    public bool feignDeathActive;
    public bool shielded;

    // How often a player can use basic attack
    private float basicAttackCooldown;


    public void SetPlayer(string name, int index)
    {
        className = name;
        playerIndex = index;

    //    attackName = "Attack" + index;
    }


    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();

        startController = GameObject.Find("StartController").GetComponent<StartController>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        original = GetComponent<SpriteRenderer>().sprite;

        X_Boundary = 29;
        Y_Boundary = 28;
        speed = 7;

        basicAttackCooldown = 0;
        isDead = false;
        isFakeDead = false;
        ability1Uses = 3;
        ability2Uses = 1;

        winner = 0;
        feignDeathActive = false;
        shielded = false;
    }

    void Update()
    {

        Move();

        CheckForBasicAttack();
        UseAbility1();
        UseAbility2();

    }

    void OnGUI()
    {

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
            if (isDead || isFakeDead)
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

        if (horizontal > 0.5f)
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
        else if (horizontal < -0.5f)
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
        else if (vertical > 0.5f)
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
        else if (vertical < -0.5f)
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
        // Check to see if player presses attack
        if (basicAttackCooldown <= 0)
        {   
            //if (basicAttackPressed && !isDead)
            if (Input.GetButtonDown("X" + playerIndex) && !isDead && !feignDeathActive && !isFakeDead)
            {
                audio.PlayOneShot(basicAttackAudio, volume);
                // Temporarily create a sprite for the attack animation
                GameObject temp = (GameObject) Instantiate(attack, transform.position, transform.rotation);
                temp.transform.parent = gameObject.transform;
                Destroy(temp, .25f);
                basicAttackCooldown = 1f;
            }
        }
        else
        {
            basicAttackCooldown -= Time.deltaTime;
        }
    }

    void UseAbility1()
    {
        if (Input.GetButtonDown("A" + playerIndex) && !isDead && !isFakeDead && ability1Uses != 0)
        {
            ability1Uses--;
            GameObject tempAbility = (GameObject) Instantiate(ability1, transform.position, transform.rotation);
            if (GameObject.Find("StartController").GetComponent<StartController>().gameMode == "FFA")
            {
                tempAbility.name += playerIndex;
            }
            else if (GameObject.Find("StartController").GetComponent<StartController>().gameMode == "TDM")
            {
                tempAbility.name += teamNumber;
            }

            // If the Hunter uses Trap, don't make the ability follow the player, instantiate 
            // and stay at the position the player was at
            if (ability1.tag != "Trap")
            {
                tempAbility.transform.parent = gameObject.transform; // Make the ability move with the player
            }
        }
    }

    void UseAbility2()
    {
        if (Input.GetButtonDown("B" + playerIndex) && !isDead && !isFakeDead && ability2Uses != 0)
        {
            ability2Uses--;
            GameObject temp = (GameObject) Instantiate(ability2, transform.position, transform.rotation);
            temp.name += playerIndex;
            temp.transform.parent = gameObject.transform;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(shielded);
        if (!isDead && !shielded)
        {
            // When the player attacks, an attack animation with a collider spawns for a short duration. The attack collider is 
            // on top of the player and will trigger this method, so to prevent the player from killing itself, we need to 
            // add a check to make sure we are not killing ourselves.
            bool sameTeam = other.transform.parent != null && other.transform.parent.tag == tag && tag.Contains("Team");
            
            bool enragedAttacker = false;

            if (startController.gameMode == "FFA")
            {
                if (other.transform.parent != null && other.transform.parent.GetComponent<NPCMovement>() != null && other.transform.parent.GetComponent<NPCMovement>().isEnragedBy == playerIndex)
                {
                    enragedAttacker = true;
                }
            }
            else if (GameObject.Find("StartController").GetComponent<StartController>().gameMode == "TDM")
            {
                if (other.transform.parent != null && other.transform.parent.GetComponent<NPCMovement>() != null && other.transform.parent.GetComponent<NPCMovement>().isEnragedBy == teamNumber)
                {
                    enragedAttacker = true;
                }
            }
            

            if (other.gameObject.tag == "Basic Attack" && !other.transform.IsChildOf(transform) && !sameTeam && !enragedAttacker)
            {
                if (feignDeathActive)
                {
                    FakeDeath();
                }
                else
                {

                    if (other.gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>() != null)
                    {
                        int otherPlayerIndex = other.gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().playerIndex;
                        startController.playerScores[otherPlayerIndex - 1]++;
                        Die();
                    }
                    else
                    {
                        int otherPlayerIndex = other.gameObject.transform.parent.gameObject.GetComponent<NPCMovement>().isEnragedBy;
                        startController.playerScores[otherPlayerIndex - 1]++;
                        Die();
                    }
                }
            }
            else if (other.gameObject.tag == "Delayed Kill" && !other.transform.IsChildOf(transform) && !sameTeam)
            {
                if (feignDeathActive)
                {
                    FakeDeath();
                }
                else
                {
                    int otherPlayerIndex = other.gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().playerIndex;
                    startController.playerScores[otherPlayerIndex - 1]++;
                    Invoke("Die", 3);
                }
            }
        }
    }

    public void Die()
    {
        GetComponent<SpriteRenderer>().sprite = dead;
        isDead = true;
        gameController.standings.Add(playerIndex);

        if (winner != 0)
        {

            startController.winner = winner;
        }

        Invoke("Victory", 5);
    }

    public void FakeDeath()
    {
        feignDeathActive = false;
        GetComponent<SpriteRenderer>().sprite = dead;
        isFakeDead = true;
        Invoke("ResetSprite", 2);
    }

    void ResetSprite()
    {
        isFakeDead = false;
        GetComponent<SpriteRenderer>().sprite = original;
        shielded = true;
        Invoke("Unshield", 0.5f);
    }

    void Unshield()
    {
        shielded = false;
    }

    void Victory()
    {
        //SceneManager.LoadScene(3);
    }





}
