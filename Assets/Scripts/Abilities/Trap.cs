using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

    public GameObject mark;
    public AudioClip trapAudio;
    public float trapVolume;
    AudioSource audio;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (audio.isPlaying)
        {
            Debug.Log("playing");
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !gameObject.name.Contains(other.GetComponent<PlayerMovement>().playerIndex.ToString()))
        {
            audio.PlayOneShot(trapAudio, trapVolume);
            Transform player = other.transform;

            Vector3 pos = new Vector3(0, 3f, 0);
            GameObject tempMark = (GameObject)Instantiate(mark, player.position + pos, player.rotation);
            tempMark.transform.parent = player;

            GetComponent<Collider2D>().enabled = false;
            Invoke("DestroyTrap", 5);
            Destroy(tempMark, 5f);
        }
        else if (other.tag.Contains("Team") && !gameObject.name.Contains(other.GetComponent<PlayerMovement>().teamNumber.ToString()))
        {
            audio.PlayOneShot(trapAudio, trapVolume);
            Transform player = other.transform;

            Vector3 pos = new Vector3(0, 3f, 0);
            GameObject tempMark = (GameObject)Instantiate(mark, player.position + pos, player.rotation);
            tempMark.transform.parent = player;

            GetComponent<Collider2D>().enabled = false;
            Invoke("DestroyTrap", 5);
            Destroy(tempMark, 5f);
        }
    }

    void DestroyTrap()
    {
        Destroy(gameObject);
    }
}
