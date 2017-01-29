using UnityEngine;
using System.Collections.Generic;

public class Disguise : MonoBehaviour {

    public Sprite[] sprites = new Sprite[3];

    StartController startController;
	// Use this for initialization
	void Start () {
        startController = GameObject.Find("StartController").GetComponent<StartController>();
        if (startController.gameMode == "FFA")
        {
            transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 3)];
            Destroy(gameObject);
        }
        else if (startController.gameMode == "TDM")
        {
            List<PlayerMovement> playerList = GameObject.Find("GameController").GetComponent<GameController>().playerList;

            foreach (PlayerMovement otherPlayer in playerList)
            {
                if (transform.parent.GetComponent<PlayerMovement>().original != otherPlayer.original)
                {
                    transform.parent.GetComponent<SpriteRenderer>().sprite = otherPlayer.original;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
