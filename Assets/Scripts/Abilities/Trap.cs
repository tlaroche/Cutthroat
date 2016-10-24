using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !gameObject.name.Contains(other.GetComponent<PlayerMovement>().playerIndex.ToString()))
        {
            Debug.Log("Initializing a mark above the char's sprite");


            other.gameObject.GetComponent<PlayerMovement>().Mark();
            
            Destroy(gameObject, 0.2f);
        }
    }
}
