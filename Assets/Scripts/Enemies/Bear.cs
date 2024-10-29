using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Enemy {
    internal override int initialLife { get; } = 35;
    public override string EnemyName { get; } = "Bear";

    // Method Attack return the random value of method Attack
    public override void Attack(ref int damage) {
        dice.NumberOfSides = 14;
        damage = dice.Roll();
        Debug.Log($"{EnemyName} bitted you and deals {damage} damage to you.");
    }
}