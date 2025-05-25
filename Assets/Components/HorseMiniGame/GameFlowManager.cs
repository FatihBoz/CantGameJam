using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameInitializer : MonoBehaviour
{
    //Make this class singleton
    public static GameInitializer Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private Button applyButton;
    [SerializeField] private string battleSceneName = "BattleScene";

    void Start()
    {
        if (applyButton == null)
        {
            Debug.LogError("Apply butonu atanmad�!");
            return;
        }

        applyButton.onClick.AddListener(OnApplyButtonPressed);


    }

    void OnApplyButtonPressed()
    {
        Debug.Log("Apply butonuna bas�ld�. Tak�mlar haz�rlan�yor...");

        //TeamManager.Instance.AssignPlayerTeamHorses();  // UI'deki atlar� al�r
        // AssignEnemyTeamHorses zaten Awake'te �a�r�l�yor ama istenirse burada da �a�r�labilir

        // T�m veriler haz�r, sahneyi de�i�tir
        TeamManager.Instance.Calculate();

        SceneManager.LoadScene(battleSceneName);
    }

    [SerializeField] private Text playerStatsText;
    [SerializeField] private Text enemyStatsText;


}
