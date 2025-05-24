using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class MetabolismSystem 
{
    public NutrientStorage storageModel;

    MetabolismView view;

    float energy;

    public float Energy { get; private set; }

    public MetabolismSystem(MetabolismView view)
    {
        this.view = view;
        Start();
    }

    private void Start()
    {
        storageModel = new NutrientStorage();
        storageModel.OnValuesChanged += UpdateViewFromModel;
        UpdateViewFromModel(); // ilk açýlýþta senkronize et
    }

    private void UpdateViewFromModel()
    {
        view.UpdateView(
            storageModel.Carbohydrate.Amount,
            storageModel.Fat.Amount,
            storageModel.Protein.Amount,
            Energy
        );
    }

    public void Digest(RacePhase racePhase)
    {
        Nutrient[] nutrientPriority;

        switch (racePhase)
        {
            case RacePhase.Start:
                nutrientPriority = new[] { storageModel.Carbohydrate, storageModel.Fat, storageModel.Protein };
                break;

            case RacePhase.Race:
                nutrientPriority = new[] { storageModel.Fat, storageModel.Carbohydrate, storageModel.Protein };
                break;

            case RacePhase.Finish:
                nutrientPriority = new[] { storageModel.Carbohydrate, storageModel.Fat, storageModel.Protein };
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(racePhase), $"Unexpected RacePhase: {racePhase}");
        }

        // Try consuming the first available nutrient in the priority list
        foreach (var nutrient in nutrientPriority)
        {
            if (nutrient.Amount > 0)
            {
                Consume(nutrient);
                break;
            }
        }

        UpdateViewFromModel();
    }

    private void Consume(Nutrient nutrient)
    {
        float consumedAmount = Mathf.Min(nutrient.Amount, nutrient.DigestionRate);
        nutrient.Amount -= consumedAmount;
        Energy += consumedAmount * nutrient.EnergyPerUnit;
        UpdateViewFromModel();
    }
}
