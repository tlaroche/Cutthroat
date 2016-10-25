using UnityEngine;
using System.Collections;

public class Polymorph : MonoBehaviour {

    public Sprite mage;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("NPC"))
        {
            other.GetComponent<SpriteRenderer>().sprite = mage;
        }
    }
}
