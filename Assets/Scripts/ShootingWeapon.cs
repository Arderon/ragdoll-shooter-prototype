using UnityEngine;

public class ShootingWeapon : Weapon
{
    [SerializeField] Transform firePoint;
    private int _currentAmmo;
    private float _reloadTimer = 0;

    private new void Awake()
    {
        base.Awake();
        _currentAmmo = weaponData.MagazineCapacity;
    }

    void Update()
    {
        _reloadTimer -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(weaponData.ProjectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * weaponData.ProjectileSpeed;
        bullet.GetComponent<Bullet>().SetDamage(weaponData.BaseDamage);
        _currentAmmo--;
        Debug.Log(_currentAmmo);
    }

    private void TryShoot()
    {
        if (_currentAmmo > 0 && _reloadTimer <= 0)
        {
            Shoot();
        }
        else if (_reloadTimer > 0)
        {
            
        }
        else
        {
            Reload();
        }
    }

    private void Reload()
    {
        _reloadTimer = weaponData.ReloadTime;
        _currentAmmo = weaponData.MagazineCapacity;
    }

    public override void Use()
    {
        TryShoot();
    }
}
