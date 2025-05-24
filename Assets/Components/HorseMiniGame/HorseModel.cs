
using UnityEngine;

public class HorseModel
{
    public float Speed { get; set; } = 10f;
    public float Weight { get; set; } = 10f;
    public float AttackScore { get; set; }
    public float DefenseScore { get; set; }
    public HorseShoeType HorseShoeType { get; set; } = HorseShoeType.Plain;

    public MetabolismSystem MetabolismSystem { get; private set; }

    public HorseModel(MetabolismView view)
    {
        MetabolismSystem = new MetabolismSystem(view);
    }

    public void CalculateWeight()
    {
        var storage = MetabolismSystem.storageModel;
        Weight += storage.Protein.Amount * 0.1f +
                  storage.Fat.Amount * 0.05f +
                  storage.Carbonhydrate.Amount * 0.02f;
    }

    public void RandomizeStats()
    {
        Speed = Random.Range(5f, 20f);
        Weight = Random.Range(5f, 15f);
        AttackScore = Random.Range(0f, 100f);
        DefenseScore = Random.Range(0f, 100f);
        MetabolismSystem.storageModel.RandomizeNutritionStats();

        HorseShoeType[] shoeTypes = (HorseShoeType[])System.Enum.GetValues(typeof(HorseShoeType));
        HorseShoeType = shoeTypes[Random.Range(0, shoeTypes.Length)];
    }
}
