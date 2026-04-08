using UnityEngine;

public class RandomMoverController : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private int spawnCount = 10;
    [SerializeField] private GameObject moverPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMovers();
        }
    }

    private void SpawnMovers()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject obj = Instantiate(moverPrefab, startPoint.position, Quaternion.identity);

            // 랜덤 색상
            Renderer rend = obj.GetComponent<Renderer>();
            rend.material.color = new Color(
                Random.value,
                Random.value,
                Random.value
            );

            RandomMover randomMover = obj.GetComponent<RandomMover>();
            randomMover.Init(startPoint.position, endPoint.position);
        }
    }
}
