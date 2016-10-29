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

    int numPlayers = 2;

	// Use this for initialization
	void Start () {
        startController = GameObject.Find("StartController").GetComponent<StartController>();
        /*startController.player1 = "";
        startController.player2 = "";
        startController.player3 = "";
        startController.player4 = "";*/
        

        player1selected = false;
        player2selected = false;
        player3selected = false;
        player4selected = false;
    }
	
	// Update is called once per frame
    void Update()
    {
        if (startController.players.Count >= numPlayers)
        {
            for (int i = 0; i < numPlayers; i++)
            {
                CheckPlayerLockIn(startController.players, i);
            }
        }

        //CheckPlayerLockIn(ref startController.player1, startController.PLAYER1_INDEX);
        //CheckPlayerLockIn(ref startController.player2, startController.PLAYER2_INDEX);
        //CheckPlayerLockIn(ref startController.player3, startController.PLAYER3_INDEX);
        //CheckPlayerLockIn(ref startController.player4, startController.PLAYER4_INDEX);

        CheckAllPlayersReadyToStartGame();
    }

    // Make sure the player has chosen a class to play
    void CheckPlayerLockIn(List<string> players, int index)
    {
        // Press A to select Warrior
        if (/*Input.GetButtonDown("0") || */Input.GetButtonDown("A" + (index + 1)))
        {
            if (players[index] == "")
            {
                players[index] = "Warrior";
            }
            else
            {
                players[index] = "";
            }
            //Debug.Log(players[index]);
        }
        // Press B to select Ranger
        else if (/*Input.GetButtonDown("1") ||*/ Input.GetButtonDown("B" + (index + 1)))
        {
            if (players[index] == "")
            {
                players[index] = "Ranger";
            }
            else
            {
                players[index] = "";
            }
            //Debug.Log(players[index]);
        }
        // Press X to select Mage
        else if (/*Input.GetButtonDown("2") ||*/ Input.GetButtonDown("X" + (index + 1)))
        {
            if (players[index] == "")
            {
                players[index] = "Mage";
            }
            else
            {
                players[index] = "";
            }
            //Debug.Log(players[index]);
        }
        // Press Y to select Rogue
        else if (/*Input.GetButtonDown("3") ||*/ Input.GetButtonDown("Y" + (index + 1)))
        {
            if (players[index] == "")
            {
                players[index] = "Rogue";
            }
            else
            {
                players[index] = "";
            }
            //Debug.Log(players[index]);
        }
        //Debug.Log(playerIndex + "player:" + player);
    }

    // When someone presses start, make sure all players have chosen a class to play
    void CheckAllPlayersReadyToStartGame()
    {

        if (Input.GetButtonDown("Start1") || Input.GetButtonDown("Start2") || Input.GetKeyDown("7"))
        {

            bool allPlayersReady = true;

            for (int i = 0; i < numPlayers; i++)
            {
                if (startController.players[i] == "")
                {
                    allPlayersReady = false;
                }
            }

            if (allPlayersReady)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                Debug.Log("All players not ready yet");
            }
        }
    }

    void OnGUI()
    {
        //Debug.Log(startController.players.Count);
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (startController.players[0] != "" && startController.players[1] != "")
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), charSelectScreen[3], ScaleMode.ScaleToFit);
            }
            else if (startController.players[0] != "")
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), charSelectScreen[1], ScaleMode.ScaleToFit);
            }
            else if (startController.players[1] != "")
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
