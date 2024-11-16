using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GeneralItemName", menuName = "Item/New General Item")]
public class ItemGeneral : Item {
    [Header("General Components")]
    public GeneralType generalType;
    
    public enum GeneralType {
        Food,
        Drink,
        Gold
    }

}