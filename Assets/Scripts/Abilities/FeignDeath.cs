using UnityEngine;
using System.Collections;

public class FeignDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.parent.GetComponent<PlayerMovement>().feignDeathActive = true;
        Invoke("Reset", 2);
        Destroy(gameObject, 2.25f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Reset()
    {
        transform.parent.GetComponent<PlayerMovement>().feignDeathActive = false;
    }
}
