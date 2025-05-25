using Unity.VisualScripting;
using UnityEngine;

public class CiritBattle : MonoBehaviour
{


    void HandleCiritAttack(Horse attackerHorse, Horse dodgerHorse)
    {
        if(attackerHorse.Model.AttackScore > dodgerHorse.Model.DefenseScore)
        {
            //attackerHorse.teamColor.AddScore(attackerHorse.AttackScore - dodgerHorse.DefenseScore);
        }
    }
}
