using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public GameObject hitVFX;
    private ShootingWeapon weapon;
    private int damage = 0;
    private string damagableLayerName = "Damagable";
    private int damagableLayerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        damagableLayerMask = LayerMask.NameToLayer(damagableLayerName);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the bullet on collision
        GameObject vfx = Instantiate(hitVFX);
        vfx.transform.position = collision.contacts[0].point;
        vfx.transform.rotation = Quaternion.LookRotation(-transform.forward);
        if (collision.gameObject.layer == damagableLayerMask)
        {
            IDamagable damagable = FindDamagableComponent(collision.gameObject);
            if (damagable != null)
            {
                damagable.TakeDamage(damage);
            }
            StartCoroutine(WaitAndDestroy(0.01f));
        }
        else
        {
            Destroy(gameObject);
        }
        
        //Destroy(gameObject);
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private IDamagable FindDamagableComponent(GameObject obj)
    {
        IDamagable damagable = null;
        Transform currentTransform = obj.transform;
        while (damagable == null)
        {
            Debug.Log(currentTransform.gameObject.name);
            damagable = currentTransform.GetComponent<IDamagable>();
            if (damagable != null || currentTransform.parent == null) break;
            currentTransform = currentTransform.parent;
        }
        return damagable;
    }

    IEnumerator WaitAndDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
