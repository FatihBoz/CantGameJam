using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class BattleStatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerStatsText;
    [SerializeField] private TextMeshProUGUI enemyStatsText;

    void Start()
    {
        if (TeamManager.Instance == null)
        {
            Debug.LogError("TeamManager sahnede yok!");
            return;
        }

        playerStatsText.text = $"PLAYER TEAM\nAttack: {TeamManager.Instance.playerAttack:F1}\nDefense: {TeamManager.Instance.playerDefense:F1}";
        enemyStatsText.text = $"ENEMY TEAM\nAttack: {TeamManager.Instance.enemyAttack:F1}\nDefense: {TeamManager.Instance.enemyDefense:F1}";

        StartCoroutine(GoToMainMenuAfterDelay(3f));
    }

    private IEnumerator GoToMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu");
    }
}
