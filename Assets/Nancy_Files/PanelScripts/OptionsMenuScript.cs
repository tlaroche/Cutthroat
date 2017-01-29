using UnityEngine;
//using UnityEditor;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsMenuScript : MonoBehaviour //Functions for FreeFOrAll Fucntionality and Animations
{
    public GameObject menuTextImage;
    public Image[] optionImages = new Image[5];
    Vector2[] startPos = new Vector2[5];
    Vector2[] endPos = new Vector2[5];
    float duration = 10.0f; //10 frames

    Button fullScreenButton;
    public int isFullScreen;

    Slider volumeSlider;

    public GameObject defaultSelectedObject;

    void Awake()
    {
        //check if the the user has fullScreen prefs, if not, default is fullscreen
        fullScreenButton = optionImages[0].GetComponentInChildren<Button>();
        isFullScreen = PlayerPrefs.HasKey("isFullScreen") ? PlayerPrefs.GetInt("isFullScreen") : 1;
        changePlayerFullScreenPrefs();

        //check if the the user has volume prefs, if not, default is screen resolution is 1
        volumeSlider = optionImages[1].GetComponentInChildren<Slider>();
        volumeSlider.value = PlayerPrefs.HasKey("volume") ? PlayerPrefs.GetFloat("volume") : 1;

        startPos[0] = new Vector2(-1400, optionImages[0].transform.position.y); //hardcode for optimization
        startPos[1] = new Vector2(1400, optionImages[1].transform.position.y);
        startPos[2] = new Vector2(-1400, optionImages[2].transform.position.y);
        startPos[3] = new Vector2(1400, optionImages[3].transform.position.y);
        startPos[4] = new Vector2(-1400, optionImages[4].transform.position.y);

        endPos[0] = optionImages[0].transform.position;
        endPos[1] = optionImages[1].transform.position;
        endPos[2] = optionImages[2].transform.position;
        endPos[3] = optionImages[3].transform.position;
        endPos[4] = optionImages[4].transform.position;
    }

    void OnEnable()
    {
        GetComponent<UINavigationScript>().setDefaultGameObject(defaultSelectedObject);
        StartCoroutine(fancyImageEasing(duration));
    }

    void OnDisable()
    {
        menuTextImage.SetActive(false);

        if (EventSystem.current.currentSelectedGameObject != null)
            OnDeselect();
    }
    public void changeFullScreen()
    {
        isFullScreen = -isFullScreen;
    }

    public void changePlayerFullScreenPrefs()
    {
        if (isFullScreen == 1)
        {
            Screen.fullScreen = true;
            fullScreenButton.GetComponentInChildren<Text>().text = "ON";
        }
        else
        {
            Screen.fullScreen = false;
            fullScreenButton.GetComponentInChildren<Text>().text = "OFF";
        }
    }

    public void setPlayerFullScreenPrefs()
    {
        PlayerPrefs.SetInt("isFullScreen", isFullScreen);
    }

    public void setPlayerVolumePrefs()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    public void OnSelect()
    {
        GameObject currentObject = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        Vector2 scaleOffset = new Vector2(1.05f, 1.05f);
        currentObject.transform.localScale = scaleOffset;
        
        currentObject.GetComponent<Image>().color = new Color32(240, 255, 160, 255);
    }

    public void OnDeselect()
    {
        GameObject currentObject = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        Vector2 scaleOffset = new Vector2(1, 1);
        currentObject.transform.localScale = scaleOffset;

        currentObject.GetComponent<Image>().color = new Color32(184, 184, 184, 255);
    }

    IEnumerator fancyImageEasing(float duration)
    {
        for (float time = 0; time <= duration; time++)
        {
            float lerpAmount = time / duration;
            lerpAmount = Mathf.Sin(lerpAmount * Mathf.PI * 0.5f); //Easing in
            for (int i = 0; i < 5; i++)
            {
                optionImages[i].transform.position = Vector2.Lerp(startPos[i], endPos[i], lerpAmount);
            }
            yield return null;
        }
    }
}
