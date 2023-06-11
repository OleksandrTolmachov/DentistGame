using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class BadTooth : MonoBehaviour
{ 
    public abstract event UnityAction<BadTooth> OnToothHealed;

    public abstract void MakeUnhealthy();

    public abstract void MakeHealthy();
}
