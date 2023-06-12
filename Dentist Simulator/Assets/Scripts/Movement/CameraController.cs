using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Transform[] switchPoints; // Array of switch points with specified rotations
    public float switchSpeed = 2f; // Speed at which the camera switches between points
    private int currentPointIndex = 1; // Index of the current switch point
    public int CurrentPointIndex
    {
        get { return currentPointIndex; }
        set
        {
            if (switchPoints.Length - 1 >= value && value >= 0)
            {
                if(value == 0)
                {
                    Left.interactable = false;
                }
                else if(value == switchPoints.Length - 1) 
                {
                    Right.interactable = false;
                }
                else
                {
                    Left.interactable = true;
                    Right.interactable = true;
                }
                currentPointIndex = value;
            }
        }
    }

    private Transform targetPoint; // The target switch point for the camera
    private Quaternion targetRotation; // The target rotation for the camera

    [SerializeField] private Button Left;
    [SerializeField] private Button Right;

    private void Start()
    {
        // Set the initial target point and rotation
        SetTargetPoint(switchPoints[currentPointIndex], currentPointIndex);
    }

    private void FixedUpdate()
    {
        SetTargetPoint(switchPoints[currentPointIndex], currentPointIndex);

        // Smoothly move the camera towards the target point and rotation
        transform.position = Vector3.Lerp(transform.position, targetPoint.position, switchSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, switchSpeed * Time.deltaTime);
    }

    private void SetTargetPoint(Transform point, int index)
    {
        // Set the target point and rotation
        targetPoint = point;
        targetRotation = point.rotation;
    }

    public void IncrementIndex() { CurrentPointIndex++; }
    public void DecrementIndex() { CurrentPointIndex--; }
}
