using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    
    public GameObject npcWarrior;
    public GameObject npcMage;
    public GameObject npcRanger;
    public GameObject npcRogue;

    public List<PlayerMovement> playerList = new List<PlayerMovement>(4);
    List<PlayerMovement> playersAlive = new List<PlayerMovement>();
    public List<int> standings = new List<int>();
    int numPlayers;

    StartController startController;
    int warriorCount;
    int rogueCount;
    int rangerCount;
    int mageCount;

    bool isRoundOver;

    // Use this for initialization
    void Start() {
        isRoundOver = false;

        // Count the class numbers to instantiate an even number of NPCs
        warriorCount = 0;
        rogueCount = 0;
        rangerCount = 0;
        mageCount = 0;

        startController = GameObject.Find("StartController").GetComponent<StartController>();
        startController.ResetBeforeRound();
        
        numPlayers = playersAlive.Count;
        if (startController.isFreeForAllMode)
        {
            for (int i = 0; i < playerList.Capacity; i++)
            {
                // Instantiate the player to the character they have chosen, and set their player index to i+1
                GameObject playerObject = InstantiatePlayer(startController.players[i], i + 1);
                //playerList.Add(playerObject.GetComponent<PlayerMovement>());
                if (playerObject == null)
                {
                    playerList.Add(null);
                }
                else
                {
                    playerList.Add(playerObject.GetComponent<PlayerMovement>());
                }

                if (playerList[i] != null)
                {
                    playersAlive.Add(playerList[i]);
                }
            }

            InstantiateNPCs(npcWarrior, warriorCount, 8, 12);
            InstantiateNPCs(npcMage, mageCount, 8, 12);
            InstantiateNPCs(npcRanger, rangerCount, 8, 12);
            InstantiateNPCs(npcRogue, rogueCount, 8, 12);
            
        }
        else
        {
            foreach (int playerIndex in startController.team1)
            {
                Debug.Log(startController.players[playerIndex - 1]);
                GameObject playerObject = InstantiatePlayer(startController.players[playerIndex - 1], playerIndex);
                playerObject.tag = "Team1";

                PlayerMovement player = playerObject.GetComponent<PlayerMovement>();
                player.teamNumber = 1;

                playerList.Add(player);
                playersAlive.Add(player);

                Debug.Log(playerObject.tag);
            }
            foreach (int playerIndex in startController.team2)
            {
                Debug.Log(startController.players[playerIndex - 1]);
                GameObject playerObject = InstantiatePlayer(startController.players[playerIndex - 1], playerIndex);
                playerObject.tag = "Team2";

                PlayerMovement player = playerObject.GetComponent<PlayerMovement>();
                player.teamNumber = 2;

                playerList.Add(player);
                playersAlive.Add(player);

                Debug.Log(playerObject.tag);
            }
            foreach (string s in startController.players)
            {
                Debug.Log(s);
            }

            InstantiateNPCs(startController.teams[0], 0, 18, 22);
            InstantiateNPCs(startController.teams[1], 0, 18, 22);
        }


    }

    // Update is called once per frame
    void Update() {
        if (!isRoundOver && startController.isFreeForAllMode)
        {
            CheckForFFAWinner();
        }
        else if (!isRoundOver)
        {
            CheckForTeamDeathmatchWinner();
        }
    }


    GameObject InstantiatePlayer(string player, int playerIndex)
    {
        GameObject playerObject = null;

        if (player == "Warrior")
        {
            playerObject = (GameObject) GameObject.Instantiate(startController.warrior.gameObject, 
                new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);

            warriorCount++;
            playerObject.GetComponent<PlayerMovement>().SetPlayer(player, playerIndex);
            //return playerObject;
        }
        else if (player == "Mage")
        {
            playerObject = (GameObject) GameObject.Instantiate(startController.mage.gameObject,
                new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);

            mageCount++;

            playerObject.GetComponent<PlayerMovement>().SetPlayer(player, playerIndex);
            //return playerObject;
        }
        else if (player == "Rogue")
        {
            playerObject = (GameObject) GameObject.Instantiate(startController.rogue.gameObject, 
                new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);

            rogueCount++;

            playerObject.GetComponent<PlayerMovement>().SetPlayer(player, playerIndex);
            //return playerObject;
        }
        else if (player == "Ranger")
        {
            playerObject = (GameObject) GameObject.Instantiate(startController.ranger.gameObject, 
                new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);

            rangerCount++;

            playerObject.GetComponent<PlayerMovement>().SetPlayer(player, playerIndex);
            //return playerObject;
        }

        return playerObject;
    }

    void InstantiateNPCs(string npc, int classCount, int minNPC, int maxNPC)
    {
        if (npc == "Warrior")
        {
            InstantiateNPCs(npcWarrior, warriorCount, minNPC, maxNPC);
        }
        else if (npc == "Ranger")
        {
            InstantiateNPCs(npcRanger, rangerCount, minNPC, maxNPC);
        }
        else if (npc == "Mage")
        {
            InstantiateNPCs(npcMage, mageCount, minNPC, maxNPC);
        }
        else if (npc == "Rogue")
        {
            InstantiateNPCs(npcRogue, rogueCount, minNPC, maxNPC);
        }
    }

    void InstantiateNPCs(GameObject npcType, int classCount, int minNPC, int maxNPC)
    {
        // Number of NPCs to be generated with the number of players taken into account
        int numNPC = Random.Range(minNPC, maxNPC) - classCount;
        for (int i = 0; i < numNPC; i++)
        {
            GameObject npcObject = (GameObject) Instantiate(npcType, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
            startController.npcList.Add(npcObject);
        }
    }

    // Check for free for all winner
    void CheckForFFAWinner()
    {
        for (int i = 0; i < playersAlive.Count; i++)
        {
            if (playersAlive[i].isDead)
            {
                playersAlive.Remove(playersAlive[i]);
            }
        }
        
        if (playersAlive.Count == 1)
        {
            isRoundOver = true;
            startController.winner = playersAlive[0].playerIndex;
            Invoke("Victory", 3);
        }
    }

    void CheckForTeamDeathmatchWinner()
    {
        Debug.Log(playersAlive.Count);
        for (int i = 0; i < playersAlive.Count; i++)
        {
            if (playersAlive[i].isDead)
            {
                playersAlive.Remove(playersAlive[i]);
            }
        }

        bool oneTeamAlive = true;
        if (playersAlive.Count == 0)
        {
            oneTeamAlive = true;
        }
        else
        {
            for (int i = 0; i < playersAlive.Count - 1; i++)
            {
                if (playersAlive[i].teamNumber != playersAlive[i + 1].teamNumber)
                {
                    oneTeamAlive = false;
                    break;
                }
            }
        }

        if (oneTeamAlive)
        {
            isRoundOver = true;
            startController.winner = playersAlive[0].teamNumber;
            Invoke("Victory", 3);
        }
    }

    void PrintPlayersAlive()
    {
        for (int i = 0; i < playersAlive.Count; i++)
        {
            Debug.Log("Player " + playersAlive[i].playerIndex);
        }
    }

    void Victory()
    {
        startController.roundsPlayed++;
        SceneManager.LoadScene(3);
        
        for (int i = 0; i < 4; i++)
        {
            //Debug.Log("player" + (i + 1) + "score " + startController.playerScores[i]);
        }
    }
}
