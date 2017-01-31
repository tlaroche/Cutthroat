using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    public List<GameObject> npcList;
    /*
    public Texture startScreen;
    //public Texture gameOptionsScreen;
    public Texture roundOver;
    public Texture team1win;
    public Texture team2win;
    public Texture p1win;
    public Texture p2win;
    public Texture p3win;
    public Texture p4win;
    public Texture[] numbers;
    */
    /*
    public Texture[] gameOptionsScreens = new Texture[5];
    public Texture gameModeSelection;
    */

    //public bool isFreeForAllMode; ->deprecated

    public string gameMode;

    public readonly float GAME_TIME = 90f;
    public readonly float NPC_DEATH_COOLDOWN = 3f;

    public bool displayGameOptions;
    public bool displayGameModeSelect;
    public int numOfRounds;
    public int roundsPlayed;
    public int numControllersConnected;

    public readonly int PLAYER1_INDEX = 1;
    public readonly int PLAYER2_INDEX = 2;
    public readonly int PLAYER3_INDEX = 3;
    public readonly int PLAYER4_INDEX = 4;

    public GameObject warrior;
    public GameObject mage;
    public GameObject ranger;
    public GameObject rogue;
    public GameObject tutorialRogue;
    
    /*
    public int p1score;
    public int p2score;
    public int p3score;
    public int p4score;
    */

    public int[] playerScores = new int[4];
    public int[] teamScores = new int[2];
    public List<string> players;
    public List<string> teams;
    public List<int> team1;
    public List<int> team2;

    float gameTimer;
    bool isGameStarted;
    bool suddenDeathStarted;
    float npcDeathCooldown;
    float npcDeathTimer;

    public int winner;
    public bool done;


    bool inputReset;

	// Use this for initialization
	void Start ()
    {
        Cursor.visible = false;
        // Check to see if the set of rounds is over, destroy current game if over.
        GameObject temp = GameObject.Find("StartController");
        if (temp.GetComponent<StartController>().done)
        {
            Destroy(temp);
        }
        done = true;
        if (SceneManager.GetActiveScene().buildIndex != 2) //if not victory scene 
        {
            done = false;
        }


        // Initialize the players' character selections to empty
        players = new List<string>(4);
        teams = new List<string>(2);
        teams.Add("");
        teams.Add("");

        // Get the number of controllers that are connected
        string[] joystickNames = Input.GetJoystickNames();
        for (int i = 0; i < joystickNames.Length; i++)
        {
            if (joystickNames[i].Contains("XBOX") || joystickNames[i].Contains("Xbox"))
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

        // Initialize scores of teams to zero
        teamScores[0] = 0;
        teamScores[1] = 0;

        // List of all the unique npc gameobject names used for killing them off in sudden death
        //npcList = new ArrayList();
        npcList = new List<GameObject>();
        isGameStarted = false;
        suddenDeathStarted = false;
        gameTimer = GAME_TIME;

        displayGameOptions = false;
        displayGameModeSelect = false;
        numOfRounds = 3;
        roundsPlayed = 0;

        inputReset = false;

        // npcs will die off every X seconds when sudden death starts
        npcDeathTimer = NPC_DEATH_COOLDOWN;
    }

    public void ResetBeforeRound()
    {
        npcList.Clear();
        isGameStarted = false;
        suddenDeathStarted = false;
        gameTimer = GAME_TIME;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
	
    
	// Update is called once per frame
	void Update ()
    {

        /*
        // Main Menu
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

                if (Input.GetButtonDown("Start1") || Input.GetButtonDown("Start2") || Input.GetButtonDown("Start3") || Input.GetButtonDown("Start4"))
                {
                    SceneManager.LoadScene(1);
                }
                else if (Input.GetButtonDown("Back1") || Input.GetButtonDown("Back2") || Input.GetButtonDown("Back3") || Input.GetButtonDown("Back4"))
                {
                    displayGameOptions = false;
                }

            }
            else if (displayGameModeSelect)
            {
                if (Input.GetButtonDown("A1") || Input.GetButtonDown("A2") || Input.GetButtonDown("A3") || Input.GetButtonDown("A4"))
                {
                    displayGameOptions = true;
                    isFreeForAllMode = true;
                }
                else if (Input.GetButtonDown("B1") || Input.GetButtonDown("B2") || Input.GetButtonDown("B3") || Input.GetButtonDown("B4"))
                {
                    isFreeForAllMode = false;
                    SceneManager.LoadScene(4);
                }
                else if (Input.GetButtonDown("Back1") || Input.GetButtonDown("Back2") || Input.GetButtonDown("Back3") || Input.GetButtonDown("Back4"))
                {
                    displayGameModeSelect = false;
                }
            }
            else
            {
                if (Input.GetButtonDown("Start1") || Input.GetButtonDown("Start2") || Input.GetButtonDown("Start3") || Input.GetButtonDown("Start4"))
                {
                    displayGameModeSelect = true;
                }
            }
        }
        // Game scene
        else */ if (SceneManager.GetActiveScene().buildIndex == 1)
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
                    List<GameObject> LivingList = npcList;
                    npcDeathTimer -= Time.deltaTime;
                    if (npcDeathTimer <= 0f)
                    {
                        // Reset death cooldown
                        npcDeathTimer = NPC_DEATH_COOLDOWN;

                        if (npcList.Count > 0)
                        {
                            GameObject npcToKill;
                            int index = Random.Range(0, LivingList.Count);
                            npcToKill = LivingList[index];
                            npcToKill.GetComponent<NPCMovement>().KillNPC();
                            LivingList.RemoveAt(index);
                        }
                    }
                }
            }
        }
        
        //if victory scene, check if num rounds is finished, check if a player presses start, go to main menu
        if (SceneManager.GetActiveScene().buildIndex == 2 /*&& (Input.GetButtonDown("Start1") || Input.GetButtonDown("Start2") || Input.GetButtonDown("Start3") || Input.GetButtonDown("Start4"))*/)
        {
            if (gameMode == "FFA")
            {
                //if (numOfRounds > 1)
                if (roundsPlayed < numOfRounds)
                {
                    //roundsPlayed++;
                    //SceneManager.LoadScene(1);
                }

                else
                {
                    done = true;
                    //SceneManager.LoadScene(0);
                }
            }
            else if (gameMode == "TDM")
            {
                done = true;
                //SceneManager.LoadScene(0);
            }
            
        }
        

	}

    void OnGUI()
    {
        GUIStyle timerStyle = new GUIStyle();
        timerStyle.fontSize = 50;
        /*
        if (!isGameStarted && SceneManager.GetActiveScene().buildIndex == 0) //screen textures
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
        else */ if (isGameStarted && SceneManager.GetActiveScene().buildIndex == 1)
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
        /*
        if (SceneManager.GetActiveScene().buildIndex == 3) //player winner screen free for all
        {
            if (isFreeForAllMode)
            {
                //switch(winner)
                {
                    case 1:
                        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), p1win, ScaleMode.ScaleToFit);
                        break;
                    case 2:
                        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), p2win, ScaleMode.ScaleToFit);
                        break;
                    case 3:
                        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), p3win, ScaleMode.ScaleToFit);
                        break;
                    case 4:
                        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), p4win, ScaleMode.ScaleToFit);
                        break;
                }
                
                Debug.Log(Screen.height + " " + Screen.width);

                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), roundOver, ScaleMode.ScaleToFit);
                //GUI.DrawTexture(new Rect(Screen.width / 2, Screen.height/4, 50, 50), numbers[roundsPlayed]);
                GUI.DrawTexture(new Rect(925, 170, 45, 45), numbers[roundsPlayed]);
                GUI.DrawTexture(new Rect(1055, 170, 45, 45), numbers[numOfRounds]);

                GUI.DrawTexture(new Rect(695, 320, 45, 45), numbers[winner]);

                DrawPlayerScore(0);
                DrawPlayerScore(1);
                DrawPlayerScore(2);
                DrawPlayerScore(3);
                
                //COMMENT STARTED HERE
                switch (winner)
                {
                    case 1:
                        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), roundOver, ScaleMode.ScaleToFit);
                        break;
                    case 2:
                        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), p1win, ScaleMode.ScaleToFit);
                        break;
                    case 3:
                        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), p1win, ScaleMode.ScaleToFit);
                        break;
                    case 4:
                        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), p1win, ScaleMode.ScaleToFit);
                        break;
                }
                //COMMENT ENDED HERE
                GUI.DrawTexture(new Rect(Screen.width / 2 - 25, Screen.height / 2, 50, 100), numbers[winner]);
            }
            else //player winner team deathmatch
            {
                switch (winner)
                {
                    case 1:
                        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), team1win, ScaleMode.ScaleToFit);
                        break;
                    case 2:
                        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), team2win, ScaleMode.ScaleToFit);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }
            }
        }
        */
    }
    

        /*
    void DrawPlayerScore(int playerIndex) //x, y coordinates to draw on screen
    {
        int x;
        int y = 675;
        switch(playerIndex)
        {
            case 0:
                x = 550;
                break;
            case 1:
                x = 800;
                break;
            case 2:
                x = 1050;
                break;
            case 3:
                x = 1300;
                break;
            default:
                x = 0;
                break;
        }
        if (players[playerIndex] != "")
        {
            GUI.DrawTexture(new Rect(x, y, 90, 90), numbers[playerScores[playerIndex]]);
        }
        else
        {
            GUI.DrawTexture(new Rect(x, y, 90, 90), numbers[10]);
        }
    }
    */

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



    //temporary functions to work with startController and gameController
    public void setGameModeFFA()
    {
        gameMode = "FFA";
    }

    public void setGameModeTDM()
    {
        gameMode = "TDM";
    }

    public void setGameModeCVC()
    {
        gameMode = "CVC";
    }

    public void setGameModeMI1()
    {
        gameMode = "MI1";
    }

    public void setGameModeMI2()
    {
        gameMode = "MI2";
    }
}
