using UnityEngine;
using System.Collections.Generic;

public class WaitingPlatform : MonoBehaviour
{
    [Header("Settings")]
    public List<Transform> waypoints;
    public float speed = 3f;
    public bool moveOnlyWhenStepped = true; // Bisa diatur di Inspector

    private int currentTargetIndex = 0;
    private bool isPlayerOnTop = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Pastikan platform mulai di titik pertama
        if (waypoints.Count > 0)
        {
            transform.position = waypoints[0].position;
        }
    }

    void FixedUpdate()
    {
        if (waypoints.Count < 2) return;

        // Logic: Gerak kalau player di atas ATAU kalau moveOnlyWhenStepped dimatikan
        if (!moveOnlyWhenStepped || isPlayerOnTop)
        {
            MovePlatform();
        }
    }

    void MovePlatform()
    {
        Vector3 targetPos = waypoints[currentTargetIndex].position;

        // Gerak menggunakan Rigidbody agar player ikut nempel
        Vector3 newPos = Vector3.MoveTowards(rb.position, targetPos, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        // Kalau sampai di titik tujuan, ganti ke titik berikutnya
        if (Vector3.Distance(rb.position, targetPos) < 0.05f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % waypoints.Count;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Cek Tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Jadiin player anak platform biar nempel
            collision.transform.SetParent(transform);
            isPlayerOnTop = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Lepas player pas turun
            collision.transform.SetParent(null);
            isPlayerOnTop = false;
        }
    }
}