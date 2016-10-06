using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CharacterSelectionController : MonoBehaviour {

    StartController startController;

	// Use this for initialization
	void Start () {
        startController = GameObject.Find("Controller").GetComponent<StartController>();
        startController.player1 = "";
        startController.player2 = "";
        startController.player3 = "";
        startController.player4 = "";
    }
	
	// Update is called once per frame
    void Update()
    {
        CheckPlayerLockIn(ref startController.player1, startController.PLAYER1_INDEX);
        CheckPlayerLockIn(ref startController.player2, startController.PLAYER2_INDEX);
        //CheckPlayerLockIn(ref startController.player3, startController.PLAYER3_INDEX);
        //CheckPlayerLockIn(ref startController.player4, startController.PLAYER4_INDEX);

        CheckAllPlayersReadyToStartGame();
    }

    // Make sure the player has chosen a class to play
    void CheckPlayerLockIn(ref string player, int playerIndex)
    {
        // Press A to select Warrior
        if (/*Input.GetButtonDown("0") || */Input.GetButtonDown("A" + playerIndex))
        {
            if (player == "")
            {
                player = "Warrior";
            }
            else
            {
                player = "";
            }
            Debug.Log(player);
        }
        // Press B to select Ranger
        else if (/*Input.GetButtonDown("1") ||*/ Input.GetButtonDown("B" + playerIndex))
        {
            if (player == "")
            {
                player = "Ranger";
            }
            else
            {
                player = "";
            }
            Debug.Log(player);
        }
        // Press X to select Mage
        else if (/*Input.GetButtonDown("2") ||*/ Input.GetButtonDown("X" + playerIndex))
        {
            if (player == "")
            {
                player = "Mage";
            }
            else
            {
                player = "";
            }
            Debug.Log(player);
        }
        // Press Y to select Rogue
        else if (/*Input.GetButtonDown("3") ||*/ Input.GetButtonDown("Y" + playerIndex))
        {
            if (player == "")
            {
                player = "Rogue";
            }
            else
            {
                player = "";
            }
            Debug.Log(player);
        }
    }

    // When someone presses start, make sure all players have chosen a class to play
    void CheckAllPlayersReadyToStartGame()
    {
        if (Input.GetButtonDown("Start1") || Input.GetButtonDown("Start2") || Input.GetKeyDown("7"))
        {
            if ((startController.player1 != "") && (startController.player2 != "") 
                /*&& (startController.player3 != "") && (startController.player4 != "")*/)
            {
                // Start game if all players have selected a class
                SceneManager.LoadScene(2);
            }
            else
            {
                Debug.Log("All players not ready yet");
            }
        }
    }

    // Creates a prefix for the controller's input (found in the Unity input manager)
    string GetControllerPrefix(int playerIndex)
    {
        return "Controller" + playerIndex + "_";
    }

}
