using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private const float maxDistance = 5000f;
    private Camera mainCamera;

    public GameObject SelectedInstrument;
    public Vector3 OffSet;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance, 1 << 3))
            {
                SelectedInstrument.transform.position = hit.collider.gameObject.transform.position + OffSet;
            }
        }
    }
}
