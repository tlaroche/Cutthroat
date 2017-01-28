using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RoundOverScript : MonoBehaviour
{
    public GameObject[] roundOverObjects = new GameObject[6];
    Vector2[] startPos = new Vector2[6];
    Vector2[] endPos = new Vector2[6];
    float duration = 10.0f;

    public Text numOfRoundsText;
    public Text winnerText;
    public GameObject mvpText;
    public GameObject[] playerKillsText = new GameObject[4];

    public GameObject[] modePanels = new GameObject[2];
    public GameObject characterClassesPanel;
    public GameObject menuTextImage;

    //temporary variables for startController and gameController
    StartController startController;

    // Use this for initialization
    void Awake ()
    {
        startPos[0] = new Vector2(-1400, roundOverObjects[0].transform.localPosition.y);
        startPos[1] = new Vector2(1400, roundOverObjects[1].transform.localPosition.y); //hardcode for optimization
        startPos[2] = new Vector2(-1400, roundOverObjects[2].transform.localPosition.y);
        startPos[3] = new Vector2(1400, roundOverObjects[3].transform.localPosition.y);
        startPos[4] = new Vector2(-1400, roundOverObjects[4].transform.localPosition.y);
        startPos[5] = new Vector2(-1400, roundOverObjects[5].transform.localPosition.y);

        endPos[0] = roundOverObjects[0].transform.localPosition;
        endPos[1] = roundOverObjects[1].transform.localPosition;
        endPos[2] = roundOverObjects[2].transform.localPosition;
        endPos[3] = roundOverObjects[3].transform.localPosition;
        endPos[4] = roundOverObjects[4].transform.localPosition;
        endPos[5] = roundOverObjects[5].transform.localPosition;

        startController = startController = GameObject.Find("StartController").GetComponent<StartController>(); //DontDestoryOnLoad() from game scene
    }
	
    void OnEnable()
    {
        if (startController.roundsPlayed != startController.numOfRounds)
            setUpRoundOverPanel();
        else
        {
            setUpMatchOverPanel();
            roundOverObjects[4].SetActive(false);
            endPos[5] = new Vector2(0, roundOverObjects[5].transform.localPosition.y);
        }

        StartCoroutine(fancyObjectEasing(duration));
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            GetComponent<UINavigationScript>().setDefaultGameObject(roundOverObjects[4]);
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
                roundOverObjects[i].transform.localPosition = Vector2.Lerp(startPos[i], endPos[i], lerpAmount);
            }
            yield return null;
        }
    }


    //taken from UINavigationScript
    public void setDefaultGameObject(GameObject defaultSelectedObject)
    {
        StartCoroutine(setDefaultGameObjectOperation(defaultSelectedObject));
    }

    IEnumerator setDefaultGameObjectOperation(GameObject defaultSelectedObject)
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultSelectedObject.gameObject, null);
    }

    public void OnSelect()
    {
        GameObject currentObject = EventSystem.current.currentSelectedGameObject;
        Vector2 scaleOffset = new Vector2(1.05f, 1.05f);
        currentObject.transform.localScale = scaleOffset;

        currentObject.GetComponent<Image>().color = new Color32(240, 255, 160, 255);
    }

    public void OnDeselect()
    {
        GameObject currentObject = EventSystem.current.currentSelectedGameObject;
        Vector2 scaleOffset = new Vector2(1, 1);
        currentObject.transform.localScale = scaleOffset;

        currentObject.GetComponent<Image>().color = new Color32(184, 184, 184, 255);
    }

    public void quitRoundsToMainMenu()
    {
        SceneManager.LoadSceneAsync(0); //load main menu
    }

    public void continueToModePanels()
    {
        if(startController.isFreeForAllMode)
        {
            modePanels[0].SetActive(true); //free for all panel
            menuTextImage.GetComponentInChildren<Text>().text = "FREE FOR ALL";
            modePanels[0].GetComponent<FreeForAllMenuScript>().loadDefaultPlayerStatesBetweenRounds();
        }
        else
        {
            modePanels[1].SetActive(true); //team deathmatch panel
            menuTextImage.GetComponentInChildren<Text>().text = "TEAM DEATHMATCH";
            modePanels[1].GetComponent<TeamDeathmatchScript>().loadDefaultTeamStatesBetweenRounds();
        }
        menuTextImage.SetActive(true);
        characterClassesPanel.SetActive(true);
    }

    void setUpRoundOverPanel()
    {
        numOfRoundsText.text = "Round " + startController.roundsPlayed + " of " + startController.numOfRounds;

        if (startController.isFreeForAllMode)
            winnerText.text = "Player " + startController.winner + " is the last one standing";
        else
        {
            winnerText.text = "Team " + startController.winner + " is victorious";
            startController.teamScores[startController.winner - 1]++; //change this
        }

        for (int i = 0; i < 4; i++)
        {
            playerKillsText[i].GetComponent<Text>().text =  startController.playerScores[i].ToString();
            playerKillsText[i].SetActive(false);
            playerKillsText[i].SetActive(true);
        }
        
        setDefaultGameObject(roundOverObjects[4]);
    }

    void setUpMatchOverPanel()
    {
        numOfRoundsText.text = "Round " + startController.roundsPlayed + " of " + startController.numOfRounds;
        
        int currentMaxKills = startController.playerScores[0];
        int currentWinner = 1; //most amount of kills
        
        for (int i = 1; i < 4; i ++)
        {
            if (startController.playerScores[i] > currentMaxKills)
            {
                currentMaxKills = startController.playerScores[i];
                currentWinner = i + 1;
            }
        }

        int teamWinner = 0;
        if (!startController.isFreeForAllMode)
        {
            if (startController.teamScores[0] > startController.teamScores[1])
                teamWinner = 1;
            else
                teamWinner = 2;
        }

        if (startController.isFreeForAllMode)
            winnerText.text = "Player " + currentWinner + " is VICTORIOUS!";
        else
        {
            winnerText.text = "Team " + teamWinner + " is VICTORIOUS!";
            endPos[2] = new Vector2(winnerText.transform.localPosition.x, 175);
            mvpText.SetActive(true);
            mvpText.GetComponent<Text>().text = "Player " + currentWinner + " is the MVP";
        }
        
        for (int i = 0; i < 4; i++)
        {
            playerKillsText[i].GetComponent<Text>().text = startController.playerScores[i].ToString();
            playerKillsText[i].SetActive(false);
            playerKillsText[i].SetActive(true);
        }

        setDefaultGameObject(roundOverObjects[5]);
    }
    
}
