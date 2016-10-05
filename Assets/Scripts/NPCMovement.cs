using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCMovement : MonoBehaviour
{
    public Sprite dead;
    Sprite original;
    public int speed;
    public int X_Boundary;
    public int Y_Boundary;
    float horizontal;
    float vertical;

    private float timer;
    private bool isDead;

    // Use this for initialization
    void Start()
    {
        original = GetComponent<SpriteRenderer>().sprite;
        timer = 0;
        isDead = false;
    }

    void Update()
    {
        if (!isDead)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = Random.Range(0.5f, 2);

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
                        if (transform.position.x == X_Boundary)
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
                        if (transform.position.x == X_Boundary * -1)
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
                        if (transform.position.y == Y_Boundary)
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
                        if (transform.position.y == Y_Boundary * -1)
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
                    case 6:
                        horizontal = 0;
                        vertical = 0;
                        break;
                    case 7:
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
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        if (other.gameObject.tag == "Basic Attack")
        {
            isDead = true;
            Debug.Log("attacking");
            GetComponent<SpriteRenderer>().sprite = dead;
            Invoke("ResetSprite", 1);
            
        }
    }

    void ResetSprite()
    {
        GetComponent<SpriteRenderer>().sprite = original;
        isDead = false;
    }
   
}
