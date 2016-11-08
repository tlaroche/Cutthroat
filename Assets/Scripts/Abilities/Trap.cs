using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

    public GameObject mark;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !gameObject.name.Contains(other.GetComponent<PlayerMovement>().playerIndex.ToString()))
        {
            Transform player = other.transform;

            Vector3 pos = new Vector3(0, 3f, 0);
            GameObject tempMark = (GameObject)Instantiate(mark, player.position + pos, player.rotation);
            tempMark.transform.parent = player;
            
            Destroy(gameObject);
            Destroy(tempMark, 5f);
        }
        else if (other.tag.Contains("Team") && !gameObject.name.Contains(other.GetComponent<PlayerMovement>().teamNumber.ToString()))
        {
            Transform player = other.transform;

            Vector3 pos = new Vector3(0, 3f, 0);
            GameObject tempMark = (GameObject)Instantiate(mark, player.position + pos, player.rotation);
            tempMark.transform.parent = player;

            Destroy(gameObject);
            Destroy(tempMark, 5f);
        }
    }
}
