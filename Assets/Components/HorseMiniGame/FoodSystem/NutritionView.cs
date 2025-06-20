using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NutritionView : MonoBehaviour
{
    public Image carbSlider;
    public Image fatSlider;
    public Image proteinSlider;
    public Image energySlider;


    public void UpdateView(NutrientStorage storage, float stamina)
    {
        UpdateSlider(carbSlider, storage.GetCarbRatio(), "Carbs");
        UpdateSlider(fatSlider, storage.GetFatRatio(), "Fats");
        UpdateSlider(proteinSlider, storage.GetProteinRatio(), "Proteins");

        UpdateSlider(energySlider, stamina, "Stamina"); // energy do�rudan gelen oran
    }

    private void UpdateSlider(Image slider, float ratio, string label)
    {
        slider.fillAmount = ratio;

        var text = slider.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            text.text = $"{label}: {Mathf.RoundToInt(ratio * 100)}%";
        }
    }
}
