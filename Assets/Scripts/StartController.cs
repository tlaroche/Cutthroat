using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class StartController : MonoBehaviour {

    public List<GameObject> npcList;
    public Texture startScreen;
    //public Texture gameOptionsScreen;
    public Texture p1win;
    public Texture p2win;
    public Texture one;
    public GameObject[] numbers;

    public Texture[] gameOptionsScreens = new Texture[5];
    public Texture gameModeSelection;

    public readonly float GAME_TIME = 90f;
    public readonly float NPC_DEATH_COOLDOWN = 5f;

    public bool displayGameOptions;
    public bool displayGameModeSelect;
    public int numOfRounds;
    public int numControllersConnected;

    public readonly int PLAYER1_INDEX = 1;
    public readonly int PLAYER2_INDEX = 2;
    public readonly int PLAYER3_INDEX = 3;
    public readonly int PLAYER4_INDEX = 4;

    public GameObject warrior;
    public GameObject mage;
    public GameObject ranger;
    public GameObject rogue;
    
    public int p1score;
    public int p2score;
    public int p3score;
    public int p4score;

    public int[] playerScores = new int[4];

    public List<string> players;

    float gameTimer;
    bool isGameStarted;
    bool suddenDeathStarted;
    float npcDeathCooldown;
    float npcDeathTimer;

    public int winner;
    public bool done;


    bool inputReset;
    

	// Use this for initialization
	void Start () {
        // Check to see if the set of rounds is over, destroy current game if over.
        GameObject temp = GameObject.Find("StartController");
        if (temp.GetComponent<StartController>().done)
        {
            Destroy(temp);
        }
        done = true;
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            done = false;
        }


        // Initialize the players' character selections to empty
        players = new List<string>(4);
        InitCharacterSelectionTest();

        // Get the number of controllers that are connected
        string[] joystickNames = Input.GetJoystickNames();
        for (int i = 0; i < joystickNames.Length; i++)
        {
            if (joystickNames[i].Contains("XBOX"))
            {
                numControllersConnected++;
            }
        }
        Debug.Log("controllers connected: " + numControllersConnected);

        // Initialize scores of players to zero;
        for (int i = 0; i < playerScores.Length; i++)
        {
            playerScores[i] = 0;
        }
        
        // List of all the unique npc gameobject names used for killing them off in sudden death
        //npcList = new ArrayList();
        npcList = new List<GameObject>();
        isGameStarted = false;
        suddenDeathStarted = false;
        gameTimer = GAME_TIME;

        displayGameOptions = false;
        displayGameModeSelect = false;
        numOfRounds = 3;

        inputReset = false;

        // npcs will die off every X seconds when sudden death starts
        npcDeathTimer = NPC_DEATH_COOLDOWN;
    }

    public void ResetBeforeRound()
    {
        npcList.Clear();
        isGameStarted = false;
        suddenDeathStarted = false;
        gameTimer = 90f;
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
            if (displayGameOptions)
            {
                float input = Input.GetAxis("Horizontal1");
                if (!inputReset && input  <-0.5f)
                {
                    numOfRounds--;
                    numOfRounds = Mathf.Clamp(numOfRounds, 1, 5);
                    inputReset = true;
                }
                else if (!inputReset && input > 0.5f)
                {
                    numOfRounds++;
                    numOfRounds = Mathf.Clamp(numOfRounds, 1, 5);
                    inputReset = true;
                }
                else if (inputReset && input < 0.5f && input > -0.5f)
                {
                    inputReset = false;
                }
                //Debug.Log(numOfRounds);

                if (Input.GetButtonDown("Start1"))
                {
                    SceneManager.LoadScene(1);
                }

            }
            else if (displayGameModeSelect)
            {
                if (Input.GetButtonDown("A1") || Input.GetButtonDown("A2") || Input.GetButtonDown("A3") || Input.GetButtonDown("A4"))
                {
                    displayGameOptions = true;
                }
                else if (Input.GetButtonDown("B1") || Input.GetButtonDown("B2") || Input.GetButtonDown("B3") || Input.GetButtonDown("B4"))
                {
                    SceneManager.LoadScene(4);
                }
            }
            else
            {
                if (Input.GetButtonDown("Start1") || Input.GetButtonDown("Start2") || Input.GetButtonDown("Start3") || Input.GetButtonDown("Start4"))
                {
                    //SceneManager.LoadScene(1);
                    displayGameModeSelect = true;
                }
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
                        npcDeathTimer = NPC_DEATH_COOLDOWN;

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
            if (numOfRounds > 1)
            {
                numOfRounds--;
                SceneManager.LoadScene(1);
            }
            else
            {
                done = true;
                SceneManager.LoadScene(0);
            }
        }

	}

    
    void OnGUI()
    {
        GUIStyle timerStyle = new GUIStyle();
        timerStyle.fontSize = 50;

        if (!isGameStarted && SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (displayGameOptions)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), gameOptionsScreens[numOfRounds - 1], ScaleMode.ScaleToFit);
            }
            else if (displayGameModeSelect)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), gameModeSelection, ScaleMode.ScaleToFit);
            }
            else
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), startScreen, ScaleMode.ScaleToFit);
            }
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
            Debug.Log("width: " + Screen.width + " height: " + Screen.height);
            switch(winner)
            {
                case 1:
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), p1win, ScaleMode.ScaleToFit);
                    break;
                case 2:
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), p2win, ScaleMode.ScaleToFit);
                    break;
            }

            GUI.DrawTexture(new Rect(Screen.width / 2 - 25, Screen.height / 2, 50, 100), one);
        }
    }

    string GetTimerString()
    {
        int minutes = Mathf.FloorToInt(gameTimer / 60f);
        int seconds = Mathf.FloorToInt(gameTimer - minutes * 60);
        return (string.Format("{0:0}:{1:00}", minutes, seconds));
    }

    public void InitCharacterSelectionTest()
    {
        players.Clear();
        for (int i = 0; i < players.Capacity; i++)
        {
            if (i == 1)
            {
                players.Add("Warrior");
            }
            else
            {
                players.Add("");
            }
        }
    }

    public void InitCharacterSelection()
    {
        players.Clear();
        for (int i = 0; i < 4; i++)
        {
            players.Add("");
        }
    }
}
