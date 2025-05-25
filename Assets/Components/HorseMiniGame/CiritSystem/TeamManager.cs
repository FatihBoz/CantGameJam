using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public static TeamManager Instance { get; private set; }

   // public GameObject PlayerHorses;
    public GameObject HorsePrefabEnemy;

    public List<TeamType> teams = new List<TeamType>();

    public float playerAttack;
    public float playerDefense;

    public float enemyAttack;
    public float enemyDefense;



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
            Debug.LogError("PlayerHorses GameObject atanmadý!");
            return;
        }

        PlayerHorse[] horses = PlayerHorses.GetComponentsInChildren<PlayerHorse>();
        TeamType playerTeam = ReturnPlayerTeam();

        Debug.Log($"Player takýmýna at atanýyor, toplam {horses.Length} at bulundu.");

        foreach (PlayerHorse horse in horses)
        {
            playerTeam.AddHorse(horse.gameObject);
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
            EnemyHorse enemyHorse = newHorse.GetComponent<EnemyHorse>();
            if (enemyHorse != null)
            {
                enemyTeam.AddHorse(enemyHorse.gameObject);
                //enemyHorse.PrintStats();

            }
        }

        Debug.Log("Enemy takýmýna 5 at atandý.");
    }

    public List<GameObject> ReturnPlayerTeamList()
    {
        TeamType playerTeam = ReturnPlayerTeam();
        if (playerTeam == null)
        {
            Debug.LogError("Player team bulunamadý!");
            return null;
        }
        List<GameObject> horses = playerTeam.GetHorseList();
        
        return horses;
    }

    public void Calculate()
    {
        TeamType playerTeam = GetTeamByColor(TeamColor.Red);
        TeamType enemyTeam = GetTeamByColor(TeamColor.Blue);

        playerAttack = playerTeam.GetTotalAttackScore();
        playerDefense = playerTeam.GetTotalDefenseScore();

        enemyAttack = enemyTeam.GetTotalAttackScore();
        enemyDefense = enemyTeam.GetTotalDefenseScore();

        Debug.Log("=== PLAYER TEAM STATS ===");
        PrintHorseStats(playerTeam);

        Debug.Log("=== ENEMY TEAM STATS ===");
        PrintHorseStats(enemyTeam);

        Debug.Log($"Toplam -> Player Attack: {playerAttack}, Player Defense: {playerDefense}");
        Debug.Log($"Toplam -> Enemy Attack: {enemyAttack}, Enemy Defense: {enemyDefense}");
    }

    private void PrintHorseStats(TeamType team)
    {
        List<GameObject> horses = team.GetHorseList();
        for (int i = 0; i < horses.Count; i++)
        {
            GameObject horseObj = horses[i];
            Horse horse = horseObj.GetComponent<Horse>();
            if (horse != null)
            {
                Debug.Log(
                    $"{team.TeamColor} - Horse #{i + 1} -> " +
                    $"Attack: {horse.Model.AttackScore}, Defense: {horse.Model.DefenseScore}");
            }
            else
            {
                Debug.LogWarning($"{team.TeamColor} takýmýndaki Horse #{i + 1} nesnesinde 'Horse' bileþeni yok!");
            }
        }
    }

}
