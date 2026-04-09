using UnityEngine;
using UnityEngine.Rendering;

public class ScreenIndicator : MonoBehaviour
{
    private Camera cam;

    private Vector3 worldCoord;
    private Vector3 ndcCoord;
    private Vector3 viewportCoord;
    private Vector3 screenCoord;

    public CubeController[] cubes;
    public int cubesCount;

    private void Start()
    {
        cam = Camera.main;
        cubesCount = cubes.Length;
    }

    private void Update()
    {
        for (int i = 0; i < cubesCount; i++)
        {
            worldCoord = cubes[i].transform.position;
            screenCoord = cam.WorldToScreenPoint(worldCoord);
            viewportCoord = cam.ScreenToViewportPoint(screenCoord);
            ndcCoord = new Vector2(viewportCoord.x * 2f - 1f, viewportCoord.y * 2f - 1f);

            if (screenCoord.z < 0)
            {
                cubes[i].ShowImage(new Vector2(
                Mathf.Clamp(-screenCoord.x, 10f, cam.pixelWidth - 10),
                Mathf.Clamp(-screenCoord.y, 10f, cam.pixelHeight - 10)));
            }
            else if (ndcCoord.x > 1 || ndcCoord.x < -1 ||
                ndcCoord.y > 1 || ndcCoord.y < -1)
            {
                cubes[i].ShowImage(new Vector2(
                    Mathf.Clamp(screenCoord.x, 10f, cam.pixelWidth - 10),
                    Mathf.Clamp(screenCoord.y, 10f, cam.pixelHeight - 10)));
            }
            else
            {
                cubes[i].HideImage();
            }
            // Debug.Log($"{screenCoord.x}, {screenCoord.y}, {cam.pixelWidth}, {cam.pixelHeight}");
        }
    }
}
