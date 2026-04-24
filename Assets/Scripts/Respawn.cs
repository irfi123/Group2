using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (transform.position.y < -10)
            DoRespawn();
    }

    public void DoRespawn()
    {
        transform.SetPositionAndRotation(originalPosition, originalRotation);
    }
}