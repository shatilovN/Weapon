using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    
    [SerializeField] private WeaponConfig _weaponConfig;
    [SerializeField] private Transform _mainCameraTransform;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private WeaponUI _weaponUI;

    private Animator _weaponAnimator;
    private AudioSource _weaponAudioSourse;
    private float _nextFire = 0f;
    private bool _isReloading = false;
    private bool _noRounds = false;

    public int CurrentAmmo { get; private set; }

    public int CurrentAmmoInMagazine { get; private set; }

    public static bool IsShooting { get; private set; }

    private void Start()
    {
        _weaponAnimator = GetComponent<Animator>();
        _weaponAudioSourse = GetComponent<AudioSource>();
        _weaponAudioSourse.clip = _weaponConfig.ShotClip;
        CurrentAmmoInMagazine = _weaponConfig.CurrentAmmoInMagazine;
        CurrentAmmo = _weaponConfig.CurrentAmmo;

        _weaponUI.ShowRoundsInfo(CurrentAmmoInMagazine, CurrentAmmo);
    }

    private void Update()
    {
        nextFire += Time.deltaTime;
        CheckShooting();
        _weaponAnimator.SetBool(GlobalStringVariables.IS_SHOOTING, IsShooting);
        CheckReload();
    }

    private void Shooting()
    {
        if (!_isReloading && !_noRounds)
        {
            ParticleSystem shotEfect = Instantiate(_weaponConfig.ShotEfect, _bulletSpawn.position, _bulletSpawn.rotation);
            Destroy(shotEfect, 1f);

            RaycastHit hit;

            if (Physics.Raycast(_mainCameraTransform.position, _mainCameraTransform.forward, out hit, _weaponConfig.FiringRange))
            {
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * _weaponConfig.RepulsiveForse, ForceMode.Impulse);

                    Enemy enemy = hit.collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.OnTakeDamage(_weaponConfig.Damage, hit);
                    }
                }
            }

            CurrentAmmoInMagazine--;
            nextFire = 0f;
            _weaponAudioSourse.Play();
            _weaponUI.ShowRoundsInfo(CurrentAmmoInMagazine, CurrentAmmo);
            IsShooting = true;
        }
    }

    private void CheckShooting()
    {
        IsShooting = false;
        if (PlayerInput.GetShootInput())
            if (_weaponConfig.RateOfFire < nextFire)
                Shooting();
    }

    private void CheckReload()
    {
        if (CurrentAmmoInMagazine <= 0 || PlayerInput.CheckWeaponReload())
            StartCoroutine(ReloadCorutine());

    }

    private IEnumerator ReloadCorutine()
    {

        if (CurrentAmmo > 0)
        {
            int ammoForFullMagazine;
            _isReloading = true;

            ammoForFullMagazine = _weaponConfig.CurrentAmmoInMagazine - CurrentAmmoInMagazine;

            if (CurrentAmmo >= ammoForFullMagazine)
            {
                CurrentAmmo -= ammoForFullMagazine;
                CurrentAmmoInMagazine += ammoForFullMagazine;
            }

            else
            {
                CurrentAmmoInMagazine += CurrentAmmo;
                CurrentAmmo = 0;
            }

            
            _weaponAnimator.SetBool(GlobalStringVariables.IS_RELOADING, _isReloading);

            yield return new WaitForSeconds(_weaponConfig.ReloadTime);

            _isReloading = false;
            _weaponAnimator.SetBool(GlobalStringVariables.IS_RELOADING, _isReloading);
            _weaponUI.ShowRoundsInfo(CurrentAmmoInMagazine, CurrentAmmo);
        }

        else
        {
            _noRounds = true;
        }
    }
}


