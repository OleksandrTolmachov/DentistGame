using UnityEngine;
using UnityEngine.Events;

public class Stone : MonoBehaviour
{
    public event UnityAction OnTap;

    private void OnMouseDown()
    {
        OnTap?.Invoke();
    }
}
