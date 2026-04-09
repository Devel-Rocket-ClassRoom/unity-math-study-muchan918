using UnityEngine;

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

    private void LateUpdate()
    {
        for (int i = 0; i < cubesCount; i++)
        {
            worldCoord = cubes[i].transform.position;
            screenCoord = cam.WorldToScreenPoint(worldCoord);
            viewportCoord = cam.ScreenToViewportPoint(screenCoord);
            ndcCoord = new Vector2(viewportCoord.x * 2f - 1f, viewportCoord.y * 2f - 1f);

            if (screenCoord.z < 0)
            {
                // 화면 중심 기준으로 명시적으로 반전
                float flippedX = Screen.width - screenCoord.x;
                float flippedY = Screen.height - screenCoord.y;

                cubes[i].ShowImage(new Vector2(
                    Mathf.Clamp(flippedX, 10f, Screen.width - 10f),
                    Mathf.Clamp(flippedY, 10f, Screen.height - 10f)
                ));
            }
            else if (ndcCoord.x > 1 || ndcCoord.x < -1 ||
                ndcCoord.y > 1 || ndcCoord.y < -1)
            {
                cubes[i].ShowImage(new Vector2(
                    Mathf.Clamp(screenCoord.x, 10f, Screen.width - 10f),
                    Mathf.Clamp(screenCoord.y, 10f, Screen.height - 10f)
                ));
            }
            else
            {
                cubes[i].HideImage();
            }
            // Debug.Log($"{screenCoord.x}, {screenCoord.y}, {cam.pixelWidth}, {cam.pixelHeight}");
        }
    }
}
