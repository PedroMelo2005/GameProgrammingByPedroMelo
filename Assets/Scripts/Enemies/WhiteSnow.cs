using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteSnow : Enemy {
    internal override int initialLife { get; } = 40;
    public override string EnemyName { get; } = "White Snow";

    // Method Attack return the random value of method Attack
    public override void Attack(ref int damage) {
        dice.NumberOfSides = 16;
        damage = dice.Roll();
        Debug.Log($"{EnemyName} poisoned you and deals {damage} damage to you.");
     }
}