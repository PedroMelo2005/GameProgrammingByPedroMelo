using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon {
    public override string ItemName { get; } = "Axe";

    public override void WeaponDamage(ref int damage) {
        dice.NumberOfSides = 40;
        damage = dice.Roll();
    }
}