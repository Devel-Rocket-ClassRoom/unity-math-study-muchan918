using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public bool isReturning;
    public Vector3 originalPosition;
    public float timeReturn = 2f;
    private Vector3 startPosition;
    private float timer;

    private Terrain terrain;

    private void Start()
    {
        terrain = Terrain.activeTerrain;
    }

    public void DragStart()
    {
        ResetDrag();

        originalPosition = transform.position;
    }

    public void Return()
    {
        isReturning = true;
        startPosition = transform.position;

    }

    private void ResetDrag()
    {
        isReturning = true;
        timer = 0f;

        originalPosition = Vector3.zero;
        startPosition = Vector3.zero;
    }

    private void Update()
    {
        if (isReturning)
        {
            timer += Time.deltaTime / timeReturn;
            Vector3 newPos = Vector3.Lerp(startPosition, originalPosition, timer);
            newPos.y = terrain.SampleHeight(newPos);
            transform.position = newPos;

            if (timer > 1f)
            {
                transform.position = originalPosition;
                timer = 0f;
                isReturning = false;
            }
        }
    }
}
