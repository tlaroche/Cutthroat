using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

    public GameObject attack;

    public int speed;
    
    public int X_Boundary;
    public int Y_Boundary;

    private float basicAttackCooldown;

    // Use this for initialization
    void Start () {
        basicAttackCooldown = 0;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Controller1_Horizontal");
        float vertical = Input.GetAxis("Controller1_Vertical");
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        Debug.Log("Horizontal float: " + horizontal);
        Debug.Log("Vertical floatL" + vertical);
                       
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

        if (basicAttackCooldown <= 0)
        {
            if (Input.GetKeyDown("space") || Input.GetButtonDown("Controller1_A"))
            {
                GameObject temp = (GameObject)Instantiate(attack, transform.position, transform.rotation);
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

    void OnGUI()
    {
        //GUI.Label(new Rect(0, 0, 200, 100), stack[stack.Count - 1] + "");
        //GUI.Label(new Rect(0, 0, 200, 100), "hor:" + horizontal + " vert:" + vertical);
        //GUI.Label(new Rect(0, 0, 200, 100), "Direction: " + direction + "Pos:" + transform.position);
    }

    
}
