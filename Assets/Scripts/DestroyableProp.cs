using UnityEngine;
using System;

public class DestroyableProp : MonoBehaviour, IDestroyable, IDamagable
{
    [SerializeField] Transform destroyableParent;

    int _health;
    int _maxHealth;
    private bool _destroyed = false;

    private int Health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = Math.Clamp(value, 0, _maxHealth);
            if(_health <= 0 && !_destroyed)
            {
                Destroy();
            }
        }
    }

    public void Start()
    {
        DisablePartsPhisics(destroyableParent);
    }

    public void Destroy()
    {
        Rigidbody rb = destroyableParent.GetComponent<Rigidbody>();
        Collider coll = destroyableParent.GetComponent<Collider>();
        if (rb != null) rb.isKinematic = true;
        if (coll != null) coll.enabled = false;
        EnablePartsPhisics(destroyableParent);
        _destroyed = true;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public void DisablePartsPhisics(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            Collider coll = child.GetComponent<Collider>();
            if (rb != null) rb.isKinematic = true;
            if (coll != null) coll.enabled = false;
        }
    }

    public void EnablePartsPhisics(Transform parent)
    {
        foreach(Transform child in parent)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            Collider coll = child.GetComponent<Collider>();
            if (rb != null) rb.isKinematic = false;
            if (coll != null) coll.enabled = true;
        }
    }
}
