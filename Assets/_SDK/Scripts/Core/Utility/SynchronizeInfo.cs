using System;
using System.Collections;
using UnityEngine;

namespace apps.Utility
{
    public struct SynchronizeInfo : IEnumerable
    {
        public bool IsCompleted;

        private float _startedTime;
        private Action _action;

        public float Time { get; private set; }
        public float TimePassed => UnityEngine.Time.time - _startedTime;
        public float TimeToFinish => Time - TimePassed;

        public SynchronizeInfo(Action action, float time)
        {
            Time = time;
            IsCompleted = false;

            _startedTime = UnityEngine.Time.time;
            _action = action ?? throw new ArgumentNullException("The action has a null value!...");
        }

        public IEnumerator GetEnumerator()
        {
            yield return new WaitForSeconds(Time);

            IsCompleted = true;
            _action?.Invoke();
        }
    }
}
