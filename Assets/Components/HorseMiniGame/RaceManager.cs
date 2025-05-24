using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Çift örneði engelle
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Sahne geçiþlerinde korunmasýný istiyorsan
    }

    Horse[] horses;

    public float raceTime;

}

public enum RacePhase
{
    Start,
    Race,
    Finish
}
