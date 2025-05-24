using UnityEngine;

[System.Serializable]
public class Nutrient
{
    [SerializeField]
    private float amount;

    public NutrientType Type { get; }
    public string Name { get; }
    public float DigestionRate { get; }
    public float EnergyPerUnit { get; }
    public float Amount
    {
        get => amount;
        set => amount = value;
    }

    public Nutrient(NutrientType type, float amount = 0f)
    {
        Type = type;
        this.amount = amount;

        // Set values based on nutrient type
        switch (type)
        {
            case NutrientType.Carbohydrate:
                Name = "Carbohydrate";
                DigestionRate = 0.5f;
                EnergyPerUnit = 4.0f;
                break;
            case NutrientType.Fat:
                Name = "Fat";
                DigestionRate = 0.3f;
                EnergyPerUnit = 9.0f;
                break;
            case NutrientType.Protein:
                Name = "Protein";
                DigestionRate = 0.4f;
                EnergyPerUnit = 4.0f;
                break;
            default:
                throw new System.ArgumentException("Invalid NutrientType");
        }
    }
}

public enum NutrientType
{
    Carbohydrate,
    Fat,
    Protein
}