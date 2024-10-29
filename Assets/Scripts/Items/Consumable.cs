using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : Item {
    public override bool IsUsable { get; } = true;
    public abstract void ConsumableHeal(ref int healGiven);
}