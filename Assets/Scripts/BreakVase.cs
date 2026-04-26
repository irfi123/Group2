using UnityEngine;

public class BreakVase : MonoBehaviour
{
    public GameObject fracturedVase;
    public AudioSource audioSource;
    public AudioClip breakSound;
    public BallInVase[] balls;
    private bool broken = false;

    void OnTriggerEnter(Collider other)
    {
        if (broken) return;
        if (other.CompareTag("Destroyer"))
        {
            broken = true;
            if (audioSource != null && breakSound != null)
                audioSource.PlayOneShot(breakSound);
            fracturedVase.SetActive(true);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            foreach (BallInVase ball in balls)
                if (ball != null) ball.Release();
        }
    }
}