using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ItemScript {
    public override bool IsUsable { get; } = false;
    public virtual void WeaponDamage(ref int damage) { }
}