using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batman : Enemy {
    internal override int initialLife { get; } = 60;
    public override string EnemyName { get; } = "Batman";

    // Method Attack return the random value of method Attack
    public override void Attack(ref int damage) {
        dice.NumberOfSides = 25;
        damage = dice.Roll();
        Debug.Log($"{EnemyName} threw a boomerang in you and deals {damage} damage to you.");
    }
}