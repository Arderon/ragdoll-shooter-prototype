using UnityEngine;

public class ShootingWeapon : Weapon
{
    public Transform firePoint;
    void LateUpdate()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    Shoot();
        //}
    }
    public void Shoot()
    {
        GameObject bullet = Instantiate(weaponData.ProjectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * weaponData.ProjectileSpeed;
        bullet.GetComponent<Bullet>().SetDamage(weaponData.BaseDamage);
    }

    public override void Use()
    {
        Shoot();
    }
}
