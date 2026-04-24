using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hoverSound;

    XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(_ => PlaySound());
        interactable.hoverExited.AddListener(_ => StopSound());
    }

    void PlaySound()
    {
        if (audioSource != null && hoverSound != null)
            audioSource.PlayOneShot(hoverSound);
    }

    void StopSound()
    {
        audioSource.Stop();
    }
}