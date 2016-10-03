using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CharacterSelectionController : MonoBehaviour {

    StartController startController;

	// Use this for initialization
	void Start () {
        startController = GameObject.Find("Controller").GetComponent<StartController>();
        startController.player1 = "";
    }
	
	// Update is called once per frame
	void Update () {
	    if (/*Input.GetButtonDown("0") || */Input.GetKeyDown("0"))
        {
            if (startController.player1 == "")
            {
                startController.player1 = "Warrior";
            }
            else
            {
                startController.player1 = "";
            }
            Debug.Log(startController.player1);
        }
        else if (/*Input.GetButtonDown("1") ||*/ Input.GetKeyDown("1"))
        {
            if (startController.player1 == "")
            {
                startController.player1 = "Ranger";
            }
            else
            {
                startController.player1 = "";
            }
            Debug.Log(startController.player1);
        }
        else if (/*Input.GetButtonDown("2") ||*/ Input.GetKeyDown("2"))
        {
            if (startController.player1 == "")
            {
                startController.player1 = "Mage";
            }
            else
            {
                startController.player1 = "";
            }
            Debug.Log(startController.player1);
        }
        else if (/*Input.GetButtonDown("3") ||*/ Input.GetKeyDown("3"))
        {
            if (startController.player1 == "")
            {
                startController.player1 = "Rogue";
            }
            else
            {
                startController.player1 = "";
            }
            Debug.Log(startController.player1);
        }

        if (/*Input.GetButtonDown("7") ||*/ Input.GetKeyDown("7"))
        {
            if (startController.player1 != "")
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}
