using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

    bool ifExitedMatch = false;
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
}
