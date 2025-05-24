[System.Serializable]
public class RaceSettings
{
    public TrackType trackType;
    public float raceDuration;
}

public enum TrackType
{
    Dirt,
    Grass
}