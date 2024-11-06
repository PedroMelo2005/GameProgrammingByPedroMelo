using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller {
    public int NumberOfSides = 6;

    // Method Roll that generates a random number
    public int Roll() {
        int randomRoll = Random.Range(0, NumberOfSides + 1);
        return randomRoll;
    }

}