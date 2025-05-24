using System;
using UnityEditor.SceneManagement;
using UnityEngine;

public class NutrientStorage
{
    public event Action OnValuesChanged;

    public Nutrient Carbohydrate { get; private set; }
    public Nutrient Fat { get; private set; }
    public Nutrient Protein { get; private set; }
    
    public NutrientStorage()
    {
        // Initialize nutrients with zero amounts
        Carbohydrate = new Nutrient(NutrientType.Carbohydrate);
        Fat = new Nutrient(NutrientType.Fat);
        Protein = new Nutrient(NutrientType.Protein);
    }

    public void Add(NutritionSO type)
    {
        // Update only the Amount for each Nutrient
        Carbohydrate.Amount += type.carbohydrate.Amount;
        Fat.Amount += type.fat.Amount;
        Protein.Amount += type.protein.Amount;
        OnValuesChanged?.Invoke();
    }


    //public void UseEnergy(float amount)
    //{
    //    Energy = Mathf.Max(0, Energy - amount);
    //}

    //public float CalculateStartSpeedBoost()
    //{
    //    float buff = Mathf.Clamp(Proteins * 0.1f, 0f, 5f);
    //    return buff;
    //}

    //public void ApplyBuff(ref float speed)
    //{
    //    float buff = CalculateStartSpeedBoost();
    //    speed += buff;
    //}

    //public float GetTotalEnergy() => Energy;
}
