
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public SavableVector resetCameraMin;
    public SavableVector resetCameraMax;
    public Vector2 minBoundaries;
    public Vector2 maxBoundaries;

    private float smoothing = 0.02f;

    #region Cached references
    private Transform target;
    #endregion


    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        minBoundaries = resetCameraMin.currentValue;
        maxBoundaries = resetCameraMax.currentValue;
    }

    private void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minBoundaries.x, maxBoundaries.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBoundaries.y, maxBoundaries.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }

}
