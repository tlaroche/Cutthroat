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
        warriorCount = 0;
        rogueCount = 0;
        rangerCount = 0;
        mageCount = 0;

        startController = GameObject.Find("Controller").GetComponent<StartController>();

        InstantiatePlayer(startController.player1);
        //InstantiatePlayer(startController.player2);
        //InstantiatePlayer(startController.player3);
        //InstantiatePlayer(startController.player4);

        InstantiateNPCs(npcWarrior, warriorCount);
        InstantiateNPCs(npcMage, mageCount);
        InstantiateNPCs(npcRanger, rangerCount);
        InstantiateNPCs(npcRogue, rogueCount);
    }

    // Update is called once per frame
    void Update() {

    }

    void InstantiatePlayer(string player)
    {
        if (player == "Warrior")
        {
            GameObject.Instantiate(startController.warrior, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
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
        int numNPC = Random.Range(6, 10) - classCount;
        for (int i = 0; i < numNPC; i++)
        {
            Instantiate(npcType, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
        }
    }
}
