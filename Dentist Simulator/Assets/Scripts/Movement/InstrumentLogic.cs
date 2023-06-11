using System.Linq;
using UnityEngine;

public class InstrumentLogic : MonoBehaviour
{
    private const int BottomToothAmount = 15;
    private const int TopToothAmount = 31;

    private Tooth[] _teeth;
    private int _selectedTooth = 6;

    public GameObject SelectedInstrument;

    private void Awake()
    {
        _teeth = FindObjectsOfType<Tooth>();
    }

    private void FixedUpdate()
    {
        var tooth = _teeth.First(tooth => tooth.Number == _selectedTooth);

        if (SelectedInstrument != null)
        {
            SelectedInstrument.transform.position = Vector3.MoveTowards(SelectedInstrument.transform.position,
                tooth.Point.transform.position, 0.2f);

            SelectedInstrument.transform.LookAt(tooth.transform);
        }
    }

    public void MoveToLeft()
    {
        if (_selectedTooth != 0)
            _selectedTooth--;
    }

    public void MoveToRight()
    {
        if(_selectedTooth != TopToothAmount)
            _selectedTooth++;
    }

    public void MoveToTop()
    {
        _selectedTooth = _selectedTooth + BottomToothAmount;
    }

    public void MoveToBottom()
    {
        _selectedTooth = _selectedTooth - BottomToothAmount;
    }
}
