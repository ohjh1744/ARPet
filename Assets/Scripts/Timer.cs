using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Text;
using System.Data;

public class Timer : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _textTimer;

    private StringBuilder _lastTime;

    private StringBuilder _currentTime;

    private DateTime _now;

    private bool _isTimeChange;

    public bool IsTimeChange { get { return _isTimeChange; } }

    private Coroutine _routine;

    [SerializeField] private float _routineDurate; 

    private float _waitTime;

    private void Awake()
    {
        _lastTime = new StringBuilder();
        _currentTime = new StringBuilder();
        UpdateTime();
        _lastTime.Clear().Append(_currentTime);
    }

    private void Start()
    {
        _isTimeChange = false;
        _waitTime = _routineDurate;
        _textTimer.SetText(_currentTime.ToString());
    }

    void Update()
    {
        UpdateTime();

        if (_currentTime.ToString() != _lastTime.ToString())
        {
            _routine = StartCoroutine(TimeChange());
            _textTimer.SetText(_currentTime.ToString());
            _lastTime.Clear().Append(_currentTime);
        }

    }

    private void UpdateTime()
    {
        _now = DateTime.Now;
        _currentTime.Clear();
        _currentTime.Append($"{_now.Hour:D2}:{_now.Minute:D2}");
    }

    private IEnumerator TimeChange()
    {
        _isTimeChange = true;
        yield return _waitTime;

        _isTimeChange = false;
        _routine = null;
    }
}
