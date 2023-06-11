using UnityEngine;

public class Tooth : MonoBehaviour
{
    [field: SerializeField]
    public int Number { get; private set; }

    [field: SerializeField]
    public Transform Point { get; private set; }
}
