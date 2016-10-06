using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartController : MonoBehaviour {

    public static ArrayList npcGameObjectNames;

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
    bool isGameStarted;
    float npcDeathCooldown;

    // Iterator for the npc object names
    int index;

	// Use this for initialization
	void Start () {
        index = 0;
        // List of all the unique npc gameobject names used for killing them off in sudden death
        npcGameObjectNames = new ArrayList();
        isGameStarted = false;
        gameTimer = 0f;

        // npcs will die off every X seconds when sudden death starts
        npcDeathCooldown = 1f;
	}

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        /*if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            DontDestroyOnLoad(gameObject);
        }*/
    }
	
	// Update is called once per frame
	void Update () {
        // Start screen
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            // You can combine these two if statements, but separated them for debugging purposes
            if (Input.GetButtonDown("Controller1_Start"))
            {
                Debug.Log("loading scene 1 controller1 pressed start");
                SceneManager.LoadScene(1);
            }

            if (Input.GetButtonDown("Controller2_Start"))
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

                gameTimer += Time.deltaTime;
                //Debug.Log(gameTimer);

                if (gameTimer >= 3f) // Sudden death starts
                {
                    npcDeathCooldown -= Time.deltaTime;
                    if (npcDeathCooldown <= 0f)
                    {
                        // Reset death cooldown
                        npcDeathCooldown = 1f;
                        
                        // Going through the npc game object name arraylist and killing off all of the npcs 
                        if (index < npcGameObjectNames.Count)
                        {
                            GameObject npcToKill = GameObject.Find((string)npcGameObjectNames[index]);
                            npcToKill.GetComponent<NPCMovement>().KillNPC();
                            index++;
                        }
                    }
                }
            }
        }
	}

    
    void OnGUI()
    {
        if (isGameStarted)
        {
            // Display timer
            int minutes = Mathf.FloorToInt(gameTimer / 60f);
            int seconds = Mathf.FloorToInt(gameTimer - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

            GUI.Label(new Rect(10, 10, 250, 100), niceTime);
        }
    }
}
