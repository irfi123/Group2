using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketSnapSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip snapSound;

    XRSocketInteractor socket;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(_ => PlaySnapSound());
    }

    void PlaySnapSound()
    {
        if (audioSource != null && snapSound != null)
            audioSource.PlayOneShot(snapSound);
    }
}