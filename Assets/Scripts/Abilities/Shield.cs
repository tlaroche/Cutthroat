using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CreateShield();
        Invoke("Destroy", 3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void CreateShield()
    {
        transform.parent.gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    void Destroy()
    {
        Destroy(gameObject);
        transform.parent.gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
    
    // This method checks to see if the player is currently being shielded.
    bool CheckHasShield()
    {
        // When the player uses the shield ability, the player object will have 1 shield instantiated
        int shieldCounter = 0;
        foreach (Transform child in transform.parent)
        {
            if (child.tag == "Shield")
            {
                Debug.Log("Child tag:" + child.tag);
                shieldCounter++;
            }
        }
        Debug.Log("Number of shields: " + shieldCounter);
        return shieldCounter == 1;
    }


}
