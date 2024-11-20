using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public List<InventoryPanel> panelList = new List<InventoryPanel>();

    void Start () {
        panelList = GetComponents<InventoryPanel>().ToList();
    }

    void Update() {

    }

}