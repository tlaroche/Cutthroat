using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MissionsMatchUpScript : MonoBehaviour
{
    public GameObject[] missionMatchUpObjects = new GameObject[3];
    public Text readyText;
    float duration = 10f;
    float shake = 0.1f;
    float shakePower = 10f;
    Vector2[] startPos = new Vector2[3];
    Vector2[] endPos = new Vector2[3];

    public int numPlayers = 0;
    public bool[] isPlayerActive = new bool[4];
    public GameObject[] playerQueueImages = new GameObject[4];

    public GameObject readyButton;
    bool isPartyReady = false;

    public StartController startController;
	// Use this for initialization
	void Start ()
    {

        startPos[0] = new Vector2(-1400, 280); //hardcode for optimization
        startPos[1] = new Vector2(1400, 0);
        startPos[2] = new Vector2(-1400, -270);

        endPos[0] = new Vector2(0,  280);
        endPos[1] = new Vector2(0, 0);
        endPos[2] = new Vector2(0, -270);
    }
	
    void OnEnable()
    {
        GetComponent<UINavigationScript>().setDefaultGameObject(missionMatchUpObjects[2]);
        readyText.GetComponent<Text>().color = new Color(1, 1, 1, 0.25f);

        missionMatchUpObjects[2].GetComponent<Button>().interactable = false;

        StartCoroutine(fancyObjectEasing(duration));
        loadDefaultMatchSetup();

        isPartyReady = false;
    }

    void OnDiasble()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            GetComponent<UINavigationScript>().OnDeselect();
        }
    }

	// Update is called once per frame
	void Update ()
    {
        checkIfPressedStart();
        checkIfPartyIsReady();

        if (isPartyReady)
        {
            missionMatchUpObjects[2].GetComponentInChildren<Button>().interactable = true;
            readyText.GetComponent<Text>().color = new Color(1, 1, 1, 1);
        }

        if (EventSystem.current.currentSelectedGameObject != null)
        {
            GetComponent<UINavigationScript>().setDefaultGameObject(missionMatchUpObjects[2]);
        }
    }

    IEnumerator fancyObjectEasing(float duration)
    {
        for (float time = 0; time <= duration; time++)
        {
            float lerpAmount = time / duration;
            lerpAmount = Mathf.Sin(lerpAmount * Mathf.PI * 0.5f); //Easing in

            for (int i = 0; i < 3; i++)
            {
                missionMatchUpObjects[i].transform.localPosition = Vector2.Lerp(startPos[i], endPos[i], lerpAmount);
            }
            yield return null;
        }
    }

    void checkIfPressedStart()
    {
        for (int i = 0; i < 4; i++)
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

    void playerIsNotActive(GameObject statePanel) //Will override sprites later
    {
        statePanel.GetComponent<Image>().color = new Color32(0, 0, 255, 255);
    }

    void playerIsActive(GameObject statePanel)
    {
        statePanel.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
    }

    void loadDefaultMatchSetup()
    {
        numPlayers = 0;

        isPlayerActive[0] = false;
        isPlayerActive[1] = false;
        isPlayerActive[2] = false;
        isPlayerActive[3] = false;

        playerIsNotActive(playerQueueImages[0]); //BLUE for not present, and GREEN for ready
        playerIsNotActive(playerQueueImages[1]);
        playerIsNotActive(playerQueueImages[2]);
        playerIsNotActive(playerQueueImages[3]);
    }

    public void shakeButtonIfNotInteractable()
    {
        if (missionMatchUpObjects[2].GetComponent<Button>().interactable == false)
            StartCoroutine(missionMatchUpObjects[2].GetComponent<ShakeEffectScript>().shakeEffect(shake, shakePower)); //Shake this.Panel
    }

    public void setVariablesForStartController()
    {
       if(startController.gameMode == "MI1")
        {
            startController.numOfRounds = 1;
            for (int i = 0; i < 4; i++)
            {
                if (isPlayerActive[i])
                    startController.players.Add("Rogue");
                else
                    startController.players.Add("");
            }

            SceneManager.LoadScene(1);
        }
    }


    void checkIfPartyIsReady()
    {
        isPartyReady = false; //you need more than one player and player one is active

        if (numPlayers > 0)
        {
            isPartyReady = true;
        }
        else
        {
            missionMatchUpObjects[2].GetComponent<Button>().interactable = false;
            readyText.GetComponent<Text>().color = new Color(1, 1, 1, 0.25f);
        }
    }
}
