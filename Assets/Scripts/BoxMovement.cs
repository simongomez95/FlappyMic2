using UnityEngine;
using UnityEngine.UI;

public class BoxMovement : MonoBehaviour
{
    public float jumpForce = 50f;

    private Rigidbody2D rb;

    public int score = 0;

    public Text scoreText;

	public AudioClip flap;

	AudioSource audio;

	void Start()
	{
		audio = GetComponent<AudioSource>();
	}

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText.text = score.ToString();
    }

    void Update()
    {
		if (MicrophoneInput.Loudness > 0) {
			//audio.PlayOneShot(flap, 0.7F);
		}
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
