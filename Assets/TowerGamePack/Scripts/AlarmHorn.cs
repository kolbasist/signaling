using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(AudioSource))]

public class AlarmHorn : MonoBehaviour
{   
    [SerializeField] private float _fadeDuration;
    [SerializeField] private AudioClip _clip;

    private AudioSource _source;
    private Coroutine _fadeInJob;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.clip = _clip;
        _source.loop = true;
        _source.volume = _minVolume;
    }

    public void SetOn()
    {
        _fadeInJob = StartCoroutine(FadeVolume(_maxVolume));
        _source.Play();
    }

    public void SetOff()
    {
        if (_fadeInJob != null)
            StopCoroutine(_fadeInJob);
        StartCoroutine(FadeVolume(_minVolume));
    }

    private IEnumerator FadeVolume(float value)
    {
        while (_source.volume != value)
        {
            _source.volume = Mathf.MoveTowards(_source.volume, value, Time.deltaTime / _fadeDuration);            
            yield return null;
        }

        if (_source.volume == _minVolume)
            _source.Stop();
    }    
}
