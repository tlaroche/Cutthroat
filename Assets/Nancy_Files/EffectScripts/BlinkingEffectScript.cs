using UnityEngine;
using System.Collections;

public class BlinkingEffectScript : MonoBehaviour
{
    GameObject blinkingObject;
    IEnumerator co;

    public void startBlink(GameObject thisObject)
    {
        blinkingObject = thisObject;
        co = Blink();
        StartCoroutine(co);
    }

    public void stopBlink()
    {
        StopCoroutine(co);
    }

    IEnumerator Blink()
    {
        while (true)
        {
            blinkingObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            blinkingObject.SetActive(false);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
