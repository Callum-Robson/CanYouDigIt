using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "new CustomTile", menuName = "Custom Tile")]
public class CustomTile : Tile
{
    public bool destructible;
}
