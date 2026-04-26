using System.Collections;
using UnityEngine;

public class FracturedVase : MonoBehaviour
{
    public float freezeAfter = 2f;

    void OnEnable()
    {
        StartCoroutine(FreezeAfterDelay());
    }

    IEnumerator FreezeAfterDelay()
    {
        yield return new WaitForSeconds(freezeAfter);
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            rb.isKinematic = true;
    }
}