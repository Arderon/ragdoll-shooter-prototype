using System.Threading;
using UnityEngine;

public abstract class EquipableItem : MonoBehaviour
{
    private bool _isEquiped = false;
    private float _timer = 0f;
    private const float _timeTillPickUp = 1.5f;
    public abstract void Use();

    public void OnEquip()
    {
        _isEquiped = true;
    }

    public void OnDrop()
    {
        _isEquiped = false;
        _timer = _timeTillPickUp;
    }

    public bool CanBeEquiped()
    {
        return _timer <= 0 && !_isEquiped;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
    }

}
