using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    internal DiceRoller dice = new DiceRoller();
    internal virtual int initialLife { get; }
    public virtual string EnemyName { get; }
    public int EnemyLife;
    public virtual void Attack(ref int damage) { }

    // Create function "ResetEnemyStats()"
    public void ResetEnemyStats() {
        EnemyLife = initialLife; // Assign variable "EnemyLife" to "initialLife"
    }

    // Method IsAlive return if enemy is alive
    public bool IsAlive() {
        if (EnemyLife > 0) {
            return true;
        }
        else {
            return false;
        }
    }

    // Create function TakeDamage that deals the damage on the enemy
    public void TakeDamage(int damageTaken) {
        EnemyLife -= damageTaken;

        // If loop that reassign the EnemyLife to 0 if the EnemyLife is less or equal 0
        if (EnemyLife <= 0) {
            EnemyLife = 0;
        }
        Debug.Log($"{EnemyName} takes {damageTaken} damage. {EnemyName} has: {EnemyLife} life points");
    }

}