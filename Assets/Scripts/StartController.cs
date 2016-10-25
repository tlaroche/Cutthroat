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
    bool suddenDeathStarted;
    float npcDeathCooldown;
    float npcDeathTimer;

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
        suddenDeathStarted = false;
        gameTimer = 90f;

        // npcs will die off every X seconds when sudden death starts
        npcDeathCooldown = 5f;
        npcDeathTimer = npcDeathCooldown;
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
                if (!suddenDeathStarted)
                {
                    gameTimer -= Time.deltaTime;

                    if (gameTimer <= 0) // Sudden death starts
                    {
                        suddenDeathStarted = true;
                        gameTimer = 0f;
                    }
                }

                if (suddenDeathStarted)
                {
                    //gameTimer += Time.deltaTime;

                    npcDeathTimer -= Time.deltaTime;
                    if (npcDeathTimer <= 0f)
                    {
                        // Reset death cooldown
                        npcDeathTimer = npcDeathCooldown;

                        if (npcList.Count > 0)
                        {
                            GameObject npcToKill = npcList[Random.Range(0, npcList.Count)];
                            npcToKill.GetComponent<NPCMovement>().KillNPC();
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
        GUIStyle timerStyle = new GUIStyle();
        timerStyle.fontSize = 50;

        if (!isGameStarted && SceneManager.GetActiveScene().buildIndex == 0)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), startScreen, ScaleMode.ScaleToFit);
        }
        else if (isGameStarted && SceneManager.GetActiveScene().buildIndex == 2)
        {
            // Display the game timer
            if (gameTimer <= 15f)
            {
                timerStyle.normal.textColor = Color.red;
            }
            else
            {
                timerStyle.normal.textColor = Color.white;
            }

            GUI.Label(new Rect(10, 10, 250, 100), GetTimerString(), timerStyle);
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

    string GetTimerString()
    {
        int minutes = Mathf.FloorToInt(gameTimer / 60f);
        int seconds = Mathf.FloorToInt(gameTimer - minutes * 60);
        return (string.Format("{0:0}:{1:00}", minutes, seconds));
    }
}
