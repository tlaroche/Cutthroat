using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReadyAnimationScript : MonoBehaviour
{
    public Image[] readyImages = new Image[2];
    Vector2[] startPos = new Vector2[2];
    Vector2[] endPos = new Vector2[2];
    float duration = 10.0f;

    public GameObject readyToFightText;
    public GameObject pressStartText;

    // Use this for initialization
    void Awake ()
    {
        startPos[0] = new Vector2(-6000, readyImages[0].transform.localPosition.y); //hardcode for optimization
        startPos[1] = new Vector2(6000, readyImages[1].transform.localPosition.y);

        endPos[0] = readyImages[0].transform.localPosition;
        endPos[1] = readyImages[1].transform.localPosition;
    }

    void OnEnable()
    {
        StartCoroutine(fancyImageEasing(duration));
    }

    void OnDisable()
    {
        readyToFightText.SetActive(false);
        this.GetComponent<BlinkingEffectScript>().stopBlink();
    }

    IEnumerator fancyImageEasing(float duration)
    {
        for (float time = 0; time <= duration; time++)
        {
            float lerpAmount = time / duration;
            lerpAmount = Mathf.Sin(lerpAmount * Mathf.PI * 0.5f); //Easing in

            for (int i = 0; i < 2; i++)
            {
                readyImages[i].transform.localPosition = Vector2.Lerp(startPos[i], endPos[i], lerpAmount);
            }
            yield return null;
        }

        readyToFightText.SetActive(true);
        this.GetComponent<BlinkingEffectScript>().startBlink(pressStartText);
    }
}
