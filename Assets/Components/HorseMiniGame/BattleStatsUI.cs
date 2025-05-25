using UnityEngine;
using UnityEngine.UI;

public class BattleStatsUI : MonoBehaviour
{
    [SerializeField] private Text playerStatsText;
    [SerializeField] private Text enemyStatsText;

    void Start()
    {
        if (TeamManager.Instance == null)
        {
            Debug.LogError("TeamManager sahnede yok!");
            return;
        }

        TeamType playerTeam = TeamManager.Instance.GetTeamByColor(TeamColor.Red);
        TeamType enemyTeam = TeamManager.Instance.GetTeamByColor(TeamColor.Blue);

        float playerAttack = playerTeam.GetTotalAttackScore();
        float playerDefense = playerTeam.GetTotalDefenseScore();

        float enemyAttack = enemyTeam.GetTotalAttackScore();
        float enemyDefense = enemyTeam.GetTotalDefenseScore();

        playerStatsText.text = $"PLAYER TEAM\nAttack: {playerAttack:F1}\nDefense: {playerDefense:F1}";
        enemyStatsText.text = $"ENEMY TEAM\nAttack: {enemyAttack:F1}\nDefense: {enemyDefense:F1}";
    }
}
