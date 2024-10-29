using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteSnow : Enemy {
    public override string EnemyName { get; } = "White Snow";

    public WhiteSnow() : base(40) { } // Class WhiteSnow initializes with 40 life points

    // Method Attack return the random value of method Attack
    public override int Attack() {
        dice.NumberOfSides = 16;
        int attackDamage = dice.Roll();
        Debug.Log($"{EnemyName} poisoned you and deals {attackDamage} damage to you.");
        return attackDamage;
    }
}