using System.Collections.Generic;
using UnityEngine;

public class TeamType
{
    private const int MaxHorseCount = 5;

    private List<Horse> horseTeamList = new List<Horse>();

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

    public void AddHorse(Horse horse)
    {
        if (horseTeamList.Count >= MaxHorseCount)
        {
            Debug.LogWarning("Takýmda maksimum sayýda at var!");
            return;
        }

        horseTeamList.Add(horse);
    }

    public void RemoveHorse(Horse horse)
    {
        if (horseTeamList.Contains(horse))
        {
            horseTeamList.Remove(horse);
        }
    }

    public List<Horse> GetHorseList()
    {
        return horseTeamList;
    }
}

public enum TeamColor
{
    //Red is Player's team, Blue is Enemy's team
    Red,
    Blue
}

