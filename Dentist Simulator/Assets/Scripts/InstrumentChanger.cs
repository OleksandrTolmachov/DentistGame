using UnityEngine;

public class InstrumentChanger : MonoBehaviour
{
    public GameObject Drill;
    public GameObject Wadding;
    [SerializeField] private InstrumentLogic _instrumentLogic;

    public void TakeDrill()
    {
        if (Drill.activeSelf)
        {
            _instrumentLogic.SelectedInstrument = null;
            Drill.SetActive(false);
        }
        else
        {
            _instrumentLogic.SelectedInstrument = Drill;
            Drill.SetActive(true);
            Wadding.SetActive(false);
        }
    }

    public void TakeWadding()
    {
        if (Wadding.activeSelf)
        {
            _instrumentLogic.SelectedInstrument = null;
            Wadding.SetActive(false);
        }
        else
        {
            _instrumentLogic.SelectedInstrument = Wadding;
            Wadding.SetActive(true);
            Drill.SetActive(false);
        }
    }
}
