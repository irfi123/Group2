using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Respawn respawn = other.GetComponent<Respawn>();
        if (respawn != null)
            respawn.DoRespawn();
    }
}
