using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Vector3 originPosition;
    public bool isReturn;
    private float returnSpeed;
    public Terrain terrain;

    private void Start()
    {
        returnSpeed = 0.1f;
    }

    private void Update()
    {
        if (isReturn)
        {
            Vector3 newPos = Vector3.Lerp(
                transform.position,
                originPosition,
                returnSpeed * Time.deltaTime
            );

            newPos.y = terrain.SampleHeight(newPos) + 3f;

            transform.position = newPos;
        }

        if (Mathf.Abs(transform.position.x - originPosition.x) < 0.3f
            && Mathf.Abs(transform.position.z - originPosition.z) < 0.3f)
        {
            isReturn = false;
            Debug.Log("반납 끝");
            transform.position = originPosition;
        }
    }
}
