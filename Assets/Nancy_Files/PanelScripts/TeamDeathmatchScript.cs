using UnityEngine;
using System.Collections;

public class TeamDeathmatchScript : MonoBehaviour
{
    public GameObject[] TeamDeathmatchObjects = new GameObject[6];
    Vector2[] startPos = new Vector2[6];
    Vector2[] endPos = new Vector2[6];
    float duration = 10.0f;

    public GameObject thisPanel;
    public GameObject characterClassesPanel;
    public GameObject prevPanel;
    int counter = 0;

    public GameObject readyPanel;
    bool isReadyPlaying;

    void Awake()
    {
        startPos[0] = new Vector2(-1400, TeamDeathmatchObjects[0].transform.position.y);
        startPos[1] = new Vector2(1400, TeamDeathmatchObjects[1].transform.position.y); //hardcode for optimization
        startPos[2] = new Vector2(-1400, TeamDeathmatchObjects[2].transform.position.y);
        startPos[3] = new Vector2(-1400, TeamDeathmatchObjects[3].transform.position.y);
        startPos[4] = new Vector2(1400, TeamDeathmatchObjects[4].transform.position.y);
        startPos[5] = new Vector2(1400, TeamDeathmatchObjects[5].transform.position.y);

        endPos[0] = TeamDeathmatchObjects[0].transform.position;
        endPos[1] = TeamDeathmatchObjects[1].transform.position;
        endPos[2] = TeamDeathmatchObjects[2].transform.position;
        endPos[3] = TeamDeathmatchObjects[3].transform.position;
        endPos[4] = TeamDeathmatchObjects[4].transform.position;
        endPos[5] = TeamDeathmatchObjects[5].transform.position;
    }


    void OnEnable()
    {
        TeamDeathmatchObjects[0].SetActive(true);
        StartCoroutine(fancyObjectEasing(duration));
        characterClassesPanel.SetActive(true);

        isReadyPlaying = false;

        //loadDefaultPlayerStates();
    }

    void OnDisable()
    {
        TeamDeathmatchObjects[0].SetActive(false);
        characterClassesPanel.SetActive(false);
        //readyPanel.SetActive(false);
    }

    IEnumerator fancyObjectEasing(float duration)
    {
        for (float time = 0; time <= duration; time++)
        {
            float lerpAmount = time / duration;
            lerpAmount = Mathf.Sin(lerpAmount * Mathf.PI * 0.5f); //Easing in

            for (int i = 0; i < 6; i++)
            {
                TeamDeathmatchObjects[i].transform.position = Vector2.Lerp(startPos[i], endPos[i], lerpAmount);
            }
            yield return null;
        }
    }

}
