using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private List<UiMiniGame> uiMiniGamePrefabs;
    [SerializeField] private List<string> sceneMiniGameNames;
    [SerializeField] private Canvas canvas;


    public void OnButtonPressed(UiMiniGameType type)
    {
        if (!NotificationSystem.Instance.LoadNextGame(false, type, "HEXAPAWA"))
        {
            return;
        }
        UiMiniGame t = uiMiniGamePrefabs.FirstOrDefault(prefab => prefab.type == type);
        Instantiate(t.prefab, canvas.transform);
    }


    public void OnButtonPressed(string sceneName)
    {
        if (!NotificationSystem.Instance.LoadNextGame(true, UiMiniGameType.None, sceneName))
        {
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}


[System.Serializable]
public class UiMiniGame
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public UiMiniGameType type;
}


[System.Serializable]
public class SceneMiniGame
{
    [SerializeField] public string sceneName;
    [SerializeField] public SceneMiniGame type;
}




public enum UiMiniGameType
{
    ArrowCollecting,
    ArrowMaking,
    ShotCleaning,
    CiritCollecting,
    None
}

public enum SceneMiniGameType
{
    Horse,
    OilMaking,
    Oiling,
    None
}