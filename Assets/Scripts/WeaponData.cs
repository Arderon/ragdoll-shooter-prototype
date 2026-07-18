using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string Name;
    public int BaseDamage;
    public float BaseAttackSpeed = 0.5f;
    public Sprite Icon;
    public bool IsRange;

    [Header("Range Weapon")]
    public GameObject ProjectilePrefab;
    public float ProjectileSpeed = 10;
    public int MagazineCapacity = 10;
    public int ReloadTime = 2;
    public int ProjectilesPerShoot = 1;
    public int ProjectilesPerRound = 1;
    public float SpreadX = 0f;    
    public float SpreadY = 0f;    
}
