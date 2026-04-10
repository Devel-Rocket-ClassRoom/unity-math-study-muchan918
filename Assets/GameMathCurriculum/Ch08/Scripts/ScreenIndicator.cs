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

            if (Mathf.Abs(screenCoord.z) < 1f)
            {
                Debug.Log($"{screenCoord.x}, {screenCoord.y} {i}의 z가 0 부근");
                //return;
            }




            // if (screenCoord.z < 0)
            // {
            //     // 화면 중심 기준으로 명시적으로 반전
            //     float flippedX = Screen.width - screenCoord.x;
            //     float flippedY = Screen.height - screenCoord.y;

            //     cubes[i].ShowImage(new Vector2(
            //         Mathf.Clamp(flippedX, 10f, Screen.width - 10f),
            //         Mathf.Clamp(flippedY, 10f, Screen.height - 10f)
            //     ));
            // }
            if (ndcCoord.x > 1 || ndcCoord.x < -1 ||
                ndcCoord.y > 1 || ndcCoord.y < -1)
            {
                // cubes[i].ShowImage(new Vector2(
                //     Mathf.Clamp(screenCoord.x, 10f, Screen.width - 10f),
                //     Mathf.Clamp(screenCoord.y, 10f, Screen.height - 10f)
                // ));

                Vector3 local = cam.transform.InverseTransformPoint(cubes[i].transform.position);
                Vector2 dir = new Vector2(local.x, local.y).normalized;
                Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
                float scale = Mathf.Min(center.x / Mathf.Abs(dir.x), center.y / Mathf.Abs(dir.y));
                Vector2 pos = center + dir * scale;
                cubes[i].ShowImage(pos);
            }
            else
            {
                cubes[i].HideImage();
            }
            // Debug.Log($"{screenCoord.x}, {screenCoord.y}, {cam.pixelWidth}, {cam.pixelHeight}");
        }
    }
}
