using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CreateShield();
        Invoke("StopShield", 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void CreateShield()
    {
        //transform.parent.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        transform.parent.gameObject.GetComponent<PlayerMovement>().shielded = true;
        transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 10;
    }

    void StopShield()
    {
        transform.parent.gameObject.GetComponent<PlayerMovement>().shielded = false;
        transform.parent.gameObject.GetComponent<PlayerMovement>().speed = 7;
        Destroy(gameObject);
    }
}
