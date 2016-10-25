using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCMovement : MonoBehaviour
{
    public GameObject basicAttack;
    
    public Sprite dead;
    Sprite original;
    
    public int speed;
    public int X_Boundary;
    public int Y_Boundary;
    float horizontal;
    float vertical;

    private float timer;
    private bool isDead;
    private bool isAttacked;
    private bool beforeSuddenDeath;


    // Use this for initialization
    void Start()
    {
        X_Boundary = 29;
        Y_Boundary = 28;
        speed = 7;

        beforeSuddenDeath = true;
        original = GetComponent<SpriteRenderer>().sprite;
        timer = 0;
        isAttacked = false;
        isDead = false;
    }

    void Update()
    {
        if (!isAttacked && !isDead)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = Random.Range(0.5f, 2);


                // Random npc movements, more of a chance to idle, and less chance to stay near a wall
                switch (Random.Range(0, 8))
                {
                    case 0: // idle
                        horizontal = 0;
                        vertical = 0;
                        break;
                    case 1: // idle
                        horizontal = 0;
                        vertical = 0;
                        break;
                    case 2:
                        if (transform.position.x == X_Boundary) // move away from right border
                        {
                            horizontal = -1;
                            vertical = 0;
                        }
                        else
                        {
                            horizontal = 1;
                            vertical = 0;
                        }
                        break;
                    case 3:
                        if (transform.position.x == X_Boundary * -1) // move away from left border
                        {
                            horizontal = 1;
                            vertical = 0;
                        }
                        else
                        {
                            horizontal = -1;
                            vertical = 0;
                        }
                        break;
                    case 4:
                        if (transform.position.y == Y_Boundary) // move away from top border
                        {
                            horizontal = 0;
                            vertical = -1;
                        }
                        else
                        {
                            horizontal = 0;
                            vertical = 1;
                        }
                        break;
                    case 5:
                        if (transform.position.y == Y_Boundary * -1) // move away from bottom border
                        {
                            horizontal = 0;
                            vertical = 1;
                        }
                        else
                        {
                            horizontal = 0;
                            vertical = -1;
                        }
                        break;
                    case 6: //idle
                        horizontal = 0;
                        vertical = 0;
                        break;
                    case 7: //idle
                        horizontal = 0;
                        vertical = 0;
                        break;
                }
            }

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
    }
    
    // Checks attack collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDead && !isAttacked)
        {
            if (other.gameObject.tag == "Basic Attack" && !other.transform.IsChildOf(transform))
            {
                isAttacked = true;
                Die();
            }
        }
    }

    public void Die()
    {
        isAttacked = true;
        GetComponent<SpriteRenderer>().sprite = dead;
        Invoke("ResetSprite", 2);
    }

    // After an NPC is attacked, reset the sprite to the original
    void ResetSprite()
    {
        if (!isDead)
        {
            GetComponent<SpriteRenderer>().sprite = original;
            isAttacked = false;
        }
    }
    
    // Kill npc, set sprite to the dead sprite, and set isDead true
    public void KillNPC()
    {
        //Debug.Log("Killing NPC");
        //GameObject npc = GameObject.Find(name);
        //npc.GetComponent<SpriteRenderer>().sprite = dead;
        isDead = true;
        GetComponent<SpriteRenderer>().sprite = dead;
    }

    public void BasicAttack()
    {
        if (!isDead && !isAttacked)
        {
            GameObject attack = (GameObject) Instantiate(basicAttack, transform.position, transform.rotation);
            attack.transform.parent = gameObject.transform;
            Destroy(attack, 0.25f);
        }
    }
}
