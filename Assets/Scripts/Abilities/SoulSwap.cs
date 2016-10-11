using UnityEngine;
using System.Collections;

public class SoulSwap : MonoBehaviour {

    public Sprite mage;

    // Use this for initialization
    void Start () {
        SwapClosestMage();
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    void SwapClosestMage()
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 pos = transform.position;
        foreach (GameObject npc in StartController.npcGameObjectNames)
        {
            if (npc.GetComponent<SpriteRenderer>().sprite == mage)
            {
                Vector3 diff = npc.transform.position - pos;
                float currDistance = diff.sqrMagnitude;
                if (currDistance < distance)
                {
                    closest = npc;
                    distance = currDistance;
                }
            }
        }

        transform.parent.position = closest.transform.position;

        closest.transform.position = pos;
    }
}
