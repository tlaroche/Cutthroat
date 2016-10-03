using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartController : MonoBehaviour {

    public GameObject warrior;
    public GameObject mage;
    public GameObject ranger;
    public GameObject rogue;

    public string player1;
    public string player2;
    public string player3;
    public string player4;

    bool firstTime;

	// Use this for initialization
	void Start () {
        firstTime = true;
	}

    void Awake()
    {
        //if (SceneManager.GetActiveScene().buildIndex != 0)
       // {
            DontDestroyOnLoad(gameObject);
        //}

       
    }
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            
            if (Input.GetKeyDown("7") /*|| Input.GetButtonDown("7")*/)
            {
                Debug.Log("loading scene 1");
                SceneManager.LoadScene(1);
            }
            
        }
	}
}
