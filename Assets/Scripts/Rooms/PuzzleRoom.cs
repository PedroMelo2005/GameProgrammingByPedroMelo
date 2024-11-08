using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoom : RoomBase {
    int timesSearched = 0;
    int puzzlesTimes = 0;
    string puzzleAnswer = "";
    bool puzzleIsCorrect = false;

    // Creating the Arrays
    string[] puzzleList = { // Array of "puzzleList"
        "What is full of holes but still holds water?", // 0
        "What can you catch but not throw?", // 1
        "What has many rings but no fingers?", // 2
        "I go all around the world, but never leave the corner. What am I?", // 3
        "What goes up but never comes back down?", // 4
    };
    string[] puzzlesAnswerList = { // Array of "puzzlesAnswerList"
        "sponge", // 0
        "cold", // 1
        "telephone", // 2
        "stamp", // 3
        "age", // 4
    };

    // Method that return a random value of which puzzle will be asked for the player
    int RandomPuzzles() {
        int randomPuzzle = Random.Range(0, puzzleList.Length); // Assign random value to "randomPuzzle"
        return randomPuzzle; // return "randomPuzzle"
    }

    public override string roomName { get; } = "Puzzle Room";

    public override void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            int index = RandomPuzzles(); // Assign the variable RandomPuzzles() to the variable index

            // Display message of which room the player enter
            Debug.Log($"You entered in the {roomName}"); // DEBUG

            // If loop that if player has solved or tried to solve the puzzle more than 3 times the player will can't solve more puzzles
            if (puzzlesTimes < 3) {
                Debug.Log("Answer the correct answer to the riddle");
                Debug.Log("RIDDLE:");
                Debug.Log(puzzleList[index]);
                /*
                puzzleAnswer = (Console.ReadLine() ?? "").ToLower();
                */

                // If loop that check if the answer is correct
                if (puzzleAnswer == puzzlesAnswerList[index]) {
                    Debug.Log($"Good job {Player.Instance.PlayerName} your answer is correct!");
                    Debug.Log("Try to search for an item");
                    puzzleIsCorrect = true;
                    puzzlesTimes++;
                }
                else if (puzzlesTimes < 3) {
                    puzzlesTimes++;
                    Debug.Log("Your answer is wrong!");
                    Debug.Log("Do you want to try again ?");
                    Debug.Log("RIDDLE:");
                    Debug.Log(puzzleList[index]);
                    /*
                    puzzleAnswer = (Console.ReadLine() ?? "");
                    */
                    if (puzzleAnswer == puzzlesAnswerList[index]) {
                        Debug.Log($"Good job {Player.Instance.PlayerName} your answer is correct!");
                        Debug.Log("Try to search for an item");
                        puzzleIsCorrect = true;
                        puzzlesTimes++;
                    }
                    else {
                        Debug.Log($"Hahaha. {Player.Instance.PlayerName} your answer is wrong again!");
                        puzzlesTimes++;
                    }
                }
            }
            else {
                Debug.Log($"{Player.Instance.PlayerName} you can't try to solve puzzles more than 3 times");
            }
        }
    }

    public override void OnRoomSearched() {
        if (puzzleIsCorrect && timesSearched < 3) {
            // Call function AddItemToInventory
            Inventory.Instance.AddItemToInventory(ref itemFound);
            Debug.Log($"You found: {itemFound}");
            timesSearched++;
        }
        else if (puzzleIsCorrect && timesSearched >= 3) {
            Debug.Log($"{Player.Instance.PlayerName} you can't search more than 3 times on {roomName}");
        }
        else {
            Debug.Log("Nothing more on this room!");
        }
    }

}