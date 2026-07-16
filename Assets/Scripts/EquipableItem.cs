using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public abstract class EquipableItem : MonoBehaviour
{
    private bool _isEquiped = false;
    private float _timer = 0f;
    private const float _timeTillPickUp = 1f;
    private Rigidbody _rb;
    private Collider _col;
    private bool _phisicsEnabled = false;
    public abstract void Use();

    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
    }

    private void Start()
    {
        if (_isEquiped)
        {
            DisablePhisics();
        }
        else
        {
            EnablePhisics();
        }
    }

    public void OnEquip()
    {
        _isEquiped = true;
        DisablePhisics();
    }

    public void OnDrop()
    {
        _isEquiped = false;
        _timer = _timeTillPickUp;
        EnablePhisics();
    }

    public bool CanBeEquiped()
    {
        return _timer <= 0 && !_isEquiped;
    }

    private void DisablePhisics()
    {
        _rb.isKinematic = true;
        _col.isTrigger = true;
        _phisicsEnabled = false;
    }

    private void EnablePhisics()
    {
        _rb.isKinematic = false;
        _col.isTrigger = false;
        _phisicsEnabled = true;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
    }
}
