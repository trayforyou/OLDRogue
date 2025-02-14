using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private bool _isRogueItHome;

    public event Action<bool> RogueChangedState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Rogue>(out _))
        {
            _isRogueItHome = true;
            RogueChangedState?.Invoke(_isRogueItHome);
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Rogue>(out _))
        {
            _isRogueItHome = false;
            RogueChangedState?.Invoke(_isRogueItHome);
        }
    }
}
