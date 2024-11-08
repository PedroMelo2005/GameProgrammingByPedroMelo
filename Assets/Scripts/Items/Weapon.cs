using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {
    public override bool IsUsable { get; } = false;
    public virtual void WeaponDamage(ref int damage) { }
}