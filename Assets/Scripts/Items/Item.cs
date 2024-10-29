using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    internal DiceRoller dice = new DiceRoller();
    public virtual string ItemName { get; }
    public virtual bool IsUsable { get; }
}