using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    internal DiceRoller dice = new DiceRoller();
    public abstract string EnemyName { get; }
    public int EnemyLife { get; set; }
    public abstract int Attack();

    // Method that return a random enemy of enemyList
    public static Enemy RandomEnemy() {
        // Create a List that has probability of select an enemy
        List<Enemy> enemyList = new List<Enemy>(); // List of enemies Instances

        // For loop that add 30 times "new Knight()" 30% of chance to be selected
        for (int i = 0; i < 30; i++) {
            enemyList.Add(new Knight());
        }

        // For loop that add 30 times "new Bear()" 30% of chance to be selected
        for (int i = 0; i < 30; i++) {
            enemyList.Add(new Bear());
        }

        // For loop that add 25 times "new WhiteSnow()" 25% of chance to be selected
        for (int i = 0; i < 25; i++) {
            enemyList.Add(new WhiteSnow());
        }

        // For loop that add 15 times "new Batman()" 15% of chance to be selected
        for (int i = 0; i < 15; i++) {
            enemyList.Add(new Batman());
        }

        return enemyList[Random.Range(0, enemyList.Count)]; // Return a random enemy of enemyList
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

    // Constructor initializes enemies life
    protected Enemy(int initialLife) {
        EnemyLife = initialLife; // Assign initialLife to EnemyLife
    }

}