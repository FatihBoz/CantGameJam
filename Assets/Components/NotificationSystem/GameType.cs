using System;
using UnityEngine;

[Serializable]
public class GameType
{
    public string gameName;
    public bool isGameFinished;
    [Header("false is UI, true is Scene")]
    public bool uiOrScene; // false is ui, true is scene
    public UiMiniGameType uiMiniGameType=UiMiniGameType.None;
    public string scene;
}
