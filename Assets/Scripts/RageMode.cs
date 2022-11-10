using Game.Systems;
using System;
using System.Collections;
using UnityEngine;

public class RageMode : MonoBehaviour
{
    [SerializeField] private int _pointsForActivation = 100;
    [SerializeField] private float _duration = 10f;
    [SerializeField] private float _slowMoValue = .5f;
    
    private int _points;
    private bool _isActive;
    private float Progress => (float)_points / _pointsForActivation;

    public Action<float> OnPointsChanged { get; set; }
    public Action OnActivated { get; set; }
    public Action OnDeactivated { get; set; }

    private void Start()
    {
        SlowMotionSystem.Reset();
    }

    public void AddPoints(int points)
    {
        if (_isActive == true) return;

        _points += points;
        OnPointsChanged?.Invoke(Progress);

        //if (Progress == 1f)
        //    Activate();
    }

    private void Activate()
    {
        if (_isActive == true) return;

        _isActive = true;
        _points = 0;
        SlowMotionSystem.Activate(_slowMoValue, true);
        OnActivated?.Invoke();
        StartCoroutine(Rage());
    }

    private IEnumerator Rage()
    {
        float t = 0f;
        int startPoints = _points;

        while (t < 1f)
        {
            t += (Time.deltaTime / Time.timeScale) / _duration;
            _points = (int)Mathf.Lerp(startPoints, 0f, t);
            OnPointsChanged?.Invoke(Progress);

            yield return null;
        }

        _isActive = false;
        SlowMotionSystem.Deactivate();
        OnDeactivated?.Invoke();
    }
}