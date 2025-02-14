using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Detector))]

public class AlarmSystem : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _speedGrowVolume = 1f;

    private AudioSource _audioSource;
    private Detector _detector;
    private Coroutine _coroutine;
    private float _minVolume;
    private float _maxVolume;
    private bool _isAlarm;

    private void Awake()
    {
        _detector = GetComponent<Detector>();
        _audioSource = GetComponent<AudioSource>();
        _minVolume = 0f;
        _maxVolume = 1f;
        _audioSource.volume = _minVolume;
        _detector.RogueChangedState += ChangeAlarmState;
    }

    private void OnDisable() =>
        _detector.RogueChangedState -= ChangeAlarmState;

    private void ChangeAlarmState(bool isAlarm)
    {

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (isAlarm)
        {
            _coroutine = StartCoroutine(TurnAlarm(_maxVolume));
        _audioSource.Play();
        }
        else
        {
            _coroutine = StartCoroutine(TurnAlarm(_minVolume));
        }
    }

    private IEnumerator TurnAlarm(float targetVolume)
    {
        _isAlarm = true;

        while (_isAlarm)
        {
            if (_audioSource.volume != targetVolume)
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speedGrowVolume * Time.deltaTime);
            else
                _isAlarm = false;

            yield return null;
        }

        if (_audioSource.volume == _minVolume)
            _audioSource.Stop();
    }
}