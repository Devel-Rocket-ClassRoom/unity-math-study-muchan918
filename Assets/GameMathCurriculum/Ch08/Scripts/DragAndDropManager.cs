using UnityEngine;
using UnityEngine.AI;

public class DragAndDropManager : MonoBehaviour
{
    public Camera camera;
    public LayerMask ground;
    public LayerMask dragObject;
    public LayerMask dragZone;
    private bool isDragging = false;
    private Transform draggingObject;

    private void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, dragObject))
            {
                Debug.Log("Drag Start");
                isDragging = true;
                draggingObject = hitInfo.collider.transform;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                Debug.Log("Drag Start");
                isDragging = false;
                draggingObject = null;

                if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, dragZone))
                {
                    draggingObject.transform.position = hitInfo.collider.transform.position;
                }
            }
        }

        if (isDragging)
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, ground))
            {
                Debug.Log(hitInfo.point);
                draggingObject.transform.position = hitInfo.point;
            }
        }
    }
}
