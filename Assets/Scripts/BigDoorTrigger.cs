using UnityEngine;

public class BigDoorTrigger : MonoBehaviour
{
    public BigDoor door;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            door.TryOpen();
    }
}