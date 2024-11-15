using System.Collections;
using System.Collections.Generic;
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

    public void Setup(Item item) {
        switch (item.rarity) {
            case Item.Rarity.Common:
                ItemIcon.color = Color.white;
                break;

            case Item.Rarity.Rare:
                ItemIcon.color = Color.blue;
                break;

            case Item.Rarity.Epic:
                ItemIcon.color = Color.magenta;
                break;

            case Item.Rarity.Legendary:
                ItemIcon.color = Color.yellow;
                break;
        }
        ItemName.text = item.ItemName;
    }

}