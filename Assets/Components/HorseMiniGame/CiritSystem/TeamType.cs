using System.Collections.Generic;
using UnityEngine;

public class TeamType
{
    private const int MaxHorseCount = 5;

    private List<PlayerHorse> horseTeamList = new List<PlayerHorse>();

    private TeamColor teamColor;
    public TeamColor TeamColor => teamColor;

    private float scorePoint = 0f;
    public float ScorePoint => scorePoint;

    public TeamType(TeamColor teamColor)
    {
        this.teamColor = teamColor;
    }

    public void AddScore(float score)
    {
        scorePoint += score;
    }

    public void AddHorse(PlayerHorse horse)
    {
        if (horseTeamList.Count >= MaxHorseCount)
        {
            Debug.LogWarning("Takýmda maksimum sayýda at var!");
            return;
        }

        horseTeamList.Add(horse);
    }

    public void RemoveHorse(PlayerHorse horse)
    {
        if (horseTeamList.Contains(horse))
        {
            horseTeamList.Remove(horse);
        }
    }

    public List<PlayerHorse> ReturnHorseQueueAsList()
    {
        Debug.Log($"Team {teamColor} has {horseTeamList.Count} horses.");
        return new List<PlayerHorse>(horseTeamList);
    }
}

public enum TeamColor
{
    //Red is Player's team, Blue is Enemy's team
    Red,
    Blue
}

