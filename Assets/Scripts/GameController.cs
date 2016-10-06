using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject npcWarrior;
    public GameObject npcMage;
    public GameObject npcRanger;
    public GameObject npcRogue;

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

        startController = GameObject.Find("Controller").GetComponent<StartController>();

        InstantiatePlayer(startController.player1);/*, startController.PLAYER1_INDEX*/
        //InstantiatePlayer(startController.player2, startController.PLAYER2_INDEX);
        //InstantiatePlayer(startController.player3, startController.PLAYER3_INDEX);
        //InstantiatePlayer(startController.player4, startController.PLAYER4_INDEX);

        InstantiateNPCs(npcWarrior, warriorCount);
        InstantiateNPCs(npcMage, mageCount);
        InstantiateNPCs(npcRanger, rangerCount);
        InstantiateNPCs(npcRogue, rogueCount);


        for (int i = 0; i < StartController.npcGameObjectNames.Count; i++)
        {
            Debug.Log(StartController.npcGameObjectNames[i]);
        }
        
    }

    // Update is called once per frame
    void Update() {

    }


    void InstantiatePlayer(string player)//, int playerIndex*/)
    {
        if (player == "Warrior")
        {
            GameObject playerObject = (GameObject) GameObject.Instantiate(startController.warrior, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
            warriorCount++;
            
        }
        else if (player == "Mage")
        {
            GameObject.Instantiate(startController.mage, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
            mageCount++;
        }
        else if (player == "Rogue")
        {
            GameObject.Instantiate(startController.rogue, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
            rogueCount++;
        }
        else if (player == "Ranger")
        {
            GameObject.Instantiate(startController.ranger, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
            rangerCount++;
        }
    }

    void InstantiateNPCs(GameObject npcType, int classCount)
    {
        // Number of NPCs to be generated with the number of players taken into account
        int numNPC = Random.Range(6, 10) - classCount;
        for (int i = 0; i < numNPC; i++)
        {
            GameObject npcObject = (GameObject) Instantiate(npcType, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
            npcObject.name = npcObject.GetInstanceID().ToString();

            StartController.npcGameObjectNames.Add(npcObject.name);
        }
    }
}
