using UnityEngine;

public class InstrumentHandler : MonoBehaviour
{
    public float MaxY;
    public float MinY;

    public ParticleSystem[] ParticleSystems;
    public Transform ParticlePosition;

    private void OnMouseDrag()
    {
        float distanceToScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        var vector = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, distanceToScreen));

        vector.y = Mathf.Clamp(vector.y, MinY, MaxY);

        transform.position = new Vector3(vector.x, vector.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Teeth"))
        {
            foreach (var particle in ParticleSystems)
                particle.Play();
        }
    }
}
