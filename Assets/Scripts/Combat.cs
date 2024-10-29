using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Combat {
    DiceRoller dice = new DiceRoller();

    // Function PlayerTurn
    public void PlayerTurn(Enemy enemy) {
        int playerHeal = 0;
        int playerDamage = 0;
        int choice = 0;
        bool validChoice = false;
        string userInput = "";

        // Check if the player has a weapon in the inventory
        List<Weapon> weapons = GameManager.player.InventoryInstance.GetWeapons(); // List of weapons
        List<Consumable> consumables = GameManager.player.InventoryInstance.GetConsumables(); // List of consumables
        Weapon selectedWeapon = null;
        Consumable selectedConsumable = null;

        // If loop with logic to select the consumable
        if (GameManager.player.PlayerLife < Player._maxPlayerLife) {
            GameManager.player.GetPlayerLife(); // Display player's life
            Debug.Log("Choose a consumable to use:");
            for (int x = 0; x < consumables.Count; x++) {
                Debug.Log($"{x + 1}: {consumables[x].ItemName}");
            }

            Debug.Log($"{consumables.Count + 1}: God's mercy");
            validChoice = false; // Reassign validChoice to false

            // While loop that keep running the code while validChoice is false
            while (validChoice != true) {
                Debug.Log("Enter your choice: ");
                /*
                userInput = (Console.ReadLine() ?? "");
                */
                // If player input is greater or equal 1 and the choice is less or equal the number of weapons in the List the variable validChoice is true
                if (int.TryParse(userInput, out choice) && choice >= 1 && choice <= consumables.Count + 1) {
                    validChoice = true; // Reassign validChoice to true
                }
                else {
                    Debug.Log("Invalid choice. Please press the number of the consumable");
                }
            }
            // If loop that if choice is less or equal the amount of consumables will run the code
            if (choice <= consumables.Count && consumables.Count > 0) {
                selectedConsumable = consumables[choice - 1]; // Assign the consumables[choice - 1] to selectedConsumable
            }

        }

        // If player don't have consumables in the inventory the player will receive a heal of until 4 life points
        if (selectedConsumable == null && GameManager.player.PlayerLife < Player._maxPlayerLife) {
            Debug.Log("The God's will have mercy and will heal you a bit");
            dice.NumberOfSides = 4; // Heal of Gods mercy
            playerHeal = dice.Roll();
            GameManager.player.PlayerLife += playerHeal;
            Debug.Log($"You healed {playerHeal} life points");
        }
        else if (selectedConsumable != null && GameManager.player.PlayerLife < Player._maxPlayerLife) {
            // Call function GiveHeal
            selectedConsumable.ConsumableHeal(ref playerHeal);
            GameManager.player.TakeHeal(playerHeal);
            Debug.Log($"You used the {selectedConsumable.ItemName}");
            Debug.Log($"You healed {playerHeal} life points");
            Debug.Log($"The {selectedConsumable.ItemName} has been removed of you inventory");
            // Call function RemoveItemOfInventory
            GameManager.player.InventoryInstance.RemoveItemOfInventory(selectedConsumable);
        }

        // If loop with logic to select the weapon
        if (weapons.Count >= 0) {
            GameManager.player.GetPlayerLife(); // Display player's life
            Debug.Log("Choose a weapon:");
            for (int x = 0; x < weapons.Count; x++) {
                Debug.Log($"{x + 1}: {weapons[x].ItemName}");
            }
            Debug.Log($"{weapons.Count + 1}: Fists");
            validChoice = false; // Reassign validChoice to false

            // While loop that keep running the code while validChoice is false
            while (validChoice != true) {
                Debug.Log("Enter your choice: ");
                /*
                userInput = (Console.ReadLine() ?? "");
                */
                // If player input is greater or equal 1 and the choice is less or equal the number of weapons in the List the variable validChoice is true
                if (int.TryParse(userInput, out choice) && choice >= 1 && choice <= weapons.Count + 1) {
                    validChoice = true; // Reassign validChoice to true
                }
                else {
                    Debug.Log("Invalid choice. Please press the number of the weapon.");
                }
            }
            // If loop that if choice is less or equal the amount of weapons will run the code
            if (choice <= weapons.Count) {
                selectedWeapon = weapons[choice - 1]; // Assign the weapons[choice - 1] to selectedWeapon
            }
        }

        // If player don't have weapon in the inventory the player will fight with the hands
        if (selectedWeapon == null || (weapons.Count > 0 && choice == weapons.Count + 1)) {
            Debug.Log("FIGHT:");
            Debug.Log("You chose to attack with your fists.");
            dice.NumberOfSides = 4; // Damage attacking with Fist
            playerDamage = dice.Roll();
            Debug.Log($"You attack with your fists and deal {playerDamage} damage.");
        }
        else {
            Debug.Log("FIGHT:");
            Debug.Log($"You attack with your {selectedWeapon.ItemName}.");
            // Call function WeaponDamage
            selectedWeapon.WeaponDamage(ref playerDamage);
            Debug.Log($"You deal {playerDamage} damage with your {selectedWeapon.ItemName}.");
        }
        // Call function enemy.TakeDamage
        enemy.TakeDamage(playerDamage);
    }

    //Function EnemyTurn
    public void EnemyTurn(Enemy enemy) {
        int enemyDamage = enemy.Attack(); // Assign enemy.Attack to enemyDamage
                                          // Call function player.TakeDamage
        GameManager.player.TakeDamage(enemyDamage);
    }

    // Function WinLose check if player won or lost
    public void CombatWinLose(Enemy enemy) {
        // If loop to display the winner
        if (enemy.IsAlive() == false) { // PLAYER WON
            Debug.Log($"You won the combat and killed the {enemy.EnemyName}");
            // Call function AddItemToInventory
            GameManager.player.InventoryInstance.AddItemToInventory(ref RoomBase.itemFound);
            Debug.Log($"Here is your reward: {RoomBase.itemFound}");
            CombatRoom.doesPlayerWon = true;
        }
        else if (GameManager.player.IsPlayerAlive != true) { // PLAYER LOST
            Debug.Log("You lost the combat!");
            CombatRoom.doesPlayerWon = false;
        }
    }

}