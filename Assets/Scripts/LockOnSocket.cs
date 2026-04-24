using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class LockOnSocket : MonoBehaviour
{
    XRSocketInteractor socket;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnItemPlaced);
    }

    void OnItemPlaced(SelectEnterEventArgs args)
    {
        StartCoroutine(LockAfterDelay(args.interactableObject.transform.GetComponent<XRGrabInteractable>()));
    }

    IEnumerator LockAfterDelay(XRGrabInteractable grab)
    {
        yield return new WaitForSeconds(2f);
        if (grab != null)
        {
            grab.throwOnDetach = false;
            grab.trackPosition = false;
            grab.trackRotation = false;
            Rigidbody rb = grab.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }
    }
}