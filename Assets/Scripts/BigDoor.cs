using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BigDoor : MonoBehaviour
{
    [Header("Settings")]
    public int puzzlesRequired = 1;
    public float openDuration = 5f;
    public float openAngle = 110f;
    public RespawnZone respawnZone;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioSource musicSource;
    public AudioClip unlockSound;
    public AudioClip doorOpenSound;
    public AudioClip outdoorAmbienceSound;
    public AudioClip lockedSound;
    public float ambienceFadeInDuration = 3f;
    public float musicFadeOutDuration = 3f;

    private int solvedCount = 0;
    private bool isOpen = false;
    private Quaternion closedRot;
    private Quaternion openRot;

    void Awake()
    {
        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();
        if (interactable != null)
            interactable.selectEntered.AddListener(_ => TryOpenLocked());
    }

    void Start()
    {
        closedRot = transform.rotation;
        openRot = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    public void PuzzleSolved()
    {
        solvedCount++;
        if (solvedCount >= puzzlesRequired)
        {
            if (audioSource != null && unlockSound != null)
                audioSource.PlayOneShot(unlockSound);
        }
    }

    public void TryOpen()
    {
        if (isOpen) return;
        if (solvedCount < puzzlesRequired) return;
        StartCoroutine(OpenDoor());
    }

    public void TryOpenLocked()
    {
        if (isOpen || solvedCount >= puzzlesRequired) return;
        if (audioSource != null && lockedSound != null)
            audioSource.PlayOneShot(lockedSound);
    }

    IEnumerator OpenDoor()
    {
        isOpen = true;
        if (respawnZone != null)
            respawnZone.gameObject.SetActive(false);

        if (audioSource != null && doorOpenSound != null)
            audioSource.PlayOneShot(doorOpenSound);

        if (outdoorAmbienceSound != null)
        {
            audioSource.clip = outdoorAmbienceSound;
            audioSource.loop = true;
            audioSource.volume = 0f;
            audioSource.Play();
            StartCoroutine(FadeIn(audioSource, ambienceFadeInDuration));
        }

        if (musicSource != null && musicSource.isPlaying)
            StartCoroutine(FadeOut(musicSource, musicFadeOutDuration));

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / openDuration;
            transform.rotation = Quaternion.Lerp(closedRot, openRot, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        transform.rotation = openRot;
    }

    IEnumerator FadeIn(AudioSource source, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(0f, 1f, t / duration);
            yield return null;
        }
        source.volume = 1f;
    }

    IEnumerator FadeOut(AudioSource source, float duration)
    {
        float startVolume = source.volume;
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }
        source.volume = 0f;
        source.Stop();
    }
}