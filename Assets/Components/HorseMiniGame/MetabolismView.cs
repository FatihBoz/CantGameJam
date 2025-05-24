using UnityEngine;
using UnityEngine.UI;

public class MetabolismView : MonoBehaviour
{
    public Image carbSlider;
    public Image fatSlider;
    public Image proteinSlider;
    public Image energySlider;

    public void UpdateView(float carbs, float fats, float proteins, float energy)
    {
        carbSlider.fillAmount = carbs;
        fatSlider.fillAmount = fats;
        proteinSlider.fillAmount = proteins;
        energySlider.fillAmount = energy;
    }
}
