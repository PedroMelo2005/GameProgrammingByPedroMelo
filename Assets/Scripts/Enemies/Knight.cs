using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Enemy {
    internal override int initialLife { get; } = 30;
    public override string EnemyName { get; } = "Knight";

    // Method Attack return the random value of method Attack
    public override void Attack(ref int damage) {
        dice.NumberOfSides = 10;
        damage = dice.Roll();
        Debug.Log($"{EnemyName} attacked and deals {damage} damage to you.");
    }
}