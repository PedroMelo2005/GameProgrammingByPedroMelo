using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSword : Weapon {
    public override string ItemName { get; } = "Long Sword";

    public override void WeaponDamage(ref int damage) {
        dice.NumberOfSides = 12;
        damage = dice.Roll();
    }
}