using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class StartController : MonoBehaviour {

    public static List<GameObject> npcList;
    public Texture startScreen;
    public Texture p1win;
    public Texture p2win;

    public readonly int PLAYER1_INDEX = 1;
    public readonly int PLAYER2_INDEX = 2;
    public readonly int PLAYER3_INDEX = 3;
    public readonly int PLAYER4_INDEX = 4;

    public GameObject warrior;
    public GameObject mage;
    public GameObject ranger;
    public GameObject rogue;

    public string player1;
    public string player2;
    public string player3;
    public string player4;

    float gameTimer;
    float countDown;
    bool isGameStarted;
    float npcDeathCooldown;

    public int winner;
    public bool done;


    // Iterator for the npc object names
    int index;

	// Use this for initialization
	void Start () {
        GameObject temp = GameObject.Find("Controller");
        if (temp.GetComponent<StartController>().done)
        {
            Destroy(temp);
        }
        done = true;
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            done = false;
        }


        index = 0;
        // List of all the unique npc gameobject names used for killing them off in sudden death
        //npcList = new ArrayList();
        npcList = new List<GameObject>();
        isGameStarted = false;
        gameTimer = 90f;

        // npcs will die off every X seconds when sudden death starts
        npcDeathCooldown = 5f;

    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }
	
	// Update is called once per frame
	void Update () {

        // Start screen
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            // You can combine these two if statements, but separated them for debugging purposes
            if (Input.GetButtonDown("Start" + PLAYER1_INDEX))
            {
                Debug.Log("loading scene 1 controller1 pressed start");
                SceneManager.LoadScene(1);
            }

            if (Input.GetButtonDown("Start" + PLAYER2_INDEX))
            {
                Debug.Log("loading scene 1 controller2 pressed start");
                SceneManager.LoadScene(1);
            }

        }
        // Game arena screen
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            // Start the game
            if (!isGameStarted)
            {
                isGameStarted = true;
                
            }
            else if (isGameStarted)
            {
                gameTimer -= Time.deltaTime;

                if (gameTimer <= 0) // Sudden death starts
                {
                    npcDeathCooldown -= Time.deltaTime;
                    if (npcDeathCooldown <= 0f)
                    {
                        // Reset death cooldown
                        npcDeathCooldown = 3f;
                        
                        // Going through the npc game object name arraylist and killing off all of the npcs 
                        if (index < npcList.Count)
                        {
                            GameObject npcToKill = npcList[index];
                            npcToKill.GetComponent<NPCMovement>().KillNPC();
                            index++;
                        }
                    }
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 3 && (Input.GetButtonDown("Start1") || Input.GetButtonDown("Start2")))
        {
            done = true;
            SceneManager.LoadScene(0);
        }

	}

    
    void OnGUI()
    {
        if (!isGameStarted && SceneManager.GetActiveScene().buildIndex == 0)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), startScreen, ScaleMode.ScaleToFit);
        }
        else if (isGameStarted && SceneManager.GetActiveScene().buildIndex == 2)
        {
            // Display timer
            int minutes = Mathf.FloorToInt(gameTimer / 60f);
            int seconds = Mathf.FloorToInt(gameTimer - minutes * 60);
            string time = string.Format("{0:0}:{1:00}", minutes, seconds);

            if (gameTimer <= 0)
            {
                time = string.Format("{0:0}:{1:00}", 0, 0);
            }
            GUI.Label(new Rect(10, 10, 250, 100), time);
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            switch(winner)
            {
                case 1:
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), p1win, ScaleMode.ScaleToFit);
                    break;
                case 2:
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), p2win, ScaleMode.ScaleToFit);
                    break;
            }
        }
    }
}
