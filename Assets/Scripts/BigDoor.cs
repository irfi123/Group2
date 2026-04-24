using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDoor : MonoBehaviour
{
    [Header("Settings")]
    public int puzzlesRequired = 1;
    public float openDuration = 5f;
    public float openAngle = 110f;
    public RespawnZone respawnZone;
    public AudioSource audioSource;
    public AudioClip doorOpenSound;

    private int solvedCount = 0;
    private bool isOpen = false;
    private Quaternion closedRot;
    private Quaternion openRot;

    void Start()
    {
        closedRot = transform.rotation;
        openRot = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    public void PuzzleSolved()
    {
        solvedCount++;
    }

    public void TryOpen()
    {
        if (isOpen) return;
        if (solvedCount < puzzlesRequired) return;
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        isOpen = true;
        if (respawnZone != null)
            respawnZone.gameObject.SetActive(false);
        if (audioSource != null && doorOpenSound != null)
            audioSource.PlayOneShot(doorOpenSound);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / openDuration;
            transform.rotation = Quaternion.Lerp(closedRot, openRot, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        transform.rotation = openRot;
    }
}