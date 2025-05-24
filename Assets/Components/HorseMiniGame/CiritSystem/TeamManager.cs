using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{

    public GameObject HorsePrefabEnemy;

    public List<TeamType> teams = new List<TeamType>();

    private void Awake()
    {
        AssignTeams();
    }

    public void AssignTeams()
    {
        teams.Add(new TeamType(TeamColor.Red)); // Player Team
        teams.Add(new TeamType(TeamColor.Blue));
    }

    public TeamType GetTeamByColor(TeamColor teamColor)
    {
        foreach (var team in teams)
        {
            if (team.TeamColor == teamColor)
            {
                return team;
            }
        }
        return null; // Return null if no team found with the specified color
    }

    public TeamType ReturnPlayerTeam()
    {
        return GetTeamByColor(TeamColor.Red);
    }

}
