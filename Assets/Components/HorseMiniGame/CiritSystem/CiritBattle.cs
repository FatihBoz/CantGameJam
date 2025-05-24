using Unity.VisualScripting;
using UnityEngine;

public class CiritBattle : MonoBehaviour
{


    void HandleCiritAttack(HorseModel attackerHorse, HorseModel dodgerHorse)
    {
        if(attackerHorse.AttackScore > dodgerHorse.DefenseScore)
        {
            //attackerHorse.teamColor.AddScore(attackerHorse.AttackScore - dodgerHorse.DefenseScore);
        }
    }
}
