using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform target;

    private Vector3 offset = new Vector3(0f, 8f, -8f);

    private float rotationSmoothSpeed = 10f;
    private Vector3 SmoothVelocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.TransformPoint(offset);

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref SmoothVelocity,
            0.2f
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(target.position - transform.position),
            Time.deltaTime * rotationSmoothSpeed
        );
    }
}
