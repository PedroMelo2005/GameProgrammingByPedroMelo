using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : Weapon {
    public override string ItemName { get; } = "Light Saber";

    public override void WeaponDamage(ref int damage) {
        dice.NumberOfSides = 20;
        damage = dice.Roll();
    }
}