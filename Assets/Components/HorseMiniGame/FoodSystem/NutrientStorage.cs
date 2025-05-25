using System;
using UnityEditor.SceneManagement;
using UnityEngine;

public class NutrientStorage
{
    public event Action OnValuesChanged;

    public Nutrient Carbonhydrate { get; private set; }
    public Nutrient Fat { get; private set; }
    public Nutrient Protein { get; private set; }

    private float maxCarbohydrate;
    private float maxFat;
    private float maxProtein;

    public NutrientStorage(float maxCarbs = 100f, float maxFats = 100f, float maxProteins = 100f)
    {
        Carbonhydrate = new Nutrient(NutrientType.Carbohydrate);
        Fat = new Nutrient(NutrientType.Fat);
        Protein = new Nutrient(NutrientType.Protein);

        maxCarbohydrate = Mathf.Max(0.01f, maxCarbs);
        maxFat = Mathf.Max(0.01f, maxFats);
        maxProtein = Mathf.Max(0.01f, maxProteins);

        OnValuesChanged?.Invoke();
    }

    public void Add(NutritionSO type)
    {
        Carbonhydrate.Amount = Mathf.Min(Carbonhydrate.Amount + type.carbohydrate.Amount, maxCarbohydrate);
        Fat.Amount = Mathf.Min(Fat.Amount + type.fat.Amount, maxFat);
        Protein.Amount = Mathf.Min(Protein.Amount + type.protein.Amount, maxProtein);

        OnValuesChanged?.Invoke();
    }

    public void RandomizeNutritionStats()
    {
        Carbonhydrate.Amount = UnityEngine.Random.Range(0f, maxCarbohydrate);
        Fat.Amount = UnityEngine.Random.Range(0f, maxFat);
        Protein.Amount = UnityEngine.Random.Range(0f, maxProtein);

        OnValuesChanged?.Invoke();
    }

    public float GetCarbRatio() => Mathf.Clamp01(Carbonhydrate.Amount / maxCarbohydrate);
    public float GetFatRatio() => Mathf.Clamp01(Fat.Amount / maxFat);
    public float GetProteinRatio() => Mathf.Clamp01(Protein.Amount / maxProtein);
}
