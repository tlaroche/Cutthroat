using UnityEngine;
using System.Collections;

public class ShakeEffectScript : MonoBehaviour //Coroutine script for shake this object
{
    float decreaseFactor = 1.0f;

    public IEnumerator shakeEffect(float shake, float shakePower) 
    {
        Vector2 originalPosition = transform.localPosition;

        while (shake > 0)
        {
            transform.localPosition += Random.insideUnitSphere * shakePower;
            shake -= Time.deltaTime * decreaseFactor;
            yield return null;
            transform.localPosition = originalPosition;
            yield return null;
        }
    }
}
