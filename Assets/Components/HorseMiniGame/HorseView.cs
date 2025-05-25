using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HorseView : MonoBehaviour
{
    [Header("Stat UI")]
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI weightText;

    [Header("Horse Shoe UI")]
    [SerializeField] private Image horseShoeIcon;
    [SerializeField] private Sprite plainShoeSprite;
    [SerializeField] private Sprite spikedShoeSprite;



    public void UpdateStatsUI(HorseModel model)
    {
        if (attackText != null)
            attackText.text = $"Attack: {model.AttackScore:F1}";

        if (defenseText != null)
            defenseText.text = $"Defense: {model.DefenseScore:F1}";

        if (speedText != null)
            speedText.text = $"Speed: {model.Speed:F1}";

        if (weightText != null)
            weightText.text = $"Weight: {model.Weight:F1}";
    }

    public void ShowHorseShoe(HorseModel shoeType)
    {
        if (horseShoeIcon == null) return;

        switch (shoeType.HorseShoeType)
        {
            case HorseShoeType.Plain:
                horseShoeIcon.sprite = plainShoeSprite;
                break;
            case HorseShoeType.Spiked:
                horseShoeIcon.sprite = spikedShoeSprite;
                break;
            default:
                horseShoeIcon.sprite = plainShoeSprite;
                break;
        }
    }
}
