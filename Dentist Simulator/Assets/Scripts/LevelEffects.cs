using UnityEngine;
using static UnityEngine.ParticleSystem;

public class LevelEffects : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private OralCavity _oralCavity;
    [SerializeField] private ParticleSystem _particleSystem;

    private void OnEnable()
    {
        _oralCavity.OnWon += Win;
    }

    private void OnDisable()
    {
        _oralCavity.OnWon -= Win;
    }

    public void Win()
    {
        _audioSource.Play();
        ParticleSystem newParticle = Instantiate(_particleSystem, transform.position,
            Quaternion.identity, gameObject.transform);
        newParticle.Play();
    }
}
