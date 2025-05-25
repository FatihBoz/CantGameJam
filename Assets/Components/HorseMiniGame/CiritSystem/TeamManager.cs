using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public static TeamManager Instance { get; private set; }

    public GameObject PlayerHorses;
    public GameObject HorsePrefabEnemy;

    public List<TeamType> teams = new List<TeamType>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        AssignTeams();
       // AssignPlayerTeamHorses();
        AssignEnemyTeamHorses();
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
        return null;
    }

    public TeamType ReturnPlayerTeam()
    {
        return GetTeamByColor(TeamColor.Red);
    }

    public void AssignPlayerTeamHorses()
    {
        if (PlayerHorses == null)
        {
            Debug.LogError("PlayerHorses GameObject atanmadý!");
            return;
        }

        PlayerHorse[] horses = PlayerHorses.GetComponentsInChildren<PlayerHorse>();
        TeamType playerTeam = ReturnPlayerTeam();

        foreach (PlayerHorse horse in horses)
        {
            playerTeam.AddHorse(horse);
        }

        Debug.Log($"Player takýmýna {horses.Length} at atandý.");
    }

    public void AssignEnemyTeamHorses()
    {
        TeamType enemyTeam = GetTeamByColor(TeamColor.Blue);
        if (enemyTeam == null)
        {
            Debug.LogError("Enemy team bulunamadý!");
            return;
        }

        for (int i = 0; i < 5; i++)
        {
            GameObject newHorse = Instantiate(HorsePrefabEnemy);
            PlayerHorse enemyHorse = newHorse.GetComponent<PlayerHorse>();
            if (enemyHorse != null)
            {
                enemyTeam.AddHorse(enemyHorse);
            }
        }

        Debug.Log("Enemy takýmýna 5 at atandý.");
    }
}
