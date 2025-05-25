using UnityEngine;

public class CiritBehavior
{
    public float CalculateAttackScore(NutrientStorage storageModel, float Energy)
    {
        float attackScore = 0f;
        float protein = storageModel.Protein.Amount;

        return attackScore + (protein * 0.5f) + (Energy * 0.3f);
    }

    public float CalculateDefenseScore(float speed, float weight)
    {
        float defenseScore = 0f;

        return defenseScore + (speed * 0.5f) + (weight * 0.3f);
    }
}
