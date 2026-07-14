using UnityEngine;

public class Hands : MonoBehaviour
{
    [SerializeField] Transform handsTransform;
    public bool HasItem;
    private EquipableItem currentItem;

    public void EquipItem(EquipableItem item)
    {

    }

    public void DropItem()
    {
    }
}
