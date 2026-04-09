using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float rotateSpeed = 100f;
    private Rigidbody playerRigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float Move = Input.GetAxis("Vertical");
        float Rotate = Input.GetAxis("Horizontal");

        float angle = Rotate * rotateSpeed * Time.deltaTime;
        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.AngleAxis(angle, Vector3.up));

        Vector3 delta = Move * transform.forward * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + delta);
    }
}
