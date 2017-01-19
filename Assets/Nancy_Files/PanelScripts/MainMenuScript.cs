using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour //Functions for Main Menu Functionality and Animations
{
    public Button[] mainMenuButtons = new Button[5];
    Vector2[] startPos = new Vector2[5];
    Vector2[] endPos = new Vector2[5];
    float duration = 10.0f;

    public GameObject defaultSelectedObject;

    void Awake()
    {
        startPos[0] = new Vector2(-1400, mainMenuButtons[0].transform.position.y); //hardcode for optimization
        startPos[1] = new Vector2(1400, mainMenuButtons[1].transform.position.y);
        startPos[2] = new Vector2(-1400, mainMenuButtons[2].transform.position.y);
        startPos[3] = new Vector2(1400, mainMenuButtons[3].transform.position.y);
        startPos[4] = new Vector2(1400, mainMenuButtons[4].transform.position.y);

        endPos[0] = mainMenuButtons[0].transform.position;
        endPos[1] = mainMenuButtons[1].transform.position;
        endPos[2] = mainMenuButtons[2].transform.position;
        endPos[3] = mainMenuButtons[3].transform.position;
        endPos[4] = mainMenuButtons[4].transform.position;
    }

    void OnEnable()
    {
        GetComponent<UINavigationScript>().setDefaultGameObject(defaultSelectedObject);
        StartCoroutine(fancyButtonEasing(duration));
    }

    void OnDisable()
    {
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

            for (int i = 0; i < 5; i++)
            {
                mainMenuButtons[i].transform.position = Vector2.Lerp(startPos[i], endPos[i], lerpAmount);
            }
            yield return null;
        }
    }
}