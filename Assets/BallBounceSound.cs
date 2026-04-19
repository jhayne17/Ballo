using UnityEngine;

public class BallBounceSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bounceClip;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Safe") || collision.gameObject.CompareTag("Kill"))
        {
            audioSource.PlayOneShot(bounceClip);
        }
    }
}