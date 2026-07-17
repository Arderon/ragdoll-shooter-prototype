using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string Name;
    public int BaseDamage;
    public float BaseAttackSpeed;
    public Sprite Icon;
    public bool IsRange;

    [Header("Range Weapon")]
    public GameObject ProjectilePrefab;
    public float ProjectileSpeed;
    public int MagazineCapacity;
    public int ReloadTime;
}
