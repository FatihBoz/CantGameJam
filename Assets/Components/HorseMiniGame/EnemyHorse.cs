using UnityEngine;

public class EnemyHorse : Horse
{

    public void RandomizeStats()
    {
        Model.Speed = Random.Range(5f, 20f);
        Model.Weight = Random.Range(5f, 15f);
        Model.AttackScore = Random.Range(0f, 100f);
        Model.DefenseScore = Random.Range(0f, 100f);
        Model.NutrientStorage.RandomizeNutritionStats();

        HorseShoeType[] shoeTypes = (HorseShoeType[])System.Enum.GetValues(typeof(HorseShoeType));
        Model.HorseShoeType = shoeTypes[Random.Range(0, shoeTypes.Length)];
    }

    public void PrintStats()
    {
        string stats = $"Enemy Horse Stats:\n" +
                       $"- Speed: {Model.Speed:F2}\n" +
                       $"- Weight: {Model.Weight:F2}\n" +
                       $"- Attack: {Model.AttackScore:F2}\n" +
                       $"- Defense: {Model.DefenseScore:F2}\n" +
                       $"- Horseshoe: {Model.HorseShoeType}\n" +
                       $"- Carbohydrate: {Model.NutrientStorage.Carbonhydrate.Amount:F2} / Ratio: {Model.NutrientStorage.GetCarbRatio():P0}\n" +
                       $"- Fat: {Model.NutrientStorage.Fat.Amount:F2} / Ratio: {Model.NutrientStorage.GetFatRatio():P0}\n" +
                       $"- Protein: {Model.NutrientStorage.Protein.Amount:F2} / Ratio: {Model.NutrientStorage.GetProteinRatio():P0}";

        Debug.Log(stats);
    }
}
