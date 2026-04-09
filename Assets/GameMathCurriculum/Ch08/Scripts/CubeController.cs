using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// 이미지 출력하기 좌표 반환하기
public class CubeController : MonoBehaviour
{
    public Image image;

    public void ShowImage(Vector2 position)
    {
        image.rectTransform.position = position;
        image.gameObject.SetActive(true);
    }

    public void HideImage()
    {
        image.gameObject.SetActive(false);
    }
}
