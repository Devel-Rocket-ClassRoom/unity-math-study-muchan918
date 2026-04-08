using UnityEngine;

public class RandomMover : MonoBehaviour
{
    private Vector3 p0, p1, p2, p3;
    private float duration;
    private float elapsed;

    public void Init(Vector3 start, Vector3 end)
    {
        p0 = start;
        p3 = end;

        // 중간 제어점 랜덤 생성 (시작~끝 사이 공간에서)
        p1 = RandomControlPoint(start, (start + end) * 0.3f, end);
        p2 = RandomControlPoint(start, (start + end) * 0.7f, end);

        // 랜덤 이동 시간 (1~4초)
        duration = Random.Range(0.5f, 2f);
        elapsed = 0f;

        transform.position = start;
    }

    private Vector3 RandomControlPoint(Vector3 start, Vector3 mid, Vector3 end)
    {
        // 시작~끝 사이 중간 위치 기준으로 랜덤 오프셋
        float spread = Vector3.Distance(start, end) * 0.5f;

        return mid + new Vector3(
            Random.Range(-spread, spread),
            Random.Range(-spread, spread),
            Random.Range(-spread, spread)
        );
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / duration);

        transform.position = CubicBezier(p0, p1, p2, p3, t);

        // 끝점 도달 시 파괴
        if (t >= 1f)
        {
            Destroy(gameObject);
        }
    }

    private Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);
        return Vector3.Lerp(d, e, t);
    }
}
