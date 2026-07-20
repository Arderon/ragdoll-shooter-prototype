using UnityEngine;

public class ShootingWeapon : Weapon
{
    [SerializeField] Transform firePoint;
    private int _currentAmmo;
    private float _reloadTimer = 0;
    private float _attackTimer = 0;
    private bool _holdingShoot = false;
    private bool _shootRequested = false;

    protected new void Awake()
    {
        base.Awake();
        _currentAmmo = weaponData.MagazineCapacity;
    }

    protected override void Update()
    {
        base.Update();
        _reloadTimer -= Time.deltaTime;
        _attackTimer -= Time.deltaTime;
        if (weaponData.IsAutomatic)
        {
            if (_shootRequested)
            {
                TryShoot();
            }
        }
        
    }

    protected virtual void Shoot()
    {
        for(int i = 0; i < weaponData.ProjectilesPerShoot; i++)
        {
            Quaternion rotation = GetSpreadRotation(firePoint.rotation);
            GameObject bullet = Instantiate(weaponData.ProjectilePrefab, firePoint.position, rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = bullet.transform.forward * weaponData.ProjectileSpeed;
            bullet.GetComponent<Bullet>().SetDamage(weaponData.BaseDamage);
        }
  
        _currentAmmo--;
        _attackTimer = weaponData.BaseAttackSpeed;
        Debug.Log(_currentAmmo);
    }

    protected void TryShoot()
    {
        if (_currentAmmo > 0 && _reloadTimer <= 0 && _attackTimer <= 0)
        {
            if (weaponData.IsAutomatic)
            {
                if (_holdingShoot)
                {
                    Shoot();
                    _shootRequested = true;
                }
                else
                {
                    _shootRequested = false;
                }

            }
            else
            {
                Shoot();
            }
        }
        else if (_reloadTimer > 0)
        {
            _shootRequested = false;
        }
        else if (_attackTimer > 0)
        {
            if (weaponData.IsAutomatic)
            {
                if (_holdingShoot)
                {
                    _shootRequested = true;
                }

            }
        }
        else
        {
            Reload();
            _shootRequested = false;
        }
    }

    protected void Reload()
    {
        _reloadTimer = weaponData.ReloadTime;
        _currentAmmo = weaponData.MagazineCapacity;
    }

    protected Quaternion GetSpreadRotation(Quaternion originalRotation)
    {
        float randomX = Random.Range(-weaponData.SpreadY, weaponData.SpreadY);
        float randomY = Random.Range(-weaponData.SpreadX, weaponData.SpreadX);

        Quaternion spreadRotation = Quaternion.Euler(randomX, randomY, 0f);

        return originalRotation * spreadRotation;
    }

    public override void Use()
    {
        if (_shootRequested) return;
        _holdingShoot = true;
        TryShoot();
    }

    public override void StopUse()
    {
        _holdingShoot = false;
    }
}
