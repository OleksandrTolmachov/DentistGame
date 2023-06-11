using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class OralCavity : MonoBehaviour
{
    [SerializeField] private List<BadTooth> _teeth;
    [SerializeField] private ToothSound _toothSound;
    [SerializeField] private DiseaseGenerator _diseaseGenerator;

    public event UnityAction OnWon;

    public void Prepare()
    {
        _teeth = _diseaseGenerator.Generate().ToList();
        foreach (BadTooth tooth in _teeth)
        {
            tooth.OnToothHealed += DestroyDesease;
            tooth.OnToothHealed += _toothSound.PlayHealedTooth;
        }
    }

    private void DestroyDesease(BadTooth tooth)
    {
        _teeth.Remove(tooth);
        Destroy(tooth);
        if (_teeth.Count == 0) OnWon?.Invoke(); 
    }
}
