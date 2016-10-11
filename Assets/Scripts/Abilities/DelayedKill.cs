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
}
