using System;
using UnityEngine;
using static UnityEditor.Progress;

public class EquipManager : MonoBehaviour
{
    [SerializeField] Transform handsTransform;
    [SerializeField] Transform sceneEquipablesTransform;
    public bool HasItem = false;
    private EquipableItem currentItem;

    public Action OnItemEquip;
    public Action OnItemDrop;

    [SerializeField] float dropForce = 200f;

    public void EquipItem(EquipableItem item)
    {
        if (!item.CanBeEquiped())
        {
            return;
        }

        if (HasItem) 
        {
            return;
        }

        Debug.Log(item.name + "equiped");
        item.transform.parent = handsTransform;
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.OnEquip();
        currentItem = item;
        HasItem = true;
        OnItemEquip?.Invoke();
    }

    public void DropItem()
    {
        Debug.Log(currentItem.name + "droped");
        currentItem.transform.parent = sceneEquipablesTransform;
        currentItem.OnDrop();
        ThrowObj(currentItem.gameObject, dropForce);
        currentItem = null;
        HasItem = false;
        OnItemDrop?.Invoke();
    }

    public void ThrowObj(GameObject obj, float throwForce)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if(rb == null)
        {
            Debug.Log("Cant throw. Rb is null");
        }
        Vector3 force = handsTransform.forward * throwForce;
        rb.AddForce(force);
    }

    public void UseItem()
    {
        if (currentItem != null)
        {
            currentItem.Use();
        } 
    }
}
