using UnityEngine;
using System.Collections;

public class Riot : MonoBehaviour {

	// Use this for initialization
	void Start () {
        BasicAttack();
        Destroy(gameObject, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("NPC"))
        {
            other.GetComponent<NPCMovement>().BasicAttack();
        }
    }

    void BasicAttack()
    {
        GameObject attack = transform.parent.GetComponent<PlayerMovement>().attack;
        GameObject tempAttack = (GameObject) Instantiate(attack, transform.position, transform.rotation);
        tempAttack.transform.parent = gameObject.transform;
        Destroy(tempAttack, .25f);
    }
}
