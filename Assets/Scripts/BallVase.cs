using UnityEngine;

public class BallInVase : MonoBehaviour
{
    Rigidbody rb;
    Vector3 startPosition;
    Quaternion startRotation;
    bool released = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void Release()
    {
        released = true;
        rb.isKinematic = false;
    }

    public void DoRespawn()
    {
        if (released) return;
        rb.isKinematic = true;
        transform.SetPositionAndRotation(startPosition, startRotation);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        if (!released && transform.position.y < -10)
            DoRespawn();
    }
}