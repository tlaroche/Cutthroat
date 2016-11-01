using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    
    public GameObject npcWarrior;
    public GameObject npcMage;
    public GameObject npcRanger;
    public GameObject npcRogue;

    List<PlayerMovement> playerList = new List<PlayerMovement>(4);
    List<PlayerMovement> playersAlive = new List<PlayerMovement>();
    public List<int> standings = new List<int>();
    int numPlayers;

    StartController startController;
    int warriorCount;
    int rogueCount;
    int rangerCount;
    int mageCount;

    // Use this for initialization
    void Start() {

        // Count the class numbers to instantiate an even number of NPCs
        warriorCount = 0;
        rogueCount = 0;
        rangerCount = 0;
        mageCount = 0;

        startController = GameObject.Find("StartController").GetComponent<StartController>();
        startController.ResetEverything();
        


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

        numPlayers = playersAlive.Count;
        
        InstantiateNPCs(npcWarrior, warriorCount);
        InstantiateNPCs(npcMage, mageCount);
        InstantiateNPCs(npcRanger, rangerCount);
        InstantiateNPCs(npcRogue, rogueCount);
        
        
    }

    // Update is called once per frame
    void Update() {
        CheckForWinner();
    }


    GameObject InstantiatePlayer(string player, int playerIndex)
    {
        GameObject playerObject = null;
        Debug.Log(playerIndex + player);

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

    void InstantiateNPCs(GameObject npcType, int classCount)
    {
        // Number of NPCs to be generated with the number of players taken into account
        int numNPC = Random.Range(8, 12) - classCount;
        for (int i = 0; i < numNPC; i++)
        {
            GameObject npcObject = (GameObject) Instantiate(npcType, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
            startController.npcList.Add(npcObject);
        }
    }

    void CheckForWinner()
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
            startController.winner = playersAlive[0].playerIndex;
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
        SceneManager.LoadScene(3);
    }
}
