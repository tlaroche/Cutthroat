using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UINavigationScript : MonoBehaviour //UI Manager to turn panels on and off and set selected item
{
    public GameObject prevPanels;
    public GameObject currentPanels;

    void Update()
    {
        //'z' is B
        //'x' is A
        //'c' is X
        //'v' is Y
        if (Input.GetKeyDown(KeyCode.Z)) //backbutton, go to previous panel
        {
            prevPanels.SetActive(true);
            currentPanels.SetActive(false);
        }
    }

    public void setDefaultGameObject(GameObject defaultSelectedObject)
    {
        StartCoroutine(setDefaultGameObjectOperation(defaultSelectedObject));
    }

    IEnumerator setDefaultGameObjectOperation(GameObject defaultSelectedObject)
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultSelectedObject.gameObject, null);
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
}
