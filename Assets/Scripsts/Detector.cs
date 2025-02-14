using System;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public event Action RogueHasLeft;
    public event Action RogueCome;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Rogue>(out _))
            RogueCome?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Rogue>(out _))
            RogueHasLeft?.Invoke();
    }
}