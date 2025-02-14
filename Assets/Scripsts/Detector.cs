using System;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private bool _isRogueItHome;

    public event Action<bool> RogueChangedState;

    private void Awake()
    {
        _isRogueItHome = false;
    }

    private void OnTriggerEnter(Collider other) => ChangeHomeState(other);

    private void OnTriggerExit(Collider other) => ChangeHomeState(other);

    private void ChangeHomeState(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<Rogue>(out _))
        {
            _isRogueItHome = !_isRogueItHome;
            RogueChangedState?.Invoke(_isRogueItHome);
        }
    }
}
