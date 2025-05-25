
using System;
using UnityEngine;

public class HorseModel
{
    public float Speed { get; set; } = 10f;
    public float Weight { get; set; } = 10f;
    public float Stamina { get; set; } = 100f;
    public float AttackScore { get; set; }
    public float DefenseScore { get; set; }
    public HorseShoeType HorseShoeType { get; set; } = HorseShoeType.Plain;

    //public MetabolismSystem MetabolismSystem { get; private set; }

    public event Action OnHorseDataChanged;


    private NutrientStorage nutrientStorage;
    public NutrientStorage NutrientStorage => nutrientStorage;

    public HorseModel()
    {
        nutrientStorage = new NutrientStorage();
        nutrientStorage.OnValuesChanged += () => OnHorseDataChanged?.Invoke();

        //MetabolismSystem = new MetabolismSystem();
    }

    public void CalculateWeight()
    {
        Weight += nutrientStorage.Protein.Amount * 0.1f +
                  nutrientStorage.Fat.Amount * 0.05f +
                  nutrientStorage.Carbonhydrate.Amount * 0.02f;
    }

    public void AssignHorseShoe(HorseShoeType shoeType)
    {
        HorseShoeType = shoeType;
    }
}
