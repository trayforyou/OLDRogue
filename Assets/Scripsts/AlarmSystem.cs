using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Detector))]

public class AlarmSystem : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _speedGrowVolume = 1f;

    private AudioSource _audioSource;
    private Detector _detector;
    private float _minVolume;
    private float _maxVolume;

    private void Awake()
    {
        _detector = GetComponent<Detector>();
        _audioSource = GetComponent<AudioSource>();
        _minVolume = 0f;
        _maxVolume = 1f;
        _audioSource.volume = _minVolume;
        _detector.RogueHasLeft += ChangeAlarmDown;
        _detector.RogueCome += ChangeAlarmUp;
    }

    private void OnDisable()
    {
        _detector.RogueHasLeft -= ChangeAlarmDown;
        _detector.RogueCome -= ChangeAlarmUp;
    }

    private void ChangeAlarmUp()
    {
        if(_audioSource.volume == _minVolume)
            _audioSource.Play();

        StopAllCoroutines();

        StartCoroutine(TurnAlarmUp());
    }

    private void ChangeAlarmDown()
    {
        StopAllCoroutines();

        StartCoroutine(TurnAlarmDown());
    }

    private IEnumerator TurnAlarmUp()
    {
        bool isRun = true;

        while (isRun)
        {
            if (_audioSource.volume != _maxVolume)
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _speedGrowVolume * Time.deltaTime);
            else
                isRun = false;

            yield return null;
        }
    }

    private IEnumerator TurnAlarmDown()
    {
        bool isRun = true;

        while (isRun)
        {
            if (_audioSource.volume != _minVolume)
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _speedGrowVolume * Time.deltaTime);
            else
                isRun = false;

            yield return null;
        }

        _audioSource.Stop();
    }
}