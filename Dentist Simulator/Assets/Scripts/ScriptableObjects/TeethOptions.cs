using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "TeethOptions", order = 1)]
public class TeethOptions : ScriptableObject
{
    public Material HealthySurface;
    public Material DecaySurface;
    public Material HoledSurface;
    public GameObject HoledTeeth;
}
