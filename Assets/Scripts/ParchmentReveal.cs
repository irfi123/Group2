using System.Collections;
using UnityEngine;

public class ParchmentReveal : MonoBehaviour
{
    public float revealDuration = 1f;
    public float displayDuration = 10f;
    public AudioSource audioSource;
    public AudioClip voiceOver;
    RectTransform rect;
    Vector2 fullSize;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        fullSize = rect.sizeDelta;
    }

    void OnEnable()
    {
        if (audioSource != null && voiceOver != null)
            audioSource.PlayOneShot(voiceOver);
        StartCoroutine(Reveal());
    }

    IEnumerator Reveal()
    {
        rect.sizeDelta = new Vector2(0, 0);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / revealDuration;
            rect.sizeDelta = Vector2.Lerp(Vector2.zero, fullSize, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        rect.sizeDelta = fullSize;
        yield return new WaitForSeconds(displayDuration);
        gameObject.SetActive(false);
    }
}