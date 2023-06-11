using UnityEngine;

public class ToothSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceBubble;
    [SerializeField] private AudioSource _audioSourceStone;

    public void PlayHealedTooth(BadTooth tooth)
    {
        if(tooth is ToothWithStone)
        {
            _audioSourceStone.Play();
        }
        else
        {
            _audioSourceBubble.Play();
        }
    }
}
