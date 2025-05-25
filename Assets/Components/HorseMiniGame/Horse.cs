using UnityEngine;

public class Horse: MonoBehaviour
{
    HorseModel model;
    public HorseModel Model => model;

    ////NutrientStorage nutrientStorage;
    ////public NutrientStorage NutrientStorage => nutrientStorage;

    private void Awake()
    {
        model = new HorseModel();
    }

    public void CalculateScores()
    {
        model.AttackScore = CalculateAttackScore(model.Stamina);
        model.DefenseScore = CalculateDefenseScore(model.Speed, model.Weight);
    }

    public float CalculateAttackScore(float Stamina)
    {
        float attackScore = 0f;
        float protein = model.NutrientStorage.Protein.Amount;

        return attackScore + (protein * 0.5f) + (Stamina * 0.3f);
    }

    public float CalculateDefenseScore(float speed, float weight)
    {
        float defenseScore = 0f;

        return defenseScore + (speed * 0.5f) + (weight * 0.3f);
    }

    //   CiritBehavior ciritBehavior;
}
