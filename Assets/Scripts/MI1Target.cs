using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MI1Target : MonoBehaviour {

    int speed = 7;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y == 20 && transform.position.x < 20)
        {
            if (transform.position.x + (speed * Time.deltaTime) > 20)
            {
                transform.position.Set(20f, transform.position.y, transform.position.z);
                Debug.Log(transform.position + "what");
            }
            else
            {
                transform.Translate(1 * speed * Time.deltaTime, 0, 0);
                Debug.Log(transform.position + "help");
            }
        }
        else if (transform.position.x == 20 && transform.position.y > -20)
        {
            if (transform.position.y - (speed * Time.deltaTime) < -20)
            {
                transform.position.Set(transform.position.x, -20f, transform.position.z);
                Debug.Log(transform.position);
            }
            else
            {
                transform.Translate(0, -1 * speed * Time.deltaTime, 0);
                Debug.Log(transform.position);
            }
        }
        else if (transform.position.y == -20 && transform.position.x > -20)
        {
            if (transform.position.x - (speed * Time.deltaTime) < -20)
            {
                transform.position.Set(-20f, transform.position.y, transform.position.z);
                Debug.Log(transform.position);
            }
            else
            {
                transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
                Debug.Log(transform.position);
            }
        }
        else if (transform.position.x == -20 && transform.position.y < 20)
        {
            if (transform.position.y + (speed * Time.deltaTime) > 20)
            {
                transform.position.Set(transform.position.x, 20f, transform.position.z);
                Debug.Log(transform.position);
            }
            else
            {
                transform.Translate(0, 1 * speed * Time.deltaTime, 0);
                Debug.Log(transform.position);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Basic Attack")   /*&& !other.transform.IsChildOf(transform))*/
        {
            Die();
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(3);
    }
}
