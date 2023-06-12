using UnityEngine;

public class OralCavityChanger : MonoBehaviour
{
    public GameObject OralCavity;
    public Animator Animator;

    public void Change()
    {
        Animator.SetTrigger("Healed");
        Destroy(OralCavity, 1);
    }
}
