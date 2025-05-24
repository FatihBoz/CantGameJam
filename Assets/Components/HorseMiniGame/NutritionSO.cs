using UnityEngine;

[CreateAssetMenu(fileName = "NewNutrition", menuName = "HorseSystem/Nutrition")]
public class NutritionSO : ScriptableObject
{
    public Nutrient carbohydrate = new Nutrient(NutrientType.Carbohydrate);
    public Nutrient fat = new Nutrient(NutrientType.Fat);
    public Nutrient protein = new Nutrient(NutrientType.Protein);
}