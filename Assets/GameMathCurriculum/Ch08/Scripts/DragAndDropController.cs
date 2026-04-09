using UnityEngine;

public class DragAndDropController : MonoBehaviour
{
    private bool isClick;
    private float maxRayDistance = 10000f;

    private Camera cam;
    private GameObject selectedObject;
    private Vector3 originPosition;
    public LayerMask groundLayerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (selectedObject != null)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance, groundLayerMask))
            {
                Debug.Log($"{hit.collider.tag}");
                if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Zone"))
                {
                    Vector3 newPos = hit.point;
                    newPos.y += 3f;
                    selectedObject.transform.position = newPos;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("마우스 클릭");
            if (!isClick)
            {
                if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance))
                {
                    TrySelectObject(hit);
                }
            }
            else
            {
                if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance, groundLayerMask))
                {
                    DeselectCurrent(hit);
                }
            }
        }
    }

    private void TrySelectObject(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Target"))
        {
            selectedObject = hit.collider.gameObject;
            originPosition = selectedObject.transform.position;
            isClick = true;
        }
    }

    // 여기서 selectedObject가 있는지 판단하고
    // Zone에 놓은건지 판단
    private void DeselectCurrent(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Zone"))
        {
            Vector3 newPos = hit.point;
            newPos.y += 3f;
            selectedObject.transform.position = newPos;
        }
        else
        {
            TargetController targetController = selectedObject.GetComponent<TargetController>();
            targetController.originPosition = originPosition;
            targetController.isReturn = true;
        }
        selectedObject = null;
        isClick = false;
    }
}
