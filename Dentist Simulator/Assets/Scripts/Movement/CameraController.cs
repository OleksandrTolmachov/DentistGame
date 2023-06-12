using UnityEngine;

public class CameraControllerV2 : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget;

    private Vector3 previousPosition;

    private void OnMouseDown()
    {
        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        Vector3 direction = previousPosition - newPosition;

        float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
        float rotationAroundXAxis = direction.y * 180; // camera moves vertically

        cam.transform.position = target.position;

        cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
        cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);

        cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

        previousPosition = newPosition;
    }
}
