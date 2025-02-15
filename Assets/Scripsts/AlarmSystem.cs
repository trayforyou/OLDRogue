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

    private void ChangeAlarmUp() =>
        ActivateAlarm(_maxVolume);

    private void ChangeAlarmDown() =>
        ActivateAlarm(_minVolume);

    private void ActivateAlarm(float volume)
    {
        if (!_audioSource.isPlaying)
            _audioSource.Play();

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeVolume(volume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        bool isRun = true;

        while (isRun)
        {
            if (_audioSource.volume != targetVolume)
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speedGrowVolume * Time.deltaTime);
            else
                isRun = false;

            yield return null;
        }

        if (_audioSource.volume == _minVolume)
            _audioSource.Stop();
    }
}