using UnityEngine;

public class EnemyHorse : Horse
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
