using UnityEngine;
using UnityEngine.UI;

public class BoxMovement : MonoBehaviour
{
    public float jumpForce = 50f;

    private Rigidbody2D rb;

    public int score = 0;

    public Text scoreText;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText.text = score.ToString();
    }

    void Update()
    {
        float force = jumpForce * MicrophoneInput.Loudness;
        rb.AddForce(transform.up * force);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Application.LoadLevel(0);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        score++;
        scoreText.text = score.ToString();
    }
}
