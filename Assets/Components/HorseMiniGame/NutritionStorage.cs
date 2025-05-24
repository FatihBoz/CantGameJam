using System;
using UnityEditor.SceneManagement;
using UnityEngine;

public class NutrientStorage
{
    public event Action OnValuesChanged;

    public Nutrient Carbohydrate { get; private set; }
    public Nutrient Fat { get; private set; }
    public Nutrient Protein { get; private set; }

    private float maxCarbohydrate;
    private float maxFat;
    private float maxProtein;

    public NutrientStorage(float maxCarbs = 1f, float maxFats = 1f, float maxProteins = 1f)
    {
        Carbohydrate = new Nutrient(NutrientType.Carbohydrate);
        Fat = new Nutrient(NutrientType.Fat);
        Protein = new Nutrient(NutrientType.Protein);

        maxCarbohydrate = Mathf.Max(0.01f, maxCarbs);
        maxFat = Mathf.Max(0.01f, maxFats);
        maxProtein = Mathf.Max(0.01f, maxProteins);
    }

    public void Add(NutritionSO type)
    {
        Carbohydrate.Amount = Mathf.Min(Carbohydrate.Amount + type.carbohydrate.Amount, maxCarbohydrate);
        Fat.Amount = Mathf.Min(Fat.Amount + type.fat.Amount, maxFat);
        Protein.Amount = Mathf.Min(Protein.Amount + type.protein.Amount, maxProtein);

        OnValuesChanged?.Invoke();
    }

    public float GetCarbRatio() => Mathf.Clamp01(Carbohydrate.Amount / maxCarbohydrate);
    public float GetFatRatio() => Mathf.Clamp01(Fat.Amount / maxFat);
    public float GetProteinRatio() => Mathf.Clamp01(Protein.Amount / maxProtein);
}
