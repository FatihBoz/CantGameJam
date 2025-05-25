using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class MetabolismSystem 
{
    public NutrientStorage storageModel;

    MetabolismView metabolismView;

    Nutrient currentUsingNutrient;

    float energy;
    public float Energy { get; private set; }

    bool isDigesting = false;

    public MetabolismSystem(MetabolismView metabolismView)
    {
        this.metabolismView = metabolismView;
        Start();
    }

    private void Start()
    {
        storageModel = new NutrientStorage();
        storageModel.OnValuesChanged += UpdateViewFromModel;
        UpdateViewFromModel(); // ilk açýlýþta senkronize et
    }

    public void AddNutrition(NutritionSO type)
    {
        storageModel.Add(type);
    }

    private void UpdateViewFromModel()
    {
        metabolismView.UpdateView(storageModel, Energy);
    }

    public IEnumerator Digest()
    {
        if (isDigesting || currentUsingNutrient == null)
        {
            yield break;
        }

        isDigesting = true;
        Debug.Log("Digesting nutrient: " + currentUsingNutrient.Name + " with amount: " + currentUsingNutrient.Amount);

        float delay = 2f;

        while (currentUsingNutrient != null && currentUsingNutrient.Amount > 0)
        {
            Consume(currentUsingNutrient);
            UpdateViewFromModel();
            yield return new WaitForSeconds(delay);
        }

        currentUsingNutrient = null;
        isDigesting = false;
    }

    //Update'de deðil de eventlerle çalýþtýrmak lazým.
    public void SelectNutrientType(RacePhase racePhase)
    {
        if(currentUsingNutrient != null)
        {
            return; // If a nutrient is already being used, do not select a new one
        }

        Nutrient[] nutrientPriority;

        switch (racePhase)
        {
            case RacePhase.Start:
                nutrientPriority = new[] { storageModel.Carbonhydrate, storageModel.Fat, storageModel.Protein };
                break;

            case RacePhase.Race:
                nutrientPriority = new[] { storageModel.Fat, storageModel.Carbonhydrate, storageModel.Protein };
                break;

            case RacePhase.Finish:
                nutrientPriority = new[] { storageModel.Carbonhydrate, storageModel.Fat, storageModel.Protein };
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(racePhase), $"Unexpected RacePhase: {racePhase}");
        }

        // Try consuming the first available nutrient in the priority list
        foreach (var nutrient in nutrientPriority)
        {
            if(nutrient.Amount > 0)
            {
                currentUsingNutrient = nutrient;
                return;
            }
        }
    }

    private void Consume(Nutrient nutrient)
    {
        nutrient.Amount -= nutrient.DigestionRate;
        Energy += nutrient.DigestionRate * nutrient.EnergyPerUnit;

        if(nutrient.Amount < 0)
        {
            nutrient.Amount = 0; // Ensure nutrient amount does not go negative
            currentUsingNutrient = null; // Reset to allow selection of a new nutrient
        }
        UpdateViewFromModel();
    }

    public bool ConsumeEnergy(float amount)
    {
        if (Energy >= amount)
        {
            Energy -= amount;
            UpdateViewFromModel(); // UI güncelle
            return true; // Enerji baþarýyla kullanýldý
        }
        else
        {
            Energy = 0f;
            UpdateViewFromModel();
            return false; // Yeterli enerji yok
        }
    }
}
