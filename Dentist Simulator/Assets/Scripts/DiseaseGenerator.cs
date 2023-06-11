using System.Collections.Generic;
using UnityEngine;

public class DiseaseGenerator : MonoBehaviour
{
    private GameObject[] _teethPool;

    public TeethOptions TeethOptions;
    public ParticleSystem[] ParticleSystems;
    public ParticleSystem[] ParticleSystemsV2;
    public int SpawnPercantage;

    public IEnumerable<BadTooth> Generate()
    {
        _teethPool = GameObject.FindGameObjectsWithTag("Teeth");
        List<BadTooth> teeth = new List<BadTooth>();
        foreach (var tooth in _teethPool)
        {
            if (Random.Range(0, 10) > SpawnPercantage) continue;
            int random = Random.Range(0, 3);
            switch (random)
            {
                case 0:
                    teeth.Add(GenerateDecayedTooth(tooth, "Drill", TeethOptions.DecaySurface));
                    break;
                case 1:
                    teeth.Add(GenerateDecayedTooth(tooth, "Wadding", TeethOptions.HoledSurface));
                    break;
                case 2:
                    teeth.Add(GenerateToothWithStone(tooth));
                    break;
            }
        }

        return teeth;
    }

    private BadTooth GenerateDecayedTooth(GameObject tooth, string instrument, Material unhealthy)
    {
        tooth.AddComponent<SpoiledTooth>();
        var toothComponent = tooth.GetComponent<SpoiledTooth>();
        toothComponent.HealthySurface = TeethOptions.HealthySurface;
        toothComponent.UnhealthySurface = unhealthy;
        toothComponent.HealInstrument = instrument;
        toothComponent.ParticleSystems = ParticleSystems;
        toothComponent.MakeUnhealthy();
        return toothComponent;
    }

    private BadTooth GenerateToothWithStone(GameObject tooth)
    {
        tooth.AddComponent<ToothWithStone>();
        var toothComponent = tooth.GetComponent<ToothWithStone>();
        toothComponent.StonePrefab = TeethOptions.HoledTeeth;
        toothComponent.Particles = ParticleSystemsV2;
        toothComponent.MakeUnhealthy();
        return toothComponent;
    }
}
