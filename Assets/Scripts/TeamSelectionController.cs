using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TeamSelectionController : MonoBehaviour {
    StartController startController;

    public Texture characterSelectionScreen;
    public Texture[] teamBubbles;
    public Texture[] playerBubbles;

    public List<int> team1;
    public List<int> team2;

    //public string[] teamSelection = new string[2];

    bool teamsFull;
    bool warriorTaken;
    bool rangerTaken;
    bool mageTaken;
    bool rogueTaken;

    // Use this for initialization
    void Start () {
        startController = GameObject.Find("StartController").GetComponent<StartController>();
        startController.InitCharacterSelection();

        team1 = startController.team1;
        team2 = startController.team2;

        //teamSelection[0] = "";
        //teamSelection[1] = "Warrior";
        startController.teams[0] = "";
        startController.teams[1] = "";
        teamsFull = true;
        warriorTaken = false;
        rangerTaken = false;
        mageTaken = false;
        rogueTaken = false;

        team1.Add(1);
        team1.Add(2);

        team2.Add(3);
        team2.Add(4);
    }
	
	// Update is called once per frame
	void Update () {
        //CheckTeamCharSelect(team1, 0);
        //CheckTeamCharSelect(team2, 1);
        Team1CharSelect();
        Team2CharSelect();
        CheckReadyStartGame();
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), characterSelectionScreen, ScaleMode.ScaleToFit);

        if (teamsFull)
        {

            GUI.DrawTexture(new Rect(610, 60, 80, 80), playerBubbles[0]);
            GUI.DrawTexture(new Rect(710, 60, 80, 80), playerBubbles[1]);
            GUI.DrawTexture(new Rect(1150, 60, 80, 80), playerBubbles[2]);
            GUI.DrawTexture(new Rect(1250, 60, 80, 80), playerBubbles[3]);
        }

        if (startController.teams[0] != "")
        {
            DrawTeamSelectionBubble(startController.teams[0], teamBubbles[0]);
        }
        if (startController.teams[1] != "")
        {
            DrawTeamSelectionBubble(startController.teams[1], teamBubbles[1]);
        }
    }

    void CheckTeamCharSelect(List<int> team, int teamNumber)
    {
        for (int i = 0; i < team.Count; i++)
        {
            string selection = GetPlayerCharSelect(team[i], teamNumber);
            if (selection != null)
            {
                startController.teams[teamNumber] = selection;
            }
        }
    }

    void Team1CharSelect()
    {
        for (int i = 0; i < team1.Count; i++)
        {
            string selection = GetPlayerCharSelect(team1[i], 0);
            if (selection != null)
            {
                startController.teams[0] = selection;
                startController.players[team1[0] - 1] = selection;
                startController.players[team1[1] - 1] = selection;
            }
        }
    }

    void Team2CharSelect()
    {
        //startController.players[team2[0] - 1] = startController.teams[1];
        //startController.players[team2[1] - 1] = startController.teams[1];

        for (int i = 0; i < team2.Count; i++)
        {
            string selection = GetPlayerCharSelect(team2[i], 1);
            if (selection != null)
            {
                startController.teams[1] = selection;
                Debug.Log(team2[0] + " " + (team2[0] + 1));
                Debug.Log("@" + startController.players[2]);
                startController.players[team2[0] - 1] = selection;
                startController.players[team2[1] - 1] = selection;
            }
        }
    }

    string GetPlayerCharSelect(int playerIndex, int teamNumber)
    {
        // Press A to select Warrior
        if (Input.GetButtonDown("A" + playerIndex) && !warriorTaken)
        {
            if (startController.teams[teamNumber] == "")
            {
                return "Warrior";
            }
            else
            {
                return "";
            }
        }
        // Press B to select Ranger
        else if (Input.GetButtonDown("B" + playerIndex) && !rangerTaken)
        {
            if (startController.teams[teamNumber] == "")
            {
                return "Ranger";
            }
            else
            {
                return "";
            }
        }
        // Press X to select Mage
        else if (Input.GetButtonDown("X" + playerIndex) && !mageTaken)
        {
            if (startController.teams[teamNumber] == "")
            {
                return "Mage";
            }
            else
            {
                return "";
            }
        }
        // Press Y to select Rogue
        else if (Input.GetButtonDown("Y" + playerIndex) && !rogueTaken)
        {
            if (startController.teams[teamNumber] == "")
            {
                return "Rogue";
            }
            else
            {
                return "";
            }
        }
        else return null;
    }

    void DrawTeamSelectionBubble(string character, Texture teamBubble)
    {
        if (character == "Warrior")
        {
            GUI.DrawTexture(new Rect(480, 490, 80, 80), teamBubble);
        }
        else if (character == "Ranger")
        {
            GUI.DrawTexture(new Rect(1020, 490, 80, 80), teamBubble);
        }
        else if (character == "Mage")
        {
            GUI.DrawTexture(new Rect(480, 940, 80, 80), teamBubble);
        }
        else if (character == "Rogue")
        {
            GUI.DrawTexture(new Rect(1020, 940, 80, 80), teamBubble);
        }
    }

    void CheckReadyStartGame()
    {
        if (Input.GetButtonDown("Start1") || Input.GetButtonDown("Start2") || Input.GetButtonDown("Start3") || Input.GetButtonDown("Start4"))
        {
            Debug.Log("starting");
            if (startController.teams[0] != "" && startController.teams[1] != "")
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                Debug.Log("t1" + startController.teams[0]);
                Debug.Log("t2" + startController.teams[1]);
            }
        }
    }


}
