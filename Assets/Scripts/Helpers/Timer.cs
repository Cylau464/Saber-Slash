using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Helpers
{
    public class Timer
    {
        private float _currentTime = 0;
        private float _time = 0;

        private bool _isStarted = false;
        public bool isStarted => _isStarted;

        private Action _callback;

        public Timer(float time, Action callback)
        {
            _time = time;
            _callback = callback;
        }

        public void Start()
        {
            _currentTime = 0f;
            _isStarted = true;
        }

        public void Tick(float deltaTime)
        {
            if (!_isStarted)
            {
                Debug.Log("Timer is not started");
                return;
            }

            _currentTime += deltaTime;

            if (_currentTime > _time)
            {
                _callback?.Invoke();
                _isStarted = false;
            }
        }
    }
}