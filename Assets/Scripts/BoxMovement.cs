using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    public float jumpForce = 50f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float force = jumpForce * MicrophoneInput.Loudness;
        rb.AddForce(transform.up * force);
    }
}
