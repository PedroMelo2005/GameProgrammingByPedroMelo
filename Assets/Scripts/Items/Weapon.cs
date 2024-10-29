using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item {
    public override bool IsUsable { get; } = false;
    public abstract void WeaponDamage(ref int damage);
}