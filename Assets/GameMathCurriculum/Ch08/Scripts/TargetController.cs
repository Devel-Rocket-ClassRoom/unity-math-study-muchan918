using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Vector3 originPosition;
    public bool isReturn;
    private float returnSpeed;
    public Terrain terrain;

    private void Start()
    {
        returnSpeed = 5f;
    }

    private void Update()
    {
        if (isReturn)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                originPosition,
                returnSpeed * Time.deltaTime
            );

            Vector3 newPos = new Vector3(
            transform.position.x,
            terrain.SampleHeight(transform.position) + 3f,
            transform.position.z
            );

            transform.position = newPos;
        }

        if (Vector3.Distance(transform.position, originPosition) < 0.1f)
        {
            isReturn = false;
        }
    }
}
