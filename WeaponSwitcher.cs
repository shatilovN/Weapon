using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    private int _activeWeaponIndex = 0;
    [SerializeField] WeaponUI _weaponUI;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        SwitchWeapon();
    }

    private void SelectWeapon()
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == _activeWeaponIndex)
            {
                weapon.gameObject.SetActive(true);
                _weaponUI.ShowRoundsInfo(weapon.GetComponent<Weapon>().CurrentAmmoInMagazine, weapon.GetComponent<Weapon>().CurrentAmmo);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;

        }
    }

    private void SwitchWeapon()
    {
        if (PlayerInput.GetWeaponChangeInput() == 1)
        {
            if (_activeWeaponIndex >= transform.childCount - 1)
            _activeWeaponIndex = 0;

            else
            _activeWeaponIndex++;

            SelectWeapon();
        }

        else if (PlayerInput.GetWeaponChangeInput() == -1)
        {
            if (_activeWeaponIndex <= 0)
                _activeWeaponIndex = transform.childCount - 1;

            else
                _activeWeaponIndex--;

            SelectWeapon();
        }
    }
}
