using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CharacterSelectionController : MonoBehaviour {

    StartController startController;
    //public Texture[] checkmarks = new Texture[3];
    public Texture[] charSelectScreen = new Texture[4];

    bool player1selected;
    bool player2selected;
    bool player3selected;
    bool player4selected;

    GameObject[] checks = new GameObject[4];

	// Use this for initialization
	void Start () {
        startController = GameObject.Find("Controller").GetComponent<StartController>();
        startController.player1 = "";
        startController.player2 = "";
        startController.player3 = "";
        startController.player4 = "";

        player1selected = false;
        player2selected = false;
        player3selected = false;
        player4selected = false;
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

        /*if (player != "")
        {
            switch(playerIndex)
            {
                case 1:
                    player1selected = true;
                    break;
                case 2:
                    player2selected = true;
                    break;
                case 3:
                    player3selected = true;
                    break;
                case 4:
                    player4selected = true;
                    break;
            }
        }*/
    }

    // When someone presses start, make sure all players have chosen a class to play
    void CheckAllPlayersReadyToStartGame()
    {
        if (Input.GetButtonDown("Start1") || Input.GetButtonDown("Start2") || Input.GetKeyDown("7"))
        {
            //startController.player2 = "Rogue";
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

    void OnGUI()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (startController.player1 != "" && startController.player2 != "")
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), charSelectScreen[3], ScaleMode.ScaleToFit);
            }
            else if (startController.player1 != "")
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), charSelectScreen[1], ScaleMode.ScaleToFit);
            }
            else if (startController.player2 != "")
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), charSelectScreen[2], ScaleMode.ScaleToFit);
            }
            else
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), charSelectScreen[0], ScaleMode.ScaleToFit);
            }


            /*int screenEdgeLength = (Screen.width > Screen.height) ? Screen.height : Screen.width;
            bool heightLimited = (Screen.width > Screen.height) ? true : false;

            if (heightLimited)
            {
                if (player1selected)
                {
                    GUI.DrawTexture(new Rect((Screen.width - Screen.height) / 2, 0, Screen.width / 10, Screen.height / 10), checkmark, ScaleMode.ScaleToFit);
                }
            }
            else
            {
                if (player1selected)
                {
                    GUI.DrawTexture(new Rect(0, (Screen.height - Screen.width) / 2, Screen.width / 10, Screen.height / 10), checkmark, ScaleMode.ScaleToFit);
                }
            }*/
        }
    }

}
