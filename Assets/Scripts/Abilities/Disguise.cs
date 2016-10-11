using UnityEngine;
using System.Collections;

public class Disguise : MonoBehaviour {

    public Sprite[] sprites = new Sprite[3]; 

	// Use this for initialization
	void Start () {
        transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 3)];
        Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
