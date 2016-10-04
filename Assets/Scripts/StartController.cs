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

	// Use this for initialization
	void Start () {
	}

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        /*if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            DontDestroyOnLoad(gameObject);
        }*/
    }
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            // You can combine these two if statements, but separated them for debugging purposes
            if (Input.GetButtonDown("Controller1_Start"))
            {
                Debug.Log("loading scene 1 controller1 pressed start");
                SceneManager.LoadScene(1);
            }

            if (Input.GetButtonDown("Controller2_Start"))
            {
                Debug.Log("loading scene 1 controller2 pressed start");
                SceneManager.LoadScene(1);
            }

        }
	}
}
