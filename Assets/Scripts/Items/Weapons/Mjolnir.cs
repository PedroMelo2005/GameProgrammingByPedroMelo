using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mjolnir : Weapon {
    public override string ItemName { get; } = "Mjolnir";

    public override void WeaponDamage(ref int damage) {
        dice.NumberOfSides = 40;
        damage = dice.Roll();
    }
}