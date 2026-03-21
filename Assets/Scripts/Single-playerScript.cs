using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonNetworkLightController : MonoBehaviour
{
    Light lightSource;
    bool lit;
    void Start()
    {
        lightSource = GetComponent<Light>();
        lit = lightSource.enabled;
    }
    public void Toggle()
    {
        lit = !lit;
        lightSource.enabled = lit;
    }
}
