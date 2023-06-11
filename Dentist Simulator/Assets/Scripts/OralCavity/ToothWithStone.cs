using UnityEngine;
using UnityEngine.Events;

public class ToothWithStone : BadTooth
{
    private GameObject _stone;

    public GameObject StonePrefab;
    public ParticleSystem[] Particles;
    public override event UnityAction<BadTooth> OnToothHealed;


    public override void MakeUnhealthy()
    {
        _stone = Instantiate(StonePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.3f),
            Quaternion.identity);
        var stone = _stone.GetComponent<Stone>();
        stone.OnTap += MakeHealthy;
    }

    public override void MakeHealthy()
    {
        foreach (var particle in Particles)
        {
            var newParticle = Instantiate(particle, transform.position, Quaternion.identity, gameObject.transform);
            newParticle.Play();
        }
        Destroy(_stone);
        OnToothHealed?.Invoke(this);
    }
}








