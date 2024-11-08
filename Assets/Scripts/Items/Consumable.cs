using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item {
    public override bool IsUsable { get; } = true;
    public virtual void ConsumableHeal(ref int healGiven) { }
}