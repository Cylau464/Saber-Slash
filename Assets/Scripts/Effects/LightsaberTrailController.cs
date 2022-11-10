using UnityEngine;
using Weapons;

/// <summary>
/// Controller class which uses the LightsaberTrail class to show a trail when we move the lightsaber
/// </summary>
[RequireComponent(typeof(LightsaberTrail))]
public class LightsaberTrailController : MonoBehaviour {

    [SerializeField] private Weapon _weapon;

    LightsaberTrail lightsaberTrail;

    void Start()
    {
        lightsaberTrail = GetComponent<LightsaberTrail>();
    }

    void Update()
    {
        if (_weapon != null)
        {
            if (_weapon.Moving == true)
                lightsaberTrail.Iterate(Time.unscaledTime);
        }
        else
        {
            lightsaberTrail.Iterate(Time.unscaledTime);
        }

        lightsaberTrail.UpdateTrail(Time.unscaledTime, 0f);
    }

}
