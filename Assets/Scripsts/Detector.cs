using System;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public event Action RogueHasLeft;
    public event Action RogueCome;

    private void OnTriggerEnter(Collider other) => RogueCome?.Invoke();

    private void OnTriggerExit(Collider other) => RogueHasLeft?.Invoke();
}