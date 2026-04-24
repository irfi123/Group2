using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
public class SocketPuzzle : MonoBehaviour
{
    [SerializeField] XRSocketInteractor[] sockets;
    public UnityEvent onSolved;
    bool solved;
    void Start()
    {
        foreach (var s in sockets)
        {
            s.selectEntered.AddListener(_ => CheckSolved());
            s.selectExited.AddListener(_ => CheckSolved());
        }
    }
    void CheckSolved()
    {
        if (solved) return;
        foreach (var s in sockets)
            if (!s.hasSelection) return;
        solved = true;
        onSolved.Invoke();
    }
}
