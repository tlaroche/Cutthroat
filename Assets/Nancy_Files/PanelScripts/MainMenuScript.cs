using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour //Functions for Main Menu Functionality and Animations
{
    public GameObject menuTextImage;
    Vector2 menuTextImageStartPos;
    Vector2 menuTextImageEndPos;
    public GameObject thisPanel;
    int numChildren;
    GameObject[] mainMenuObjects;
    Vector2[] startPos;
    Vector2[] endPos;
    float duration = 10.0f;

    public GameObject defaultSelectedObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<UINavigationScript>().setDefaultGameObject(defaultSelectedObject);
        }
    }

    void Awake()
    {
        numChildren = thisPanel.transform.childCount;
        mainMenuObjects = new GameObject[numChildren];
        startPos = new Vector2[numChildren];
        endPos = new Vector2[numChildren];

        menuTextImageStartPos = new Vector2(-1400, menuTextImage.transform.position.y);
        menuTextImageEndPos = menuTextImage.transform.position;
        for (int i = 0; i < numChildren; i++)
        {
            mainMenuObjects[i] = thisPanel.transform.GetChild(i).gameObject;
        }

        int inverter = -1;
        int startPosition = 1400;
        for (int i = 0; i < numChildren; i++)
        {
            startPosition *= inverter;
            startPos[i] = new Vector2(startPosition, mainMenuObjects[i].transform.position.y);
        }

        for (int i = 0; i < numChildren; i++)
            endPos[i] = mainMenuObjects[i].transform.position;

    }

    void OnEnable()
    {
        if(!(thisPanel.name == "Panel_MainMenu" || thisPanel.name == "Panel_MatchSetupMenu"))
        {
            menuTextImage.SetActive(true);

            if(thisPanel.name == "Panel_PvP")
                menuTextImage.GetComponentInChildren<Text>().text = "PvP";
            else if(thisPanel.name == "Panel_Missions")
                menuTextImage.GetComponentInChildren<Text>().text = "MISSIONS";
        }
          
        GetComponent<UINavigationScript>().setDefaultGameObject(defaultSelectedObject);
        StartCoroutine(fancyButtonEasing(duration));
    }

    void OnDisable()
    {
        menuTextImage.SetActive(false);

        if (EventSystem.current.currentSelectedGameObject != null)
        {
            defaultSelectedObject = EventSystem.current.currentSelectedGameObject;
            GetComponent<UINavigationScript>().OnDeselect();
        }
    }

    IEnumerator fancyButtonEasing(float duration)
    {
        for (float time = 0; time <= duration; time++)
        {
            float lerpAmount = time / duration;
            lerpAmount = Mathf.Sin(lerpAmount * Mathf.PI * 0.5f); //Easing in

            menuTextImage.transform.position = Vector2.Lerp(menuTextImageStartPos, menuTextImageEndPos, lerpAmount);
            for (int i = 0; i < numChildren; i++)
            {
                mainMenuObjects[i].transform.position = Vector2.Lerp(startPos[i], endPos[i], lerpAmount);
            }
            yield return null;
        }
    }

}