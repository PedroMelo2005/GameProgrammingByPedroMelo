using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Enemy {
    public override string EnemyName { get; } = "Knight";

    public Knight() : base(30) { } // Class Knight initializes with 30 life points

    // Method Attack return the random value of method Attack
    public override int Attack() {
        dice.NumberOfSides = 10;
        int attackDamage = dice.Roll();
        Debug.Log($"{EnemyName} attacked and deals {attackDamage} damage to you.");
        return attackDamage;
    }
}