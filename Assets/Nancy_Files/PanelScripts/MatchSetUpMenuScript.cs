﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MatchSetUpMenuScript : MonoBehaviour
{
    public GameObject[] matchSetupObjects = new GameObject[4];
    public Text readyText;
    float shake = 0.1f;
    float shakePower = 10f;
    Vector2[] startPos = new Vector2[4];
    Vector2[] endPos = new Vector2[4];
    float duration = 10.0f;

    public int numPlayers = 0;
    public bool[] isPlayerActive = new bool[4];
    public GameObject[] playerQueueImages = new GameObject[4];

    public int numRounds;
    public Text numRoundsText;
    public GameObject[] arrowText = new GameObject[2];
    public bool axisInUse = false;

    public int mode; //0 freeForAll, 1 teamDeathmatch, 2 mission1
    public GameObject[] modePanel = new GameObject[2];
    public GameObject characterPanel;
    public Text modePanelText;

    public GameObject couchPanel;
    
    bool isPartyReady = false;

    public GameObject analogStickImage;
    Vector2 analogStartPos;
    Vector2 analogEndPos;
    int analogStickImageDirection;
    float durationAnalogAnimation = 20f;
    IEnumerator co; //stop co-routine

    //Temporary Variables for StartController and GameController
    public StartController startController;

    void Awake()
    {
        numRounds = 3;

        //defaultSelectedObject = defaultSelectedObject.GetComponent<Selectable>();
        startPos[0] = new Vector2(-1400, matchSetupObjects[0].transform.position.y); //hardcode for optimization
        startPos[1] = new Vector2(1400, matchSetupObjects[1].transform.position.y);
        startPos[2] = new Vector2(-1400, matchSetupObjects[2].transform.position.y);
        startPos[3] = new Vector2(1400, matchSetupObjects[3].transform.position.y);

        endPos[0] = matchSetupObjects[0].transform.position;
        endPos[1] = matchSetupObjects[1].transform.position;
        endPos[2] = matchSetupObjects[2].transform.position;
        endPos[3] = matchSetupObjects[3].transform.position;

        analogStartPos = new Vector2(290, analogStickImage.transform.localPosition.y);
        analogEndPos = new Vector2(300, analogStickImage.transform.localPosition.y); ;
        analogStickImageDirection = 1;
        co = fancyAnalogAnimation(durationAnalogAnimation); 
        
    }


    public void setMode(int index) //0 freeForAll, 1 teamDeathmatch
    {
        mode = index;
    }
    
    void OnEnable()
    {
        if(mode == 0 || mode == 1)
            GetComponent<UINavigationScript>().setDefaultGameObject(matchSetupObjects[2]);
        else if(mode == 2)
            GetComponent<UINavigationScript>().setDefaultGameObject(matchSetupObjects[3]);

        readyText.GetComponent<Text>().color = new Color(1, 1, 1, 0.25f);

        matchSetupObjects[3].GetComponent<Button>().interactable = false;

        StartCoroutine(fancyObjectEasing(duration));
        loadDefaultMatchSetup();
  
        isPartyReady = false;

        StartCoroutine(co);
    }

    void OnDisable()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            GetComponent<UINavigationScript>().OnDeselect();
        }

        StopCoroutine(co);
    }

    void Update()
    {
        checkIfPressedStart();

        if (mode == 0)  //free for all
        {
            checkIfPartyIsReadyFreeForAll();
            checkIfInputHorizontal();

            if (EventSystem.current.currentSelectedGameObject == null)
                GetComponent<UINavigationScript>().setDefaultGameObject(matchSetupObjects[2]);
        }
        else if (mode == 1)
        {
            checkIfPartyIsReadyTeamDeathMatch();
            checkIfInputHorizontal();

            if (EventSystem.current.currentSelectedGameObject == null)
                GetComponent<UINavigationScript>().setDefaultGameObject(matchSetupObjects[2]);
        }
        else if (mode == 2)
        {
            checkIfPartyIsReadyCouch();

            if (EventSystem.current.currentSelectedGameObject == null)
                GetComponent<UINavigationScript>().setDefaultGameObject(matchSetupObjects[3]);
        }

        

        if (isPartyReady)
        {
            matchSetupObjects[3].GetComponentInChildren<Button>().interactable = true;
            readyText.GetComponent<Text>().color = new Color(1, 1, 1, 1);
        }
    }


    void playerIsNotActive(GameObject statePanel) //Will override sprites later
    {
        statePanel.GetComponent<Image>().color = new Color32(0, 0, 255, 255);
    }

    void playerIsActive(GameObject statePanel)
    {
        statePanel.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
    }

    void checkIfPressedStart()
    {
        for(int i = 0; i < 4; i++)
        {
            if (Input.GetButtonDown("Start" + (i + 1)))
            {
                isPlayerActive[i] = !isPlayerActive[i];
                if (isPlayerActive[i])
                {
                    numPlayers++;
                    playerIsActive(playerQueueImages[i]);
                }
                else
                {
                    numPlayers--;
                    playerIsNotActive(playerQueueImages[i]);
                }
            }
        }

    }

    void checkIfInputHorizontal() //only first player can change num of rounds
    {
        if(EventSystem.current.currentSelectedGameObject == matchSetupObjects[2])
        {
            int horizontaAnalognput = 0;
            int horizontalDPadInput = 0;

            for(int i = 0; i < 4; i++)
            {
                int newHorizontaAnalognput = (int)Input.GetAxisRaw("Horizontal" + (i + 1));
                int newHorizontalDPadInput = (int)Input.GetAxisRaw("DPadHorizontal" + (i + 1)); //returns -1, 0 or 1

                if (newHorizontaAnalognput != 0)
                    horizontaAnalognput = newHorizontaAnalognput;
                else if (newHorizontalDPadInput != 0)
                    horizontalDPadInput = newHorizontalDPadInput;
            }

            int horizontalInput;
            if (horizontaAnalognput != 0)
                horizontalInput = horizontaAnalognput;
            else
                horizontalInput = horizontalDPadInput;

            if (horizontalInput == 0)
                axisInUse = false;

            if (!axisInUse && horizontalInput != 0)
            {
                axisInUse = true;

                if (horizontalInput == -1 && numRounds > 1)
                    numRounds += horizontalInput;
                else if (horizontalInput == 1 && numRounds < 5)
                    numRounds += horizontalInput;

                if (numRounds == 5)
                    arrowText[1].SetActive(false);
                else if (numRounds == 1)
                    arrowText[0].SetActive(false);
                else
                {
                    arrowText[0].SetActive(true);
                    arrowText[1].SetActive(true);
                }

                numRoundsText.GetComponent<Text>().text = "" + numRounds;
            }
        }
    }

    void checkIfPartyIsReadyFreeForAll()
    {
        isPartyReady = false; //you need more than one player and player one is active

        if (numPlayers > 1)
        {
            isPartyReady = true;
        }
        else
        {
            matchSetupObjects[3].GetComponent<Button>().interactable = false;
            readyText.GetComponent<Text>().color = new Color(1, 1, 1, 0.25f);
        }
    }

    void checkIfPartyIsReadyTeamDeathMatch()
    {
        isPartyReady = false; //you need more than one player and player one is active

        if (numPlayers > 3)
        {
            isPartyReady = true;
        }
        else
        {
            matchSetupObjects[3].GetComponent<Button>().interactable = false;
            readyText.GetComponent<Text>().color = new Color(1, 1, 1, 0.25f);
        }
    }

    void checkIfPartyIsReadyCouch()
    {
        isPartyReady = false; //you need more than one player and player one is active

        if (numPlayers > 0)
        {
            isPartyReady = true;
        }
        else
        {
            matchSetupObjects[3].GetComponent<Button>().interactable = false;
            readyText.GetComponent<Text>().color = new Color(1, 1, 1, 0.25f);
        }
    }

    void loadDefaultMatchSetup()
    {
        numPlayers = 0;

        //numRounds = 3;
        numRoundsText.GetComponent<Text>().text = "" + numRounds;

        isPlayerActive[0] = false;
        isPlayerActive[1] = false;
        isPlayerActive[2] = false;
        isPlayerActive[3] = false;

        playerIsNotActive(playerQueueImages[0]); //BLUE for not present, and GREEN for ready
        playerIsNotActive(playerQueueImages[1]);
        playerIsNotActive(playerQueueImages[2]);
        playerIsNotActive(playerQueueImages[3]);
    }

    public void switchToModePanels()
    {
        switch (mode)
        {
            case 0:
                modePanelText.GetComponent<Text>().text = "FREE FOR ALL";
                modePanel[0].GetComponent<FreeForAllMenuScript>().loadDefaultPlayerStatesInMainMenu();
                modePanel[0].SetActive(true);
                characterPanel.SetActive(true);
                break;
            case 1:
                modePanelText.GetComponent<Text>().text = "TEAM DEATHMATCH";
                modePanel[1].GetComponent<TeamDeathmatchScript>().loadDefaultTeamStatesInMainMenu();
                modePanel[1].SetActive(true);
                characterPanel.SetActive(true);
                break;
            case 2:
                couchPanel.SetActive(true);
                break;

        }
    }

    IEnumerator fancyObjectEasing(float duration)
    {
        for (float time = 0; time <= duration; time++)
        {
            float lerpAmount = time / duration;
            lerpAmount = Mathf.Sin(lerpAmount * Mathf.PI * 0.5f); //Easing in

            for (int i = 0; i < 4; i++)
            {
                matchSetupObjects[i].transform.position = Vector2.Lerp(startPos[i], endPos[i], lerpAmount);
            }
            yield return null;
        }
    }

    IEnumerator fancyAnalogAnimation(float duration)
    {
        while(true)
        {
            analogStickImageDirection *= -1;

            if (analogStickImageDirection == 1)
            {
                for (float time = 0; time <= duration; time++)
                {
                    float lerpAmount = time / duration;
                    lerpAmount = Mathf.Sin(lerpAmount * Mathf.PI * 0.5f); //Easing in

                    for (int i = 0; i < 4; i++)
                    {
                        analogStickImage.transform.localPosition = Vector2.Lerp(analogStartPos, analogEndPos, lerpAmount);
                    }
                    yield return null;
                }
            }
            else
            {
                for (float time = 0; time <= duration; time++)
                {
                    float lerpAmount = time / duration;
                    lerpAmount = Mathf.Sin(lerpAmount * Mathf.PI * 0.5f); //Easing in

                    for (int i = 0; i < 4; i++)
                    {
                        analogStickImage.transform.localPosition = Vector2.Lerp(analogEndPos, analogStartPos, lerpAmount);
                    }
                    yield return null;
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
        
    }

    public void shakeButtonIfNotInteractable()
    {
        if(matchSetupObjects[3].GetComponent<Button>().interactable == false)
            StartCoroutine(matchSetupObjects[3].GetComponent<ShakeEffectScript>().shakeEffect(shake, shakePower)); //Shake this.Panel
    }


    //temporary functions to work with startController and gameController
    public void setVariablesInStartController()
    {
        startController.numOfRounds = numRounds;
    }

    public void setUpForPvp()
    {
        GetComponent<UINavigationScript>().setDefaultGameObject(matchSetupObjects[2]);
        matchSetupObjects[2].SetActive(true);
    }

    public void setUpforCouch()
    {
        GetComponent<UINavigationScript>().setDefaultGameObject(matchSetupObjects[3]);
        matchSetupObjects[2].SetActive(false);
    }

    

}
