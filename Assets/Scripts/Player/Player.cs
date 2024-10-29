using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;

public class Player : MonoBehaviour {
    public string PlayerName = "";
    public const int _maxPlayerLife = 50;
    public const int _minPlayerLife = 0;
    public int PlayerLife = _maxPlayerLife;
    public bool IsPlayerAlive = true;

    // Create instances
    public Inventory InventoryInstance = new Inventory(); // InventoryInstance Instance

    // Function GetPlayerLife that display the player's life
    public void GetPlayerLife() {
        Debug.Log($"{PlayerName} you have {PlayerLife} life points!");
    }

    // Function TakeDamage
    public void TakeDamage(int damageTaken) {
        PlayerLife -= damageTaken;

        // If PlayerLife is less or equal 0 the Player is dead
        if (PlayerLife <= _minPlayerLife) {
            IsPlayerAlive = false;
            PlayerLife = _minPlayerLife;
        }
    }

    // Function TakeHeal
    public void TakeHeal(int healTaken) {
        PlayerLife += healTaken;

        // If PlayerLife is greater than MaxPlayerLife the PlayerLife will be assigned to the MaxPlayerLife
        if (PlayerLife > _maxPlayerLife) {
            PlayerLife = _maxPlayerLife;
        }
    }

    // Function ResetPlayerStats that reset the player's life and player's inventory
    public void ResetPlayerStats() {
        PlayerLife = _maxPlayerLife;
        IsPlayerAlive = true;
        InventoryInstance.InventoryList.Clear();
    }
}