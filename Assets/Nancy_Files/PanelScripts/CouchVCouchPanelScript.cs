using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CouchVCouchPanelScript : MonoBehaviour
{
    public GameObject[] couchVCouchObjects = new GameObject[3];
    Vector2[] startPos = new Vector2[3];
    Vector2[] endPos = new Vector2[3];
    public Text readyText;
    float shake = 0.1f;
    float shakePower = 10f;
    float duration = 10.0f;


    // Use this for initialization
    void Awake ()
    {
        startPos[0] = new Vector2(-1400, couchVCouchObjects[0].transform.localPosition.y); //hardcode for optimization
        startPos[1] = new Vector2(1400, couchVCouchObjects[1].transform.localPosition.y);
        startPos[2] = new Vector2(-1400, couchVCouchObjects[2].transform.localPosition.y);

        endPos[0] = couchVCouchObjects[0].transform.localPosition;
        endPos[1] = couchVCouchObjects[1].transform.localPosition;
        endPos[2] = couchVCouchObjects[2].transform.localPosition;
    }

    void OnEnable()
    {
        GetComponent<UINavigationScript>().setDefaultGameObject(couchVCouchObjects[2]);
        readyText.GetComponent<Text>().color = new Color(1, 1, 1, 0.25f);

        couchVCouchObjects[2].GetComponent<Button>().interactable = false;

        StartCoroutine(fancyObjectEasing(duration));
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            GetComponent<UINavigationScript>().setDefaultGameObject(couchVCouchObjects[2]);
    }

    IEnumerator fancyObjectEasing(float duration)
    {
        for (float time = 0; time <= duration; time++)
        {
            float lerpAmount = time / duration;
            lerpAmount = Mathf.Sin(lerpAmount * Mathf.PI * 0.5f); //Easing in

            for (int i = 0; i < 3; i++)
            {
                couchVCouchObjects[i].transform.localPosition = Vector2.Lerp(startPos[i], endPos[i], lerpAmount);
            }
            yield return null;
        }
    }

    public void shakeButtonIfNotInteractable()
    {
        if (couchVCouchObjects[2].GetComponent<Button>().interactable == false)
            StartCoroutine(couchVCouchObjects[2].GetComponent<ShakeEffectScript>().shakeEffect(shake, shakePower)); //Shake this.Panel
    }
}
