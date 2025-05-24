using UnityEngine;

public class RacerHorse : MonoBehaviour
{
    /// <summary>
    /// Old Functionality: This class was used to control the horse's race system.

    /*
    private float CalculateSpeedByEnergy()
    {
        float normalizedEnergy = Mathf.Clamp01(metabolismSystem.Energy);
        return normalizedEnergy * maxSpeed;
    }

    public void MoveHorse()
    {
        speed = CalculateSpeedByEnergy();

        if (speed > 0)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            metabolismSystem.ConsumeEnergy(0.2f * Time.deltaTime);
        }
    }
    */
}
