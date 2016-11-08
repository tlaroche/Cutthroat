using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    public AudioClip shieldAudio;
    public float volume;
    AudioSource audio;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        CreateShield();
        Invoke("StopShield", 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void CreateShield()
    {
        audio.PlayOneShot(shieldAudio, volume);
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
