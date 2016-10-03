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
	void Start () {
        warriorCount = 0;
        rogueCount = 0;
        rangerCount = 0;
        mageCount = 0;

        startController = GameObject.Find("Controller").GetComponent<StartController>();

        if (startController.player1 == "Warrior")
        {
            GameObject.Instantiate(startController.warrior, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
            warriorCount++;
        }
        else if (startController.player1 == "Mage")
        {
            GameObject.Instantiate(startController.mage, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
            mageCount++;
        }
        else if (startController.player1 == "Rogue")
        {
            GameObject.Instantiate(startController.rogue, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
            rogueCount++;
        }
        else if (startController.player1 == "Ranger")
        {
            GameObject.Instantiate(startController.ranger, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
            rangerCount++;
        }

        
        int num = Random.Range(6, 10) - warriorCount;
        for (int i = 0; i < num; i++)
        {
            Instantiate(npcWarrior, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
        }

        num = Random.Range(6, 10) - mageCount;
        for (int i = 0; i < num; i++)
        {
            Instantiate(npcMage, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
        }

        num = Random.Range(6, 10) - rogueCount;
        for (int i = 0; i < num; i++)
        {
            Instantiate(npcRogue, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
        }

        num = Random.Range(6, 10) - rangerCount;
        for (int i = 0; i < num; i++)
        {
            Instantiate(npcRanger, new Vector2(Random.Range(-29f, 29f), Random.Range(-29f, 29f)), Quaternion.identity);
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
