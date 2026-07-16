using System;
using UnityEngine;
using static UnityEditor.Progress;

public class EquipManager : MonoBehaviour
{
    [SerializeField] Transform handsTransform;
    public bool HasItem = false;
    private EquipableItem currentItem;

    public Action OnItemEquip;
    public Action OnItemDrop;

    public void EquipItem(EquipableItem item)
    {
        if (HasItem) 
        {
            Debug.Log("Already has equiped item");
            return;
        }
        Debug.Log(item.name + "equiped");
        HasItem = true;
        item.transform.parent = handsTransform;
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        currentItem = item;
        OnItemEquip?.Invoke();
    }

    public void DropItem()
    {
        Debug.Log(currentItem.name + "droped");
        Console.WriteLine("Absdb");
    }

    public void UseItem()
    {
        if (currentItem != null)
        {
            currentItem.Use();
        } 
    }
}
