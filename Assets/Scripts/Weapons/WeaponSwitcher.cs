using UnityEngine;
using System.Linq;

namespace Weapons
{
    public class WeaponSwitcher : MonoBehaviour
    {
        [SerializeField] private Weapon[] _weapons;

        public Weapon CurrentWeapon { get; private set; }

        private void Start()
        {
            CurrentWeapon = _weapons.FirstOrDefault(x => x.Type == WeaponType.Sword);
        }

        public void SwitchTo(WeaponType type)
        {
            Weapon weapon = _weapons.FirstOrDefault(x => x.Type == type);

            if (CurrentWeapon != weapon)
                CurrentWeapon.Unequip(weapon.Equip);
            else
                weapon.Equip();

            CurrentWeapon = weapon;
        }
    }
}