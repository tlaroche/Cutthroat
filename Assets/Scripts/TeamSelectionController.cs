using UnityEngine;
using System.Collections.Generic;

public class TeamSelectionController : MonoBehaviour {

    public Texture characterSelectionScreen;
    public Texture[] teamBubble;

    public List<int> team1 = new List<int>();
    public List<int> team2 = new List<int>();

    public string[] teamSelection = new string[2];

    bool warriorTaken;
    bool rangerTaken;
    bool mageTaken;
    bool rogueTaken;

    // Use this for initialization
    void Start () {
        teamSelection[0] = "";
        teamSelection[1] = "";
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
        CheckTeamCharSelect(team1, 0);
        CheckTeamCharSelect(team2, 1);
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), characterSelectionScreen, ScaleMode.ScaleToFit);

        if (teamSelection[0] != "")
        {

        }
        else if (teamSelection[1] != "")
        {

        }
    }

    void CheckTeamCharSelect(List<int> team, int teamNumber)
    {
        for (int i = 0; i < team.Count; i++)
        {
            string selection = GetPlayerCharSelect(team[i]);
            if (selection != "")
            {
                teamSelection[teamNumber] = selection;
            }
        }
    }

    string GetPlayerCharSelect(int playerIndex)
    {
        // Press A to select Warrior
        if (Input.GetButtonDown("A" + (playerIndex + 1)) && !warriorTaken)
        {
            return "Warrior";
        }
        // Press B to select Ranger
        else if (Input.GetButtonDown("B" + (playerIndex + 1)) && !rangerTaken)
        {
            return "Ranger";
        }
        // Press X to select Mage
        else if (Input.GetButtonDown("X" + (playerIndex + 1)) && !mageTaken)
        {
            return "Mage";
        }
        // Press Y to select Rogue
        else if (Input.GetButtonDown("Y" + (playerIndex + 1)) && !rogueTaken)
        {
            return "Rogue";
        }
        else return "";
    }

    void DrawTeamSelectionBubble(string character)
    {

    }


}
