using UnityEngine;

public class ShootingWeapon : Weapon
{
    [SerializeField] Transform firePoint;
    private int _currentAmmo;
    private float _reloadTimer = 0;
    private float _attackTimer = 0;

    private new void Awake()
    {
        base.Awake();
        _currentAmmo = weaponData.MagazineCapacity;
    }

    protected override void Update()
    {
        base.Update();
        _reloadTimer -= Time.deltaTime;
        _attackTimer -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(weaponData.ProjectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * weaponData.ProjectileSpeed;
        bullet.GetComponent<Bullet>().SetDamage(weaponData.BaseDamage);
        _currentAmmo--;
        _attackTimer = weaponData.BaseAttackSpeed;
        Debug.Log(_currentAmmo);
    }

    private void TryShoot()
    {
        if (_currentAmmo > 0 && _reloadTimer <= 0 && _attackTimer <= 0)
        {
            Shoot();
        }
        else if (_reloadTimer > 0)
        {
            
        }
        else if (_attackTimer > 0)
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
