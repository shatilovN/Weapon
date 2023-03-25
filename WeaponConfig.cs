using UnityEngine;

[CreateAssetMenu(fileName = "Weapon config", menuName = "New weapon config")]
public class WeaponConfig : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _currentAmmo;
    [SerializeField] private int _currentAmmoInMagazine;
    [SerializeField] private float _damage;
    [SerializeField] private float _firingRange;
    [SerializeField] private float _rateOfFire;
    [SerializeField] private float _reloadTime;
    [SerializeField] private ParticleSystem _shotEfect;
    [SerializeField] private AudioClip _shotClip;
    [SerializeField] private float _repulsiveForse;
    


    public string Name => _name;

    public int CurrentAmmo => _currentAmmo;

    public int CurrentAmmoInMagazine => _currentAmmoInMagazine;

    public float Damage => _damage;

    public float FiringRange => _firingRange;

    public float RateOfFire => _rateOfFire;

    public float ReloadTime => _reloadTime;

    public ParticleSystem ShotEfect => _shotEfect;

    public AudioClip ShotClip => _shotClip;

    public float RepulsiveForse => _repulsiveForse;

}
