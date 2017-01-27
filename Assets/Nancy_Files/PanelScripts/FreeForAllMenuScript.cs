using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FreeForAllMenuScript : MonoBehaviour //Functions for FreeFOrAll Fucntionality and Animations
{
    public GameObject[] freeForAllObjects = new GameObject[6];
    Vector2[] startPos = new Vector2[6];
    Vector2[] endPos = new Vector2[6];
    float duration = 10.0f;

    public GameObject thisPanel;
    public GameObject characterClassesPanel;
    public GameObject prevPanel;
    int counter = 0;
    
    public GameObject readyPanel;
    bool isReadyPlaying;

    //public int numRounds;
    public int numPlayers;
    bool[] isPlayerActive = new bool[4];
    public bool[] isPlayerReady = new bool[4];
    public string[] playerSelection = new string[4]; //0 Warrior, 1 Ranger, 2 Mage, 3 Rogue
    public GameObject[] playerQueueImages = new GameObject[4];
    bool isPartyReady = false;
    
    //temporary Variables to work with startController and gameController
    StartController startController;

    void Awake()
    {
        startPos[0] = new Vector2(-1400, freeForAllObjects[0].transform.position.y);
        startPos[1] = new Vector2(1400, freeForAllObjects[1].transform.position.y); //hardcode for optimization
        startPos[2] = new Vector2(-1400, freeForAllObjects[2].transform.position.y);
        startPos[3] = new Vector2(-1400, freeForAllObjects[3].transform.position.y);
        startPos[4] = new Vector2(1400, freeForAllObjects[4].transform.position.y);
        startPos[5] = new Vector2(1400, freeForAllObjects[5].transform.position.y);

        endPos[0] = freeForAllObjects[0].transform.position;
        endPos[1] = freeForAllObjects[1].transform.position;
        endPos[2] = freeForAllObjects[2].transform.position;
        endPos[3] = freeForAllObjects[3].transform.position;
        endPos[4] = freeForAllObjects[4].transform.position;
        endPos[5] = freeForAllObjects[5].transform.position;

        startController = startController = GameObject.Find("StartController").GetComponent<StartController>();
    }
    

    void OnEnable()
    {
        freeForAllObjects[0].SetActive(true);
        StartCoroutine(fancyObjectEasing(duration));
        characterClassesPanel.SetActive(true);

        isReadyPlaying = false;

        loadDefaultPlayerStates();
    }

    void OnDisable()
    {
        freeForAllObjects[0].SetActive(false);
        characterClassesPanel.SetActive(false);
        readyPanel.SetActive(false);
    }

    void loadDefaultPlayerStates()
    {
        //numRounds = prevPanel.GetComponent<MatchSetUpMenuScript>().numRounds;
        numPlayers = prevPanel.GetComponent<MatchSetUpMenuScript>().numPlayers;
        isPlayerActive = prevPanel.GetComponent<MatchSetUpMenuScript>().isPlayerActive;
        isPlayerReady[0] = false;
        isPlayerReady[1] = false;
        isPlayerReady[2] = false;
        isPlayerReady[3] = false;

        for (int i = 0; i < 4; i++)
        {
            if (isPlayerActive[i])
                playerIsNotReady(playerQueueImages[i]);
            else
                playerNotActive(playerQueueImages[i]);
        }
    }

    void Update()
    {
        checkIfPressedB();
        checkIfPressedDpad();
        checkIfPartyIsReady();

        if (isPartyReady && !isReadyPlaying)
        {
            readyPanel.SetActive(true);
            isReadyPlaying = true;
        }
        else if(isPartyReady)
        {
            if(Input.GetButtonDown("Start1"))
            {
                setPlayerClassesInStartController();
                startGame();
            }
        }
    }

    void playerIsNotReady(GameObject statePanel)
    {
        statePanel.GetComponent<Image>().color = new Color32(0, 0, 255, 255);
    }

    void playerIsReady(GameObject statePanel)
    {
        statePanel.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
    }

    void playerNotActive(GameObject statePanel)
    {
       statePanel.GetComponent<Image>().color = new Color32(128, 128, 128, 255);
    }

    void checkIfPressedDpad() //Currently selects for all players, test ready state
    {
        for(int i = 0; i < 4; i++)
        {
            float inputDPadHorizontal = Input.GetAxisRaw("DPadHorizontal" + (i + 1));
            float inputDPadVertical = Input.GetAxisRaw("DPadVertical" + (i + 1));

            if (isPlayerActive[i] && inputDPadHorizontal == -1)
            {
                Debug.Log("Pressed Dpad left");
                playerSelection[i] = "Warrior"; //warrior
                isPlayerReady[i] = true;
                playerIsReady(playerQueueImages[i]);
            }
            else if (isPlayerActive[i] && inputDPadHorizontal == 1)
            {
                Debug.Log("Pressed Dpad right");
                playerSelection[i] = "Ranger"; //Ranger
                isPlayerReady[i] = true;
                playerIsReady(playerQueueImages[i]);
            }
            else if (isPlayerActive[i] && inputDPadVertical == 1)
            {
                Debug.Log("Pressed Dpad up");
                playerSelection[i] = "Mage"; //Mage
                isPlayerReady[i] = true;
                playerIsReady(playerQueueImages[i]);
            }
            else if (isPlayerActive[i] && inputDPadVertical == -1)
            {
                Debug.Log("Pressed Dpad down");
                playerSelection[i] = "Rogue"; //Rogue
                isPlayerReady[i] = true;
                playerIsReady(playerQueueImages[i]);
            }
        }
    }

    void checkIfPressedB() 
    {
        if (Input.GetButton("B1")) //Check is player holds B to back, go back to the match set-up screen (Only player 1 can do this)
        {
            counter++;
            //Debug.Log(counter);
            if (counter > 60)
                goBackToPrevPanel();
        }
        else
        {
            counter = 0;
        }

        for(int i = 0; i < 4; i++)
        {
            if (Input.GetButtonDown("B" + (i + 1))) //if the player is already ready and pressing B, make that player not ready
            {
                if (isPlayerReady[i])
                {
                    isPlayerReady[i] = false;
                    playerIsNotReady(playerQueueImages[i]);
                }
            }
        }
    }

    void goBackToPrevPanel()
    {
        prevPanel.SetActive(true);
        thisPanel.SetActive(false);
    }

    void checkIfPartyIsReady()
    {
        int playersReady = 0;
        for(int i = 0; i < 4; i++)
        {
            if (isPlayerReady[i])
                playersReady++;
        }

        //Debug.Log(playersReady);
        if (playersReady == numPlayers)
            isPartyReady = true;
        else
        {
            isPartyReady = false;
            isReadyPlaying = false;
            readyPanel.SetActive(false);
        }
    }

    IEnumerator fancyObjectEasing(float duration)
    {
        for (float time = 0; time <= duration; time++)
        {
            float lerpAmount = time / duration;
            lerpAmount = Mathf.Sin(lerpAmount * Mathf.PI * 0.5f); //Easing in

            for (int i = 0; i < 6; i++)
            {
                freeForAllObjects[i].transform.position = Vector2.Lerp(startPos[i], endPos[i], lerpAmount);
            }
            yield return null;
        }
    }

    void startGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    //temporary functions to work with startController and gameController
    public void setPlayerClassesInStartController()
    {
        for(int i = 0; i < 4; i++)
        {
            startController.players.Add(playerSelection[i]);
        }
    }
}
