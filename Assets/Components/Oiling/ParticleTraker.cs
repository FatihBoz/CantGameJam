using System.Collections.Generic;
using UnityEngine;

public class ParticleTraker : MonoBehaviour
{
    public OilingManager oilingManager;
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;

    public GameObject effectPrefab;
    public float spawnDelay = 0.1f; // Her efekt arasında bekleme

    private float timer = 0f;
    private float lastSpawnY = Mathf.Infinity; // Aynı yerde tekrar spawnlamamak için

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
    }

    void Update()
    {
        timer -= Time.deltaTime;

        int count = ps.GetParticles(particles);
        if (count == 0) return;

        Vector3 lowestWorldPos = Vector3.zero;
        float minY = float.MaxValue;

        for (int i = 0; i < count; i++)
        {
            Vector3 worldPos = particles[i].position;
            if (worldPos.y < minY)
            {
                minY = worldPos.y;
                lowestWorldPos = worldPos;
            }
        }

        // Efekti sadece gerçekten en altta oluşacak şekilde spawnla
        if (timer <= 0f && Mathf.Abs(minY - lastSpawnY) > 0.05f)
        {
            //Instantiate(effectPrefab, lowestWorldPos, Quaternion.identity);
            oilingManager.DropOilAt(lowestWorldPos);
            lastSpawnY = minY;
            timer = spawnDelay;
        }

        // İsteğe bağlı debug çizgisi
        Debug.DrawLine(lowestWorldPos, lowestWorldPos + Vector3.up * 0.2f, Color.red);
    }
    public void SwitchOilManager(OilingManager newOilingManager)
    {
        oilingManager = newOilingManager;
    }
}
