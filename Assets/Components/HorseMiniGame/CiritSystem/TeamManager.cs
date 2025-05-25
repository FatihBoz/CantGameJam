using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public static TeamManager Instance { get; private set; }

   // public GameObject PlayerHorses;
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

    public void AssignPlayerTeamHorses(Transform PlayerHorses)
    {
        if (PlayerHorses == null)
        {
            Debug.LogError("PlayerHorses GameObject atanmad�!");
            return;
        }

        PlayerHorse[] horses = PlayerHorses.GetComponentsInChildren<PlayerHorse>();
        TeamType playerTeam = ReturnPlayerTeam();

        Debug.Log($"Player tak�m�na at atan�yor, toplam {horses.Length} at bulundu.");

        foreach (PlayerHorse horse in horses)
        {
            playerTeam.AddHorse(horse.gameObject);
        }

        Debug.Log($"Player tak�m�na {horses.Length} at atand�.");
    }

    public void AssignEnemyTeamHorses()
    {
        TeamType enemyTeam = GetTeamByColor(TeamColor.Blue);
        if (enemyTeam == null)
        {
            Debug.LogError("Enemy team bulunamad�!");
            return;
        }

        for (int i = 0; i < 5; i++)
        {
            GameObject newHorse = Instantiate(HorsePrefabEnemy);
            EnemyHorse enemyHorse = newHorse.GetComponent<EnemyHorse>();
            if (enemyHorse != null)
            {
                enemyTeam.AddHorse(enemyHorse.gameObject);
                enemyHorse.PrintStats();

            }
        }

        Debug.Log("Enemy tak�m�na 5 at atand�.");
    }

    public List<GameObject> ReturnPlayerTeamList()
    {
        TeamType playerTeam = ReturnPlayerTeam();
        if (playerTeam == null)
        {
            Debug.LogError("Player team bulunamad�!");
            return null;
        }
        List<GameObject> horses = playerTeam.GetHorseList();
        
        return horses;
    }
}
