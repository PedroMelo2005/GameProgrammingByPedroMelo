using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour {
    [SerializeField] private Image ItemIcon;
    [SerializeField] private Text ItemName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(ItemData itemData) {
        switch (itemData.ItemRarity) {
            case Rarity.Common:
                ItemIcon.color = Color.white;
                break;

            case Rarity.Rare:
                ItemIcon.color = Color.blue;
                break;

            case Rarity.Epic:
                ItemIcon.color = Color.magenta;
                break;

            case Rarity.Legendary:
                ItemIcon.color = Color.yellow;
                break;
        }
        ItemName.text = itemData.ItemName;
    }

}