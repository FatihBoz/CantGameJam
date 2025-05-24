using System.Collections.Generic;
using UnityEngine;

public class TeamType
{
    float maxHorseCount = 5f;

    Queue<Horse> horseTeamQueue = new Queue<Horse>();
    
    TeamColor teamColor;
    public TeamColor TeamColor => teamColor;

    float scorePoint= 0f;
    public float ScorePoint => scorePoint;

    public void AddScore(float score)
    {
        scorePoint += score;
    }

    public TeamType(TeamColor teamColor)
    {
        this.teamColor = teamColor;

        if(teamColor == TeamColor.Blue)
        {
            AddHorseToQueue(new Horse()); // Example of adding a horse to the enemy team
        }
    }

    public void AddHorseToQueue(Horse horse)
    {
        horseTeamQueue.Enqueue(horse);
    }

    public void RemoveHorseFromQueue(Horse horse)
    {
        if (horseTeamQueue.Count > 0 && horseTeamQueue.Peek() == horse)
        {
            horseTeamQueue.Dequeue();
        }
    }



}
public enum TeamColor
{
    //Red is Player's team, Blue is Enemy's team
    Red,
    Blue
}

