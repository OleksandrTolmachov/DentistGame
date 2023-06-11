using System;
using UnityEngine;
using UnityEngine.Events;

public class SpoiledTooth : BadTooth
{
    private MeshRenderer _meshRenderer;

    public Material HealthySurface;
    public Material UnhealthySurface;
    public ParticleSystem[] ParticleSystems;
    public string HealInstrument;

    public override event UnityAction<BadTooth> OnToothHealed;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        MakeUnhealthy();
    }

    public override void MakeUnhealthy()
    {
        _meshRenderer.material = UnhealthySurface;
    }

    public override void MakeHealthy()
    {
        _meshRenderer.material = HealthySurface;
        OnToothHealed?.Invoke(this);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(HealInstrument))
        {
            MakeHealthy();
            foreach (var particle in ParticleSystems)
            {
                ParticleSystem newParticle = Instantiate(particle, transform.position, Quaternion.identity, gameObject.transform);
                newParticle.Play();
            }
        }
    }
}
