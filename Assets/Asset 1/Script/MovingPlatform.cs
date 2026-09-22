using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 3f;

    private Vector3 startPos;
    private Rigidbody rb;

    void Start()
    {
        startPos = transform.position;
        // Ambil komponen Rigidbody
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        // Hitung posisi baru
        Vector3 newPos = startPos + new Vector3(Mathf.Sin(Time.time * speed) * distance, 0, 0);

        // Gerakkan pakai Rigidbody, bukan Transform
        rb.MovePosition(newPos);
    }
}