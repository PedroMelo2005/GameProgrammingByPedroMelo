using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponName", menuName = "Item/New Weapon")]
public class ItemWeapon : Item {
    [Header("Weapon Components")]
    public int damage;
}