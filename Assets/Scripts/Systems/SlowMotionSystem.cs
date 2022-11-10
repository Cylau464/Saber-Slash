using Cysharp.Threading.Tasks;
using States.Characters.Enemy;
using System;
using System.Collections.Generic;
using System.Threading;
using Triggers;
using UnityEngine;

namespace Game.Systems
{
    public static class SlowMotionSystem
    {
        private static float _slowMoValue = .2f;
        private static CancellationTokenSource _activateTokenSource;
        private static UniTask _deactivateDelayTask;
        private static List<ISlowMotionActivator> _activators = new List<ISlowMotionActivator>();
        private static bool _manualDeactivation;

        public static Action OnActivated { get; set; }
        public static Action OnDeactivated { get; set; }

        public static void Activate()
        {
            Time.timeScale = _slowMoValue;
            OnActivated?.Invoke();
        }

        public static void Activate(float value, bool manualDeactivation)
        {
            Time.timeScale = value;
            _manualDeactivation = manualDeactivation;
        }

        public static async void Activate(float duration, ISlowMotionActivator activator)
        {
            if (_manualDeactivation == true)
                return;

            Activate();
            //CancelTask();
            _activateTokenSource = new CancellationTokenSource();
            _activators.Add(activator);
            
            try
            {
                _deactivateDelayTask = UniTask.Delay(TimeSpan.FromSeconds(duration), ignoreTimeScale: true, cancellationToken: _activateTokenSource.Token);
                await _deactivateDelayTask;
                Deactivate(activator, 0f);
            }
            catch (OperationCanceledException ex)
            {
                Debug.Log("Slow Mo deactivation delay " + ex.Message);
            }
        }

        public static void Deactivate(ISlowMotionActivator activator, float duration)
        {
            if (_manualDeactivation == true)
                return;

            if (_activators.Contains(activator) == false)
            {
                if (duration > 0f)
                    Activate(duration, activator);

                return;
            }

            _activators.Remove(activator);

            if (_activators.Count > 0)
                return;

            if (duration > 0f)
                Activate(duration, activator);
            else
                Deactivate();
        }

        public static void Deactivate()
        {
            Time.timeScale = 1f;
            _manualDeactivation = false;
            //CancelTask();
            OnDeactivated?.Invoke();
        }

        private static void CancelTask()
        {
            if (_activateTokenSource != null && _activateTokenSource.IsCancellationRequested == false)
                _activateTokenSource.Cancel();
        }

        public static void Reset()
        {
            _manualDeactivation = false;
            _activators.Clear();
        }
    }
}