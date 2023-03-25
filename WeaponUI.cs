using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Text _ammoInMagazineText;
    [SerializeField] private Text _currentAmmoText;
     
    public void ShowRoundsInfo(int ammoInMagazine, int currentAmmo)
    {
        _ammoInMagazineText.text = Convert.ToString(ammoInMagazine);
        _currentAmmoText.text = Convert.ToString(currentAmmo);
    }
}
