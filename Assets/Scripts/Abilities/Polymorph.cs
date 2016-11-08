using UnityEngine;
using System.Collections;

public class Polymorph : MonoBehaviour {

    public Sprite mage;
    public AudioClip polymorphAudio;
    public float volume;
    AudioSource audio;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(polymorphAudio, volume);
        //Destroy(gameObject, 0.1f);
        Invoke("TurnOffCollider", 0.1f);
        Invoke("DestroyPolymorph", 3f);
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

    void TurnOffCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    void DestroyPolymorph()
    {
        Destroy(gameObject);
    }
}
