using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeamDeathmatchScript : MonoBehaviour
{
    public GameObject[] TeamDeathmatchObjects = new GameObject[6];
    Vector2[] startPos = new Vector2[6];
    Vector2[] endPos = new Vector2[6];
    float duration = 10.0f;

    public GameObject thisPanel;
    public GameObject characterClassesPanel;
    public GameObject prevPanel;
    int counter = 0;

    public GameObject[] teamClassText = new GameObject[2];

    public GameObject readyPanel;
    bool isReadyPlaying;

    public string[] teams = new string[2];
    public bool[] isTeamReady = new bool[2];
    public GameObject[] playerQueueImages = new GameObject[2];
    bool isPartyReady = false;

    //temporary Variables to work with startController and gameController
    public StartController startController;

    void Awake()
    {
        startPos[0] = new Vector2(-1400, TeamDeathmatchObjects[0].transform.position.y);
        startPos[1] = new Vector2(1400, TeamDeathmatchObjects[1].transform.position.y); //hardcode for optimization
        startPos[2] = new Vector2(-1400, TeamDeathmatchObjects[2].transform.position.y);
        startPos[3] = new Vector2(-1400, TeamDeathmatchObjects[3].transform.position.y);
        startPos[4] = new Vector2(1400, TeamDeathmatchObjects[4].transform.position.y);
        startPos[5] = new Vector2(1400, TeamDeathmatchObjects[5].transform.position.y);

        endPos[0] = TeamDeathmatchObjects[0].transform.position;
        endPos[1] = TeamDeathmatchObjects[1].transform.position;
        endPos[2] = TeamDeathmatchObjects[2].transform.position;
        endPos[3] = TeamDeathmatchObjects[3].transform.position;
        endPos[4] = TeamDeathmatchObjects[4].transform.position;
        endPos[5] = TeamDeathmatchObjects[5].transform.position;
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
        else if (isPartyReady)
        {
            for(int i = 1; i < 5; i++)
            {
                if (Input.GetButtonDown("Start" + i))
                {
                    setTeamClassesInStartController();
                    startGame();
                }
            }
        }
    }

    void OnEnable()
    {
        TeamDeathmatchObjects[0].SetActive(true);
        StartCoroutine(fancyObjectEasing(duration));
        characterClassesPanel.SetActive(true);

        isReadyPlaying = false;

        teamClassText[0].SetActive(false);
        teamClassText[1].SetActive(false);
    }

    void OnDisable()
    {
        TeamDeathmatchObjects[0].SetActive(false);
        characterClassesPanel.SetActive(false);
        readyPanel.SetActive(false);

        teamClassText[0].SetActive(false);
        teamClassText[1].SetActive(false);
    }

    IEnumerator fancyObjectEasing(float duration)
    {
        for (float time = 0; time <= duration; time++)
        {
            float lerpAmount = time / duration;
            lerpAmount = Mathf.Sin(lerpAmount * Mathf.PI * 0.5f); //Easing in

            for (int i = 0; i < 6; i++)
            {
                TeamDeathmatchObjects[i].transform.position = Vector2.Lerp(startPos[i], endPos[i], lerpAmount);
            }
            yield return null;
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


    void checkIfPressedDpad() //Currently selects for all players, test ready state
    {
        for (int i = 0; i < 4; i++)
        {
            float inputDPadHorizontal = Input.GetAxisRaw("DPadHorizontal" + (i + 1));
            float inputDPadVertical = Input.GetAxisRaw("DPadVertical" + (i + 1));
            int teamNum = i < 2 ? 0 : 1;
            int otherTeamNum = i < 2 ? 1 : 0;

            if (inputDPadHorizontal == -1 && teams[otherTeamNum] != "Warrior")
            {
                Debug.Log("Pressed Dpad left");
                teams[teamNum] = "Warrior"; //warrior
                isTeamReady[teamNum] = true;
                teamClassText[teamNum].SetActive(true);
                teamClassText[teamNum].GetComponent<Text>().text = "WARRIOR";
                playerIsReady(playerQueueImages[teamNum]);
            }
            else if (inputDPadHorizontal == 1 && teams[otherTeamNum] != "Ranger")
            {
                Debug.Log("Pressed Dpad right");
                teams[teamNum] = "Ranger"; //Ranger
                isTeamReady[teamNum] = true;
                teamClassText[teamNum].SetActive(true);
                teamClassText[teamNum].GetComponent<Text>().text = "RANGER";
                playerIsReady(playerQueueImages[teamNum]);
            }
            else if (inputDPadVertical == 1 && teams[otherTeamNum] != "Mage")
            {
                Debug.Log("Pressed Dpad up");
                teams[teamNum] = "Mage"; //Mage
                isTeamReady[teamNum] = true;
                teamClassText[teamNum].SetActive(true);
                teamClassText[teamNum].GetComponent<Text>().text = "MAGE";
                playerIsReady(playerQueueImages[teamNum]);
            }
            else if (inputDPadVertical == -1 && teams[otherTeamNum] != "Rogue")
            {
                Debug.Log("Pressed Dpad down");
                teams[teamNum] = "Rogue"; //Rogue
                isTeamReady[teamNum] = true;
                teamClassText[teamNum].SetActive(true);
                teamClassText[teamNum].GetComponent<Text>().text = "ROGUE";
                playerIsReady(playerQueueImages[teamNum]);
            }
        }
    }

    void checkIfPressedB()
    {
        if (Input.GetButton("B1") || Input.GetButton("B2") || Input.GetButton("B3") || Input.GetButton("B4")) //Check is player holds B to back, go back to the match set-up screen (Only player 1 can do this)
        {
            counter++;
            Debug.Log(counter);
            if (counter > 60)
                goBackToPrevPanel();
        }
        else
        {
            counter = 0;
        }

        for (int i = 0; i < 4; i++)
        {
            if (Input.GetButtonDown("B" + (i + 1))) //if the player is already ready and pressing B, make that player not ready
            {
                int teamNum = i < 2? 0 : 1;
                
                if (isTeamReady[teamNum])
                {
                    isTeamReady[teamNum] = false;
                    teamClassText[teamNum].SetActive(false);
                    playerIsNotReady(playerQueueImages[teamNum]);
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
        for (int i = 0; i < 2; i++)
        {
            if (isTeamReady[i])
                playersReady++;
        }

        //Debug.Log(playersReady);
        if (playersReady == 2)
            isPartyReady = true;
        else
        {
            isPartyReady = false;
            isReadyPlaying = false;
            readyPanel.SetActive(false);
        }
    }

    void startGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    //temporary functions to work with startController and gameController
    public void setTeamClassesInStartController()
    {
        startController.team1.Add(1);
        startController.team1.Add(2);
        startController.team2.Add(3);
        startController.team2.Add(4);

        startController.teams[0] = teams[0];
        startController.teams[1] = teams[1];

        for(int i = 0; i < 4; i++)
        {
            int teamNum = i < 2 ? 0 : 1;
            startController.players.Add(teams[teamNum]);
        }
    }

    public void loadDefaultTeamStatesInMainMenu()
    {
        isTeamReady[0] = false;
        isTeamReady[1] = false;
    }

    public void loadDefaultTeamStatesBetweenRounds()
    {
        startController = startController = GameObject.Find("StartController").GetComponent<StartController>();

        isTeamReady[0] = false;
        isTeamReady[1] = false;

        startController.players.Clear();
        startController.team1.Clear();
        startController.team2.Clear();
    }
}
