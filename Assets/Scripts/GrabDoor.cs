using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabDoor : MonoBehaviour
{
    [SerializeField] float minAngle = -110f;
    [SerializeField] float maxAngle = 0f;
    [SerializeField] float smoothing = 12f;
    [SerializeField] float momentum = 0.95f;
    [SerializeField] float shakeMagnitude = 2f;
    [SerializeField] float shakeSpeed = 20f;
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] bool locked = true;
    public XRSimpleInteractable knob;
    public RespawnZone respawnZone;
    public AudioSource audioSource;
    public AudioClip tryOpenSound;
    public AudioClip unlockSound;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    public AudioClip doorShutSound;
    XRSimpleInteractable interactable;
    IXRSelectInteractor grabbingHand;
    Vector3 grabStartDir;
    float grabStartAngle;
    float targetAngle;
    float velocity = 0f;
    bool shaking = false;
    float shakeTimer = 0f;
    bool wasOpening = false;
    bool wasClosing = false;

    void Awake()
    {
        interactable = knob != null ? knob : GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnGrab);
        interactable.selectExited.AddListener(OnRelease);
        targetAngle = transform.localEulerAngles.y;
    }

    public void Unlock()
    {
        locked = false;
        if (audioSource != null && unlockSound != null)
            audioSource.PlayOneShot(unlockSound);
        if (respawnZone != null)
            respawnZone.gameObject.SetActive(false);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if (locked)
        {
            shaking = true;
            shakeTimer = 0f;
            if (audioSource != null && tryOpenSound != null)
                audioSource.PlayOneShot(tryOpenSound);
            return;
        }
        velocity = 0f;
        grabbingHand = args.interactorObject;
        grabStartDir = FlatDirToHand();
        grabStartAngle = targetAngle;
    }

    void OnRelease(SelectExitEventArgs args) => grabbingHand = null;

    void Update()
    {
        if (shaking)
        {
            shakeTimer += Time.deltaTime;
            float shake = Mathf.Sin(shakeTimer * shakeSpeed) * shakeMagnitude * (1 - shakeTimer / shakeDuration);
            transform.localEulerAngles = new Vector3(0, targetAngle + shake, 0);
            if (shakeTimer >= shakeDuration)
                shaking = false;
            return;
        }

        if (grabbingHand != null)
        {
            Vector3 currentDir = FlatDirToHand();
            float delta = Vector3.SignedAngle(grabStartDir, currentDir, Vector3.up);
            float newAngle = Mathf.Clamp(grabStartAngle + delta, minAngle, maxAngle);
            velocity = newAngle - targetAngle;
            targetAngle = newAngle;
        }
        else
        {
            targetAngle = Mathf.Clamp(targetAngle + velocity, minAngle, maxAngle);
            velocity *= momentum;
        }

        if (velocity < -0.1f && !wasOpening)
        {
            wasOpening = true;
            wasClosing = false;
            if (audioSource != null && doorOpenSound != null)
                audioSource.PlayOneShot(doorOpenSound);
        }

        if (velocity > 0.1f && !wasClosing)
        {
            wasClosing = true;
            wasOpening = false;
            if (audioSource != null && doorCloseSound != null)
                audioSource.PlayOneShot(doorCloseSound);
        }

        if (Mathf.Abs(velocity) < 0.01f && targetAngle >= maxAngle - 0.5f)
        {
            if (wasClosing)
            {
                wasClosing = false;
                if (audioSource != null && doorShutSound != null)
                    audioSource.PlayOneShot(doorShutSound);
            }
        }

        if (Mathf.Abs(velocity) < 0.01f)
        {
            wasOpening = false;
            wasClosing = false;
        }

        float y = Mathf.LerpAngle(transform.localEulerAngles.y, targetAngle, Time.deltaTime * smoothing);
        transform.localEulerAngles = new Vector3(0, y, 0);
    }

    Vector3 FlatDirToHand()
    {
        Vector3 handPos = grabbingHand.transform.position;
        Vector3 dir = handPos - transform.position;
        dir.y = 0;
        return dir.normalized;
    }
}