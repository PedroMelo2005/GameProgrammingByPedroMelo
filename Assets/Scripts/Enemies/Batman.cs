using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batman : Enemy {
    public override string EnemyName { get; } = "Batman";

    public Batman() : base(60) { } // Class Batman initializes with 50 life points

    // Method Attack return the random value of method Attack
    public override int Attack() {
        dice.NumberOfSides = 25;
        int attackDamage = dice.Roll();
        Debug.Log($"{EnemyName} threw a boomerang in you and deals {attackDamage} damage to you.");
        return attackDamage;
    }
}