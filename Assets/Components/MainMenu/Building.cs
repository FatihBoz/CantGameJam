using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] private bool ui;
    [SerializeField] private UiMiniGameType miniGameType;
    [SerializeField] private string sceneNameToChange;
    private Button button;
    private MainMenuManager mainMenuManager;


    private void Awake()
    {
        button = GetComponent<Button>();
        mainMenuManager = FindFirstObjectByType<MainMenuManager>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnButtonPressed);
    }


    void OnButtonPressed()
    {
        if (ui)
        {

            mainMenuManager.OnButtonPressed(miniGameType);
        }
        else
        {

            mainMenuManager.OnButtonPressed(sceneNameToChange);
        }
    }



}
