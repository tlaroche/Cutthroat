using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MissionOverScript : MonoBehaviour
{
    public GameObject[] missionOverObjects = new GameObject[4];
    Vector2[] startPos = new Vector2[4];
    Vector2[] endPos = new Vector2[4];
    float duration = 10f;

    public Text missionNumText;
    public Text victoryOrFailText;

    //public StartController startController;

	void Start()
    {
        /*
        startPos[0] = new Vector2(-1400, missionOverObjects[0].transform.localPosition.y);
        startPos[1] = new Vector2(1400, missionOverObjects[1].transform.localPosition.y); //FUCK LOCALPOSITION AND POSITION
        startPos[2] = new Vector2(-1400, missionOverObjects[2].transform.localPosition.y);
        startPos[3] = new Vector2(-1400, missionOverObjects[3].transform.localPosition.y);

        endPos[0] = missionOverObjects[0].transform.localPosition;
        endPos[1] = missionOverObjects[1].transform.localPosition;
        endPos[2] = missionOverObjects[2].transform.localPosition;
        endPos[3] = missionOverObjects[3].transform.localPosition;
        */

        startPos[0] = new Vector2(-1400, 350);
        startPos[1] = new Vector2(1400, missionOverObjects[1].transform.localPosition.y); //hardcode for optimization
        startPos[2] = new Vector2(-1400, -380);
        startPos[3] = new Vector2(-1400, -380);

        endPos[0] = new Vector2(0, 350);
        endPos[1] = new Vector2(0, 0);
        endPos[2] = new Vector2(-300, -380);
        endPos[3] = new Vector2(300, -380);

        //startController = GameObject.Find("StartController").GetComponent<StartController>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GetComponent<UINavigationScript>().setDefaultGameObject(missionOverObjects[2]);
        }
    }

	void OnEnable ()
    {
        //changeMissionNumText();
        //VictoryOrFailText();

        StartCoroutine(fancyObjectEasing(duration));
    }

    void changeMissionNumText()
    {
        /*
        if (startController.gameMode == "MI1")
            missionNumText.text = "MISSION 1 OVER";
        if (startController.gameMode == "MI2")
            missionNumText.text = "MISSION 2 OVER";
            */
    }

    void VictoryOrFailText()
    {
        /*
        if (startController.failedMissionVariable)
            missionNumText.text = "You completed the mission!";
        else
            missionNumText.text = "You have failed the mission.";
            */
    }

    public void loadGameForTryAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void quitMissionsToMainMenu()
    {
        //startController.done = true;
        SceneManager.LoadScene(0);
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

    IEnumerator fancyObjectEasing(float duration)
    {
        for (float time = 0; time <= duration; time++)
        {
            float lerpAmount = time / duration;
            lerpAmount = Mathf.Sin(lerpAmount * Mathf.PI * 0.5f); //Easing in

            for (int i = 0; i < 4; i++)
            {
                missionOverObjects[i].transform.localPosition = Vector2.Lerp(startPos[i], endPos[i], lerpAmount);
            }
            yield return null;
        }
    }

}
