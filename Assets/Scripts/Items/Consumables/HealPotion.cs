using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : Consumable {
    public override string ItemName { get; } = "Heal Potion";

    public override void ConsumableHeal(ref int healGiven) {
        dice.NumberOfSides = 12;
        healGiven = dice.Roll();
    }
}