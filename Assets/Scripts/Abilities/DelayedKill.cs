using UnityEngine;
using System.Collections;

public class DelayedKill : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
        Destroy(gameObject, 0.25f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        /*&if (other.tag == "Player" && !transform.IsChildOf(other.transform))
        {
            other.GetComponent<PlayerMovement>().Invoke("Die", 3);
        }
        else if (other.tag.Contains("NPC"))
        {
            other.GetComponent<NPCMovement>().Invoke("Die", 3);
        }*/
    }
}
