using UnityEngine;
using System;

public class DestroyableProp : MonoBehaviour, IDestroyable, IDamagable
{
    [SerializeField] Transform destroyableParent;
    [SerializeField] int _maxHealth;
    [SerializeField] float scatterPartsMultiplier;

    int _health;
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

    public void Awake()
    {
        _health = _maxHealth;
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
        ScatterParts(destroyableParent, scatterPartsMultiplier);
        _destroyed = true;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    private void DisablePartsPhisics(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            Collider coll = child.GetComponent<Collider>();
            if (rb != null) rb.isKinematic = true;
            if (coll != null) coll.enabled = false;
        }
    }

    private void EnablePartsPhisics(Transform parent)
    {
        foreach(Transform child in parent)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            Collider coll = child.GetComponent<Collider>();
            if (rb != null) rb.isKinematic = false;
            if (coll != null) coll.enabled = true;
        }
    }

    private void ScatterParts(Transform parent, float scatterMultiplier)
    {
        foreach (Transform child in parent)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            float force = UnityEngine.Random.Range(0.0f, 1.0f) * scatterMultiplier;
            Vector3 vector = force * new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
            rb.AddForce(vector, ForceMode.Impulse);
        }
    }
}
