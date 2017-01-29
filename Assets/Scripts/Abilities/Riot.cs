using UnityEngine;
using System.Collections;

public class Riot : MonoBehaviour {

    public AudioClip riotAudio;
    public float volume;
    AudioSource audio;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(riotAudio, volume);

        BasicAttack();
        Invoke("TurnOffCollider", .1f);
        Invoke("DestroyRiot", 2f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("NPC"))
        {
            if (GameObject.Find("StartController").GetComponent<StartController>().gameMode == "FFA")
            {
                other.GetComponent<NPCMovement>().BasicAttack(transform.parent.gameObject.GetComponent<PlayerMovement>().playerIndex);
            }
            else if (GameObject.Find("StartController").GetComponent<StartController>().gameMode == "TDM")
            {
                other.GetComponent<NPCMovement>().BasicAttack(transform.parent.gameObject.GetComponent<PlayerMovement>().teamNumber);
            }
        }
    }

    void BasicAttack()
    {
        GameObject attack = transform.parent.GetComponent<PlayerMovement>().attack;
        GameObject tempAttack = (GameObject) Instantiate(attack, transform.position, transform.rotation);
        tempAttack.transform.parent = transform.parent;
        Destroy(tempAttack, .25f);
    }

    void DestroyRiot()
    {
        Destroy(gameObject);
    }

    void TurnOffCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
