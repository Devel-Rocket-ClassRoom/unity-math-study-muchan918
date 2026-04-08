using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Splines;

public class SplineFollower : MonoBehaviour
{
    public Transform mover;
    public float duration = 5f;
    private SplineContainer splineContainer;
    private float t;

    private void Awake()
    {
        splineContainer = GetComponent<SplineContainer>();
    }

    // Update is called once per frame
    private void Update()
    {
        t += Time.deltaTime / duration;
        t = Mathf.Repeat(t, 1f);

        // splineContainer.Spline은 첫번째 스플라인
        if (!splineContainer.Evaluate(splineContainer.Spline, t,
            out float3 position, out float3 tangent, out float3 up))
        {
            return;
        }

        mover.position = position;
        if (math.length(tangent) > 0.001f)
        {
            mover.rotation = Quaternion.LookRotation(tangent, up);
        }
    }
}
