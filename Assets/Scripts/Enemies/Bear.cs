using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Enemy {
    public override string EnemyName { get; } = "Bear";

    public Bear() : base(35) { } // Class Bear initializes with 35 life points

    // Method Attack return the random value of method Attack
    public override int Attack() {
        dice.NumberOfSides = 14;
        int attackDamage = dice.Roll();
        Debug.Log($"{EnemyName} bitted you and deals {attackDamage} damage to you.");
        return attackDamage;
    }
}