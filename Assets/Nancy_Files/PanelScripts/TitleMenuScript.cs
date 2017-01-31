using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleMenuScript: MonoBehaviour //Functions for Title Fucntionality and Animations
{
    public GameObject[] gamePanels = new GameObject[5];

    public Image[] titleImage = new Image[2];
    float duration = 20.0f; //3 frames
    Vector2[] startPos = new Vector2[2];
    Vector2[] endPos = new Vector2[2];

    public GameObject thisPanel;
    float shakePower = 100.0f;
    float shake = 0.1f;

    public GameObject blinkingText;
    public GameObject pressBToQuitText;

    public GameObject pvpPanel;

    bool readyToStart = false;


    void Awake() //Jank way of loading prefs
    {
        gamePanels[4].SetActive(true); //"load" settings panel
        gamePanels[4].SetActive(false);

        startPos[0] = new Vector2(-1200, titleImage[0].transform.position.y);
        startPos[1] = new Vector2(1200, titleImage[1].transform.position.y);

        endPos[0] = titleImage[0].transform.position;
        endPos[1] = titleImage[1].transform.position;
    }
    
    void OnEnable()
    {
        readyToStart = false;
        StartCoroutine(fancyTitleEasing(duration, shake, shakePower));
    }

    void OnDisable()
    {
        blinkingText.SetActive(false);
        pressBToQuitText.SetActive(false);
    }

    void Update()
    {
        for(int i = 0; i < 4; i++)
        {
            if (Input.GetButtonDown("B" + (i + 1)))//Press any button to go to menu
            {
                Debug.Log("Quitting application");
                Application.Quit();
            }

            if (Input.GetButtonDown("Start" + (i + 1)) && readyToStart)//Press any button to go to menu
            {
                this.GetComponent<BlinkingEffectScript>().stopBlink();
                gamePanels[0].SetActive(false);
                gamePanels[1].SetActive(true);
            }
        }
    }

    IEnumerator fancyTitleEasing(float duration, float shake, float shakePower)
    {
        for (float time = 0; time <= duration; time++)
        {
            float lerpAmount = time / duration;
            lerpAmount = 1f - Mathf.Cos(lerpAmount * Mathf.PI * 0.5f);

            for(int i = 0; i < 2; i++)
                titleImage[i].transform.position = Vector2.Lerp(startPos[i], endPos[i], lerpAmount);
            yield return null;
        }

        StartCoroutine(thisPanel.GetComponent<ShakeEffectScript>().shakeEffect(shake, shakePower)); //Shake this.Panel
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<BlinkingEffectScript>().startBlink(blinkingText);
        pressBToQuitText.SetActive(true);
        readyToStart = true;
    }
}
