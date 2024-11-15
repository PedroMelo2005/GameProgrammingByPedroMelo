using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SerializeField]
public class ItemData {
    public Item item;
    public bool isRotated = false;
    public Vector2Int matrixPosition;
    Vector3 slotPosition = Vector3.zero;

}