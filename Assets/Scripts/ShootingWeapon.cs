using UnityEngine;

public class ShootingWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public int damage = 10;
    void LateUpdate()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    Shoot();
        //}
    }
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * bulletSpeed;
        bullet.GetComponent<Bullet>().SetDamage(damage);
    }
}
