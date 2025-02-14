using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AlarmSystem : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _speedGrowVolume = 1f;
    [SerializeField] float _timeOfWite = 1f;

    private AudioSource _audioSource;
    private float _minVolume;
    private bool _isRogueItHome;
    private bool _isAlarmOff;
    private float _maxVolume;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _isAlarmOff = true;
        _minVolume = 0f;
        _maxVolume = 1f;
        _audioSource.volume = _minVolume;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isRogueItHome = true;

        if (collision.gameObject.TryGetComponent<Rogue>(out _) && _isAlarmOff)
            StartCoroutine(TurnOnAlarm());
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Rogue>(out _))
            _isRogueItHome = false;
    }

    private IEnumerator TurnOnAlarm()
    {
        _isAlarmOff = false;

        _audioSource.Play();

        var wait = new WaitForSecondsRealtime(_timeOfWite);

        while (!_isAlarmOff)
        {
            if ((_audioSource.volume != _maxVolume) && _isRogueItHome)
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _speedGrowVolume);
            }
            else if ((_audioSource.volume != _minVolume) && !_isRogueItHome)
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _speedGrowVolume);
            }
            else if ((_audioSource.volume == _minVolume) && !_isRogueItHome)
            {
                _isAlarmOff = true;
            }

            yield return wait;
        }

        _audioSource.Stop();

        yield break;
    }
}