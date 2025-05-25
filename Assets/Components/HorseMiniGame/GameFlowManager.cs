using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private Button applyButton;
    [SerializeField] private string battleSceneName = "BattleScene";

    void Start()
    {
        if (applyButton == null)
        {
            Debug.LogError("Apply butonu atanmadý!");
            return;
        }

        applyButton.onClick.AddListener(OnApplyButtonPressed);
    }

    void OnApplyButtonPressed()
    {
        Debug.Log("Apply butonuna basýldý. Takýmlar hazýrlanýyor...");

        TeamManager.Instance.AssignPlayerTeamHorses();  // UI'deki atlarý alýr
        // AssignEnemyTeamHorses zaten Awake'te çaðrýlýyor ama istenirse burada da çaðrýlabilir

        // Tüm veriler hazýr, sahneyi deðiþtir
        SceneManager.LoadScene(battleSceneName);
    }
}
