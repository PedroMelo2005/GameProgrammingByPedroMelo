using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Weapon {
    public override string ItemName { get; } = "Dagger";

    public override void WeaponDamage(ref int damage) {
        dice.NumberOfSides = 8;
        damage = dice.Roll();
    }
}