using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {
    internal DiceRoller dice = new DiceRoller();
    public abstract string ItemName { get; }
    public abstract bool IsUsable { get; }
}