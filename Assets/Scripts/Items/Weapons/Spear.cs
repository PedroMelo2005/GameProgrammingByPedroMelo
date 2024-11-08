using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon {
    public override string ItemName { get; } = "Spear";

    public override void WeaponDamage(ref int damage) {
        dice.NumberOfSides = 20;
        damage = dice.Roll();
    }
}