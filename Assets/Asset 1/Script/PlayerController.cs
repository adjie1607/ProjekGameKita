using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))] // Memastikan objek punya CharacterController
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 7f;
    public float jumpForce = 8f;
    public float gravity = 20f;

    [Header("Visuals")]
    public SpriteRenderer sr;
    public Animator anim;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        // Hubungkan referensi controller
        controller = GetComponent<CharacterController>();
        if (anim == null) anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Cek apakah di tanah
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Menjaga player tetap menempel ke tanah
        }

        float x = 0, z = 0;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) z = 1;
            else if (Keyboard.current.sKey.isPressed) z = -1;
            if (Keyboard.current.aKey.isPressed) x = -1;
            else if (Keyboard.current.dKey.isPressed) x = 1;

            if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
            {
                // Rumus lompat: v = sqrt(h * 2 * g)
                velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
            }
        }

        // Jalankan Gerakan Horizontal
        Vector3 moveDirection = new Vector3(x, 0, z).normalized;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Jalankan Gravitasi
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        UpdateVisuals(x, moveDirection.magnitude > 0);
    }

    void UpdateVisuals(float horizontalInput, bool isWalking)
    {
        if (anim != null)
        {
            anim.SetBool("Run", isWalking);
            anim.SetBool("Jump", !isGrounded);
        }

        if (sr != null)
        {
            if (horizontalInput < 0) sr.flipX = true;
            else if (horizontalInput > 0) sr.flipX = false;
        }
    }
}